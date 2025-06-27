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
using BuildingManagement.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using BuildingManagement.Application.Interfaces.Services.Ultility;
using Microsoft.AspNetCore.Http;

namespace BuildingManagement.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PaymentService> _logger;
        private readonly IDichVuHoaDonService _hoaDonService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailServices;
        private readonly INotificationService _notificationService;
        public PaymentService(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            ILogger<PaymentService> logger,
            IDichVuHoaDonService hoaDonService, IUnitOfWork unitOfWork, IEmailService emailServices, INotificationService notificationService)
        {
            _hoaDonService = hoaDonService;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _emailServices = emailServices;
            _notificationService = notificationService;
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
                        OrderCode = Int64.Parse(existingPayment.OrderCode),
                        QrCode = existingPayment.QrCode ?? "",
                        CheckoutUrl = existingPayment.CheckoutUrl ?? "",
                        Status = existingPayment.Status,
                        CreatedAt = existingPayment.CreatedAt,
                        ExpiredAt = existingPayment.ExpiredAt ?? DateTime.UtcNow.AddMinutes(15)
                    };
                }

                // Tạo order code unique
                var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                var orderCode = long.Parse($"{request.MaHD}{timestamp % 10000}"); // Kết hợp MaHD với 4 chữ số cuối của timestamp
                if (orderCode > 9007199254740991)
                {
                    orderCode = Random.Shared.NextInt64(100000, 999999); // Fallback với số ngẫu nhiên 6 chữ số
                }
                var shortDescription = $"HD-{request.MaHD.ToString().PadLeft(4, '0')}";
                if (shortDescription.Length > 25)
                {
                    shortDescription = shortDescription.Substring(0, 25);
                }
                // Tạo payment request cho PayOS
                var payosRequest = new PayOSCreatePaymentRequest
                {
                    orderCode = orderCode,
                    amount = request.Amount,
                    description = request.Description,
                    returnUrl = request.ReturnUrl ?? $"{_configuration["AppSettings:BaseUrl"]}/payment/success",
                    cancelUrl = request.CancelUrl ?? $"{_configuration["AppSettings:BaseUrl"]}/payment/cancel",
                    buyerName = invoice.tnKhachHang.IsCaNhan ? invoice.tnKhachHang.HoTen : invoice.tnKhachHang.CtyTen,
                    buyerEmail = invoice.tnKhachHang.Email ?? "",
                    buyerPhone = invoice.tnKhachHang.DienThoai ?? "",
                    buyerAddress = $"{invoice.MaTN}, {invoice.MaTL}, {invoice.MaKN}",
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
                    OrderCode = orderCode.ToString(),
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
                    OrderCode = Int64.Parse(paymentInfo.OrderCode),
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
                var payments = await _unitOfWork.PaymentInfors.GetHistoryPaymentByMaHD(maHD);
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
                .GetByIdIncludeTable(orderCode);
        }

        public async Task<bool> UpdatePaymentStatusAsync(string orderCode, string status, string? transactionId = null)
        {
            try
            {
                var payment = await _unitOfWork.PaymentInfors
                    .GetFirstOrDefaultAsync(p => p.OrderCode == orderCode);
                    if (payment == null)
                    return false;
                var hd = await _unitOfWork.HoaDons
                                   .GetFirstOrDefaultAsync(h => h.MaHD == payment.MaHD);
                var dsHoaDonUpdate = await _unitOfWork.HoaDons.GetAllConditionAsync(h => h.MaKH == hd.MaKH);
                foreach(var item in dsHoaDonUpdate)
                {
                    await _unitOfWork.HoaDons.UpdateAsync(item);
                }
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

                await _unitOfWork.SaveChangesAsync();
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
                var payment = await _unitOfWork.PaymentInfors
                    .GetFirstOrDefaultAsync(p => p.OrderCode == orderCode);

                if (payment == null || payment.Status != "PENDING")
                    return false;

                // Gọi PayOS API để cancel payment
                var cancelled = await CallPayOSCancelPaymentAsync(orderCode);

                if (cancelled)
                {
                    payment.Status = "CANCELLED";
                    payment.CancelledAt = DateTime.UtcNow;
                    await _unitOfWork.SaveChangesAsync();
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

                var jsonContent = JsonSerializer.Serialize(request, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync($"https://api-merchant.payos.vn/v2/payment-requests", content);

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

                var response = await httpClient.GetAsync($"https://api-merchant.payos.vn/v2/payment-requests/{orderCode}");

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
        public async Task<bool> ProcessCompletedPaymentAsync(string orderCode)
        {
            try
            {
                _logger.LogInformation($"Processing completed payment for order {orderCode}");

                // 1. Lấy payment info từ database
                var paymentInfo = await _unitOfWork.PaymentInfors
                    .GetFirstOrDefaultAsync(p => p.OrderCode == orderCode);

                if (paymentInfo == null)
                {
                    _logger.LogWarning($"Payment not found for order code: {orderCode}");
                    return false;
                }

                // 2. Sync status từ PayOS
                var syncResult = await SyncPaymentStatusFromPayOSAsync(orderCode);
                if (!syncResult)
                {
                    _logger.LogWarning($"Failed to sync payment status from PayOS for order {orderCode}");
                    return false;
                }

                // 3. Reload payment info sau khi sync
                paymentInfo = await _unitOfWork.PaymentInfors
                    .GetFirstOrDefaultAsync(p => p.OrderCode == orderCode);

                if (paymentInfo.Status != "PAID")
                {
                    _logger.LogWarning($"Payment {orderCode} is not in PAID status: {paymentInfo.Status}");
                    return false;
                }

                // 4. Lấy thông tin hóa đơn
                var invoice = await _hoaDonService.GetHoaDonByID(paymentInfo.MaHD);
                var maKH = (int)invoice.MaKH;
                var dsHoaDon = await _hoaDonService.GetDSHoaDonByMaKH(maKH);
                if (invoice == null)
                {
                    _logger.LogWarning($"Invoice not found for MaHD: {paymentInfo.MaHD}");
                    return false;
                }

                // 5. Kiểm tra nếu hóa đơn đã được thanh toán rồi
                if (invoice.IsThanhToan)
                {
                    _logger.LogInformation($"Invoice {paymentInfo.MaHD} is already paid");
                    return true; // Không phải lỗi, chỉ là đã xử lý rồi
                }

                await _unitOfWork.BeginTransactionAsync();

                try
                {
                    // 6. Cập nhật trạng thái hóa đơn
                    await _hoaDonService.CapNhatDanhSachHoaDonTrangThaiThanhToan(dsHoaDon, true);

                    // 7. Tạo notification
                    var notification = new PaymentNotification
                    {
                        Id = Guid.NewGuid().ToString(),
                        MaHD = paymentInfo.MaHD,
                        OrderCode = orderCode,
                        Amount = paymentInfo.Amount,
                        CustomerName = invoice.tnKhachHang?.HoTen ?? invoice.tnKhachHang?.CtyTen ?? "Unknown",
                        Status = "PAID",
                        PaymentTime = paymentInfo.PaidAt?.ToString("yyyy-MM-ddTHH:mm:ssZ") ?? DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                        TransactionId = paymentInfo.TransactionId,
                        BankCode = paymentInfo.CounterAccountBankId,
                        IsRead = false,
                        CreatedAt = DateTime.UtcNow
                    };

                    // 8. Lưu notification (sử dụng interface từ dependency injection)
                    if (_notificationService != null)
                    {
                        await _notificationService.CreateNotificationAsync(notification);
                    }

                    // 9. Gửi email xác nhận
                    try
                    {
                        await SendPaymentConfirmationEmail(invoice, paymentInfo);
                    }
                    catch (Exception emailEx)
                    {
                        _logger.LogError(emailEx, $"Failed to send confirmation email for order {orderCode}");
                        // Không throw - email failure không nên break payment process
                    }

                    await _unitOfWork.CommitTransactionAsync();

                    _logger.LogInformation($"Successfully processed completed payment for order {orderCode}, invoice {paymentInfo.MaHD}");
                    return true;
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                    _logger.LogError(ex, $"Transaction failed while processing payment {orderCode}");
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing completed payment for order {orderCode}");
                return false;
            }
        }

        private async Task SendPaymentConfirmationEmail(dvHoaDon invoice, PaymentInfo paymentInfo)
        {
            try
            {
                // Implement email sending logic here
                // Hoặc sử dụng existing email service
                if (_emailServices != null)
                {
                    var subject = $"Xác nhận thanh toán - Hóa đơn HD-{paymentInfo.MaHD.ToString().PadLeft(4, '0')}";
                    var emailContent = GeneratePaymentConfirmationEmailContent(invoice, paymentInfo);

                    await _emailServices.SendEmailAsync(invoice.tnKhachHang.Email, subject, emailContent);
                    _logger.LogInformation($"Payment confirmation email would be sent for order {paymentInfo.OrderCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending payment confirmation email for order {paymentInfo.OrderCode}");
                throw;
            }
        }


        private string GeneratePaymentConfirmationEmailContent(object invoice, PaymentInfo paymentInfo)
        {
            // Generate email content
            return $@"
        <h2>Thanh toán thành công!</h2>
        <p>Mã đơn hàng: {paymentInfo.OrderCode}</p>
        <p>Số tiền: {paymentInfo.Amount:N0} VNĐ</p>
        <p>Thời gian: {paymentInfo.PaidAt}</p>
        <p>Mã giao dịch: {paymentInfo.TransactionId}</p>
    ";
        }
        public async Task<bool> SyncPaymentStatusFromPayOSAsync(string orderCode)
        {
            try
            {
                _logger.LogInformation($"Syncing payment status from PayOS for order {orderCode}");

                // 1. Lấy payment info từ database
                var payment = await _unitOfWork.PaymentInfors
                    .GetFirstOrDefaultAsync(p => p.OrderCode == orderCode);

                if (payment == null)
                {
                    _logger.LogWarning($"Payment not found in database for order {orderCode}");
                    return false;
                }

                // 2. Gọi PayOS API để lấy status mới nhất
                var payosStatus = await CallPayOSGetPaymentStatusAsync(orderCode);

                if (payosStatus == null || payosStatus.code != "00")
                {
                    _logger.LogWarning($"Failed to get payment status from PayOS for order {orderCode}");
                    return false;
                }

                var payosData = payosStatus.data;

                // 3. Cập nhật status từ PayOS nếu khác
                bool updated = false;

                if (payment.Status != payosData.status)
                {
                    payment.Status = payosData.status;
                    updated = true;
                    _logger.LogInformation($"Updated payment status for order {orderCode}: {payosData.status}");
                }

                // 4. Cập nhật thông tin transaction nếu đã thanh toán
                if (payosData.status == "PAID" && payosData.transactions?.Any() == true)
                {
                    var transaction = payosData.transactions.First();

                    if (payment.PaidAt == null)
                    {
                        payment.PaidAt = DateTime.UtcNow;
                        updated = true;
                    }

                    if (string.IsNullOrEmpty(payment.TransactionId))
                    {
                        payment.TransactionId = transaction.reference;
                        updated = true;
                    }

                    if (string.IsNullOrEmpty(payment.CounterAccountBankId))
                    {
                        payment.CounterAccountBankId = transaction.counterAccountBankId;
                        payment.CounterAccountBankName = transaction.counterAccountBankName;
                        payment.CounterAccountName = transaction.counterAccountName;
                        payment.CounterAccountNumber = transaction.counterAccountNumber;
                        updated = true;
                    }
                }

                // 5. Lưu nếu có thay đổi
                if (updated)
                {
                    await _unitOfWork.PaymentInfors.UpdateAsync(payment);
                    await _unitOfWork.SaveChangesAsync();
                    _logger.LogInformation($"Payment info updated for order {orderCode}");
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error syncing payment status from PayOS for order {orderCode}");
                return false;
            }
        }

        public async Task<bool> SendEmailInvoice(string to, string subject, string htmlContent, IFormFile file)
        {
            await _emailServices.SendEmailWithAttachFileAsync(to, subject, htmlContent, file);
            return true;
        }

        #endregion
    }
}
