using BuildingManagement.Application.DTOs;
using BuildingManagement.Application.Interfaces.Services;
using BuildingManagement.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Http;
using BuildingManagement.Application.Interfaces.Repositories;

namespace BuildingManagement.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PaymentService> _logger;
        private readonly IDichVuHoaDonService _hoaDonService;
        private readonly IUnitOfWork _unitOfWork;
        public PaymentService(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            ILogger<PaymentService> logger,
            IDichVuHoaDonService hoaDonService, IUnitOfWork unitOfWork)
        {
            _hoaDonService = hoaDonService;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreatePaymentResponse> CreatePaymentLinkAsync(CreatePaymentRequest request)
        {
            try
            {
                // Kiểm tra hóa đơn có tồn tại và chưa thanh toán
                var invoice = await _hoaDonService.GetHoaDonByID(request.MaHD);
                if (invoice == null)
                    throw new ArgumentException("Hóa đơn không tồn tại");

                if (invoice.IsThanhToan)
                    throw new InvalidOperationException("Hóa đơn đã được thanh toán");

                // Kiểm tra xem đã có payment link chưa hết hạn cho hóa đơn này chưa
                var existingPayment = await _unitOfWork.PaymentInfors
                    .GetFirstOrDefaultAsync(p => p.MaHD == request.MaHD &&
                               p.Status == "PENDING" &&
                               p.ExpiredAt > DateTime.UtcNow);

                if (existingPayment != null)
                {
                    return new CreatePaymentResponse
                    {
                        PaymentLinkId = existingPayment.PaymentLinkId,
                        OrderCode = existingPayment.OrderCode,
                        QrCode = existingPayment.QrCode ?? "",
                        CheckoutUrl = existingPayment.CheckoutUrl ?? "",
                        Status = existingPayment.Status,
                        CreatedAt = existingPayment.CreatedAt,
                        ExpiredAt = existingPayment.ExpiredAt ?? DateTime.UtcNow.AddMinutes(15)
                    };
                }

                // Tạo order code unique
                var orderCode = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();

                // Tạo payment request cho PayOS
                var payosRequest = new PayOSCreatePaymentRequest
                {
                    orderCode = orderCode,
                    amount = request.Amount,
                    description = request.Description,
                    returnUrl = request.ReturnUrl ?? $"{_configuration["AppSettings:BaseUrl"]}/payment/success",
                    cancelUrl = request.CancelUrl ?? $"{_configuration["AppSettings:BaseUrl"]}/payment/cancel",
                    buyerName = invoice.TenKhachHang,
                    buyerEmail = invoice.Email ?? "",
                    buyerPhone = invoice.SoDienThoai ?? "",
                    buyerAddress = $"{invoice.TenTN}, {invoice.TenTL}, {invoice.TenKN}",
                    expiredAt = (int)DateTimeOffset.UtcNow.AddMinutes(request.ExpiredAt).ToUnixTimeSeconds(),
                    items = new List<PayOSItem>
                    {
                        new PayOSItem
                        {
                            name = $"Hóa đơn HD-{request.MaHD.ToString().PadLeft(4, '0')}",
                            quantity = 1,
                            price = request.Amount
                        }
                    }
                };

                // Tạo signature
                payosRequest.signature = await GeneratePaymentSignatureAsync(payosRequest);

                // Gọi PayOS API
                var payosResponse = await CallPayOSCreatePaymentAsync(payosRequest);

                if (payosResponse == null || payosResponse.code != "00")
                {
                    throw new Exception($"PayOS API error: {payosResponse?.desc ?? "Unknown error"}");
                }

                // Lưu thông tin payment vào database
                var paymentInfo = new PaymentInfo
                {
                    OrderCode = orderCode,
                    MaHD = request.MaHD,
                    PaymentLinkId = payosResponse.data.paymentLinkId,
                    Amount = request.Amount,
                    Description = request.Description,
                    Status = "PENDING",
                    QrCode = payosResponse.data.qrCode,
                    CheckoutUrl = payosResponse.data.checkoutUrl,
                    CreatedAt = DateTime.UtcNow,
                    ExpiredAt = DateTime.UtcNow.AddMinutes(request.ExpiredAt)
                };

                await _unitOfWork.PaymentInfors.AddAsync(paymentInfo);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"Payment link created successfully for invoice {request.MaHD}, order {orderCode}");

                return new CreatePaymentResponse
                {
                    PaymentLinkId = paymentInfo.PaymentLinkId,
                    OrderCode = paymentInfo.OrderCode,
                    QrCode = paymentInfo.QrCode ?? "",
                    CheckoutUrl = paymentInfo.CheckoutUrl ?? "",
                    Status = paymentInfo.Status,
                    CreatedAt = paymentInfo.CreatedAt,
                    ExpiredAt = paymentInfo.ExpiredAt ?? DateTime.UtcNow.AddMinutes(15)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating payment link for invoice {request.MaHD}");
                throw;
            }
        }

        public async Task<PaymentStatusResponse> GetPaymentStatusAsync(string orderCode)
        {
            try
            {
                var payment = await _unitOfWork.PaymentInfors
                    .GetFirstOrDefaultAsync(p => p.OrderCode == orderCode);

                if (payment == null)
                    throw new ArgumentException("Payment not found");

                // Gọi PayOS API để lấy status mới nhất
                var payosStatus = await CallPayOSGetPaymentStatusAsync(orderCode);

                if (payosStatus != null && payosStatus.code == "00")
                {
                    // Cập nhật status từ PayOS
                    if (payment.Status != payosStatus.data.status)
                    {
                        payment.Status = payosStatus.data.status;

                        if (payosStatus.data.status == "PAID" && payosStatus.data.transactions.Any())
                        {
                            var transaction = payosStatus.data.transactions.First();
                            payment.PaidAt = DateTime.UtcNow;
                            payment.TransactionId = transaction.reference;
                            payment.CounterAccountBankId = transaction.counterAccountBankId;
                            payment.CounterAccountBankName = transaction.counterAccountBankName;
                            payment.CounterAccountName = transaction.counterAccountName;
                            payment.CounterAccountNumber = transaction.counterAccountNumber;
                        }

                        await _unitOfWork.SaveChangesAsync();
                    }
                }

                return new PaymentStatusResponse
                {
                    OrderCode = payment.OrderCode,
                    Status = payment.Status,
                    Amount = payment.Amount,
                    Description = payment.Description,
                    CreatedAt = payment.CreatedAt,
                    PaidAt = payment.PaidAt,
                    TransactionId = payment.TransactionId,
                    BankCode = payment.CounterAccountBankId
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting payment status for order {orderCode}");
                throw;
            }
        }

        public async Task<List<PaymentHistoryResponse>> GetPaymentHistoryAsync(int maHD)
        {
            try
            {
                var payments = await _unitOfWork.PaymentInfors
                    .Where(p => p.MaHD == maHD)
                    .OrderByDescending(p => p.CreatedAt)
                    .Select(p => new PaymentHistoryResponse
                    {
                        OrderCode = p.OrderCode,
                        MaHD = p.MaHD,
                        Amount = p.Amount,
                        Status = p.Status,
                        CreatedAt = p.CreatedAt,
                        PaidAt = p.PaidAt,
                        TransactionId = p.TransactionId
                    })
                    .ToListAsync();

                return payments;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting payment history for invoice {maHD}");
                throw;
            }
        }

        public async Task<PaymentInfo?> GetPaymentByOrderCodeAsync(string orderCode)
        {
            return await _unitOfWork.PaymentInfors
                .Include(p => p.HoaDon)
                .FirstOrDefaultAsync(p => p.OrderCode == orderCode);
        }

        public async Task<bool> UpdatePaymentStatusAsync(string orderCode, string status, string? transactionId = null)
        {
            try
            {
                var payment = await _context.PaymentInfos
                    .FirstOrDefaultAsync(p => p.OrderCode == orderCode);

                if (payment == null)
                    return false;

                payment.Status = status;

                if (status == "PAID")
                {
                    payment.PaidAt = DateTime.UtcNow;
                    if (!string.IsNullOrEmpty(transactionId))
                        payment.TransactionId = transactionId;
                }
                else if (status == "CANCELLED")
                {
                    payment.CancelledAt = DateTime.UtcNow;
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating payment status for order {orderCode}");
                return false;
            }
        }

        public async Task<bool> CancelPaymentAsync(string orderCode)
        {
            try
            {
                var payment = await _context.PaymentInfos
                    .FirstOrDefaultAsync(p => p.OrderCode == orderCode);

                if (payment == null || payment.Status != "PENDING")
                    return false;

                // Gọi PayOS API để cancel payment
                var cancelled = await CallPayOSCancelPaymentAsync(orderCode);

                if (cancelled)
                {
                    payment.Status = "CANCELLED";
                    payment.CancelledAt = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                }

                return cancelled;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error cancelling payment for order {orderCode}");
                return false;
            }
        }

        public async Task<string> GeneratePaymentSignatureAsync(PayOSCreatePaymentRequest request)
        {
            try
            {
                var checksumKey = _configuration["PayOS:ChecksumKey"];

                // Tạo string để hash theo format của PayOS
                var dataString = $"amount={request.amount}&cancelUrl={request.cancelUrl}&description={request.description}&orderCode={request.orderCode}&returnUrl={request.returnUrl}";

                using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(checksumKey)))
                {
                    var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dataString));
                    return Convert.ToHexString(hash).ToLower();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating payment signature");
                throw;
            }
        }

        public async Task<bool> VerifyWebhookSignatureAsync(string webhookData, string signature)
        {
            try
            {
                var webhookKey = _configuration["PayOS:WebhookKey"];

                using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(webhookKey)))
                {
                    var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(webhookData));
                    var computedSignature = Convert.ToHexString(computedHash).ToLower();

                    return signature.Equals(computedSignature, StringComparison.OrdinalIgnoreCase);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying webhook signature");
                return false;
            }
        }

        #region Private Methods - PayOS API Calls

        private async Task<PayOSCreatePaymentResponse?> CallPayOSCreatePaymentAsync(PayOSCreatePaymentRequest request)
        {
            try
            {
                using var httpClient = _httpClientFactory.CreateClient();

                // Set headers
                httpClient.DefaultRequestHeaders.Add("x-client-id", _configuration["PayOS:ClientId"]);
                httpClient.DefaultRequestHeaders.Add("x-api-key", _configuration["PayOS:ApiKey"]);
                httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

                var jsonContent = JsonSerializer.Serialize(request, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync($"{_configuration["PayOS:BaseUrl"]}/v2/payment-requests", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<PayOSCreatePaymentResponse>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"PayOS API error: {response.StatusCode} - {errorContent}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling PayOS create payment API");
                return null;
            }
        }

        private async Task<PayOSPaymentStatusResponse?> CallPayOSGetPaymentStatusAsync(string orderCode)
        {
            try
            {
                using var httpClient = _httpClientFactory.CreateClient();

                httpClient.DefaultRequestHeaders.Add("x-client-id", _configuration["PayOS:ClientId"]);
                httpClient.DefaultRequestHeaders.Add("x-api-key", _configuration["PayOS:ApiKey"]);

                var response = await httpClient.GetAsync($"{_configuration["PayOS:BaseUrl"]}/v2/payment-requests/{orderCode}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<PayOSPaymentStatusResponse>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error calling PayOS get payment status API for order {orderCode}");
                return null;
            }
        }

        private async Task<bool> CallPayOSCancelPaymentAsync(string orderCode)
        {
            try
            {
                using var httpClient = _httpClientFactory.CreateClient();

                httpClient.DefaultRequestHeaders.Add("x-client-id", _configuration["PayOS:ClientId"]);
                httpClient.DefaultRequestHeaders.Add("x-api-key", _configuration["PayOS:ApiKey"]);

                var cancelRequest = new { cancellationReason = "Cancelled by user" };
                var jsonContent = JsonSerializer.Serialize(cancelRequest);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync($"{_configuration["PayOS:BaseUrl"]}/v2/payment-requests/{orderCode}/cancel", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error calling PayOS cancel payment API for order {orderCode}");
                return false;
            }
        }

        #endregion
    }
}
