using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.DTOs;
using BuildingManagement.Application.Interfaces.Services;
using BuildingManagement.Application.Interfaces;
using BuildingManagement.Application;
using BuildingManagement.Domain.Entities;
using BuildingManagement.Application.Interfaces.Services.Ultility;
using BuildingManagement.Application.Interfaces.Repositories;
using AutoMapper;

namespace BuildingManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentWebhookController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly INotificationService _notificationService;
        private readonly IEmailService _emailService;
        private readonly IHubContext<PaymentNotificationHub> _hubContext;
        private readonly ILogger<PaymentWebhookController> _logger;
        private readonly IDichVuHoaDonService _hoaDonService;
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;

        public PaymentWebhookController(
            IPaymentService paymentService,
            INotificationService notificationService,
            IEmailService emailService,
            IHubContext<PaymentNotificationHub> hubContext,
            ILogger<PaymentWebhookController> logger,
            IDichVuHoaDonService hoaDonService,
            IUnitOfWork unitOfwork,
            IMapper mapper
            )
        {
            _hoaDonService = hoaDonService;
            _mapper = mapper;
            _paymentService = paymentService;
            _notificationService = notificationService;
            _emailService = emailService;
            _hubContext = hubContext;
            _logger = logger;
            _unitOfwork = unitOfwork;
        }

        /// <summary>
        /// Webhook endpoint cho PayOS
        /// </summary>
        [HttpPost("payos-webhook")]
        public async Task<IActionResult> HandlePayOSWebhook()
        {
            try
            {
                // Đọc raw body
                using var reader = new StreamReader(Request.Body);
                var webhookBody = await reader.ReadToEndAsync();

                if (string.IsNullOrEmpty(webhookBody))
                {
                    _logger.LogWarning("Received empty webhook body");
                    return BadRequest("Empty webhook body");
                }

                // Verify webhook signature
                var signature = Request.Headers["X-Webhook-Signature"].FirstOrDefault();
                if (string.IsNullOrEmpty(signature))
                {
                    _logger.LogWarning("Missing webhook signature");
                    return Unauthorized("Missing signature");
                }

                var isValidSignature = await _paymentService.VerifyWebhookSignatureAsync(webhookBody, signature);
                if (!isValidSignature)
                {
                    _logger.LogWarning("Invalid webhook signature");
                    return Unauthorized("Invalid signature");
                }

                // Parse webhook data
                var webhookData = JsonSerializer.Deserialize<PayOSWebhookData>(webhookBody, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                if (webhookData?.Data == null)
                {
                    _logger.LogWarning("Invalid webhook data format");
                    return BadRequest("Invalid webhook data");
                }

                _logger.LogInformation($"Received PayOS webhook: OrderCode={webhookData.Data.OrderCode}, Status={webhookData.Data.Status}");

                // Process webhook
                await ProcessPaymentWebhook(webhookData);

                return Ok(new { success = true, message = "Webhook processed successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing PayOS webhook");
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }

        /// <summary>
        /// Test webhook endpoint
        /// </summary>
        [HttpPost("test-webhook")]
        public async Task<IActionResult> TestWebhook([FromBody] PayOSWebhookData testData)
        {
            try
            {
                _logger.LogInformation($"Processing test webhook: {JsonSerializer.Serialize(testData)}");

                await ProcessPaymentWebhook(testData);

                return Ok(new { success = true, message = "Test webhook processed successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing test webhook");
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #region Private Methods

        private async Task ProcessPaymentWebhook(PayOSWebhookData webhookData)
        {
            var orderCode = webhookData.Data.OrderCode;

            // Lấy thông tin payment từ database
            var paymentInfo = await _paymentService.GetPaymentByOrderCodeAsync(orderCode);
            if (paymentInfo == null)
            {
                _logger.LogWarning($"Payment not found for order code: {orderCode}");
                throw new ArgumentException($"Payment not found for order code: {orderCode}");
            }

            // Lấy thông tin hóa đơn
            var invoice = await _hoaDonService.GetHoaDonByID(paymentInfo.MaHD);
            var invoiceDto = _mapper.Map<GetDSHoaDon>(invoice);
            if (invoice == null)
            {
                _logger.LogWarning($"Invoice not found for MaHD: {paymentInfo.MaHD}");
                throw new ArgumentException($"Invoice not found for MaHD: {paymentInfo.MaHD}");
            }

            // Tạo notification
            var notification = new PaymentNotification
            {
                Id = Guid.NewGuid().ToString(),
                MaHD = paymentInfo.MaHD,
                OrderCode = orderCode,
                Amount = webhookData.Data.Amount,
                CustomerName = invoice.MaKH.ToString(),
                Status = webhookData.Data.Status,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            // Xử lý theo trạng thái
            switch (webhookData.Data.Status.ToUpper())
            {
                case "PAID":
                    await HandleSuccessfulPayment(webhookData, paymentInfo, invoiceDto, notification);
                    break;

                case "CANCELLED":
                    await HandleCancelledPayment(webhookData, paymentInfo, notification);
                    break;

                case "EXPIRED":
                    await HandleExpiredPayment(webhookData, paymentInfo, notification);
                    break;

                default:
                    _logger.LogWarning($"Unknown payment status: {webhookData.Data.Status}");
                    return;
            }

            // Lưu notification
            await _notificationService.CreateNotificationAsync(notification);

            // Gửi thông báo real-time
            await _hubContext.SendPaymentNotificationAsync(notification);

            _logger.LogInformation($"Webhook processed successfully for order {orderCode}, status: {webhookData.Data.Status}");
        }

        private async Task HandleSuccessfulPayment(
            PayOSWebhookData webhookData,
            PaymentInfo paymentInfo,
            GetDSHoaDon invoice,
            PaymentNotification notification)
        {
            try
            {
                 await _unitOfwork.BeginTransactionAsync();

                // Cập nhật trạng thái payment
                notification.Status = "PAID";
                notification.PaymentTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
                notification.TransactionId = webhookData.Data.Reference;
                notification.BankCode = webhookData.Data.CounterAccountBankId;

                // Cập nhật payment info
                await _paymentService.UpdatePaymentStatusAsync(
                    paymentInfo.OrderCode,
                    "PAID",
                    webhookData.Data.Reference);

                // Cập nhật trạng thái hóa đơn
                await _hoaDonService.CapNhatTrangThaiThanhToan(paymentInfo.MaHD, true);

                // Gửi email xác nhận
                await SendPaymentConfirmationEmail(invoice, webhookData);

                // Gửi thông báo cập nhật hóa đơn
                await _hubContext.SendInvoiceStatusUpdateAsync(paymentInfo.MaHD, true);

                await _unitOfwork.CommitTransactionAsync();

                _logger.LogInformation($"Payment successful for invoice {paymentInfo.MaHD}, order {paymentInfo.OrderCode}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error handling successful payment for order {paymentInfo.OrderCode}");
                throw;
            }
        }

        private async Task HandleCancelledPayment(
            PayOSWebhookData webhookData,
            PaymentInfo paymentInfo,
            PaymentNotification notification)
        {
            try
            {
                notification.Status = "CANCELLED";

                // Cập nhật payment status
                await _paymentService.UpdatePaymentStatusAsync(paymentInfo.OrderCode, "CANCELLED");

                _logger.LogInformation($"Payment cancelled for invoice {paymentInfo.MaHD}, order {paymentInfo.OrderCode}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error handling cancelled payment for order {paymentInfo.OrderCode}");
                throw;
            }
        }

        private async Task HandleExpiredPayment(
            PayOSWebhookData webhookData,
            PaymentInfo paymentInfo,
            PaymentNotification notification)
        {
            try
            {
                notification.Status = "EXPIRED";

                // Cập nhật payment status
                await _paymentService.UpdatePaymentStatusAsync(paymentInfo.OrderCode, "EXPIRED");

                _logger.LogInformation($"Payment expired for invoice {paymentInfo.MaHD}, order {paymentInfo.OrderCode}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error handling expired payment for order {paymentInfo.OrderCode}");
                throw;
            }
        }

        private async Task SendPaymentConfirmationEmail(GetDSHoaDon invoice, PayOSWebhookData webhookData)
        {
            try
            {
                if (string.IsNullOrEmpty(invoice.EmailKhachHang))
                {
                    _logger.LogWarning($"No email address for invoice {invoice.MaHD}");
                    return;
                }

                var emailContent = GeneratePaymentConfirmationEmailContent(invoice, webhookData);
                var subject = $"Xác nhận thanh toán - Hóa đơn HD-{invoice.MaHD.ToString().PadLeft(4, '0')}";

                await _emailService.SendEmailAsync(
                    to: invoice.EmailKhachHang,
                    subject: subject,
                    htmlContent: emailContent
                );

                _logger.LogInformation($"Payment confirmation email sent for invoice {invoice.MaHD}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send confirmation email for invoice {invoice.MaHD}");
                // Don't throw - email failure shouldn't break the payment process
            }
        }

        private string GeneratePaymentConfirmationEmailContent(GetDSHoaDon invoice, PayOSWebhookData webhookData)
        {
            var formatCurrency = (decimal amount) => amount.ToString("N0") + " VNĐ";

            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <title>Xác nhận thanh toán</title>
    <style>
        body {{ 
            font-family: Arial, sans-serif; 
            line-height: 1.6; 
            color: #333; 
            max-width: 600px; 
            margin: 0 auto; 
            padding: 20px; 
        }}
        .header {{ 
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); 
            color: white; 
            padding: 30px 20px; 
            text-align: center; 
            border-radius: 10px 10px 0 0; 
        }}
        .content {{ 
            background: #f8f9fa; 
            padding: 30px; 
            border-radius: 0 0 10px 10px; 
        }}
        .success-badge {{ 
            background: #28a745; 
            color: white; 
            padding: 10px 20px; 
            border-radius: 25px; 
            display: inline-block; 
            margin: 20px 0; 
            font-weight: bold; 
        }}
        .info-table {{ 
            width: 100%; 
            border-collapse: collapse; 
            margin: 20px 0; 
            background: white; 
            border-radius: 8px; 
            overflow: hidden; 
            box-shadow: 0 2px 10px rgba(0,0,0,0.1); 
        }}
        .info-table th, .info-table td {{ 
            padding: 15px; 
            text-align: left; 
            border-bottom: 1px solid #dee2e6; 
        }}
        .info-table th {{ 
            background: #e9ecef; 
            font-weight: bold; 
            width: 40%; 
        }}
        .amount {{ 
            font-size: 24px; 
            font-weight: bold; 
            color: #28a745; 
        }}
        .footer {{ 
            text-align: center; 
            margin-top: 30px; 
            padding: 20px; 
            border-top: 2px solid #dee2e6; 
            color: #6c757d; 
        }}
        .contact-info {{ 
            background: #e3f2fd; 
            padding: 20px; 
            border-radius: 8px; 
            margin: 20px 0; 
        }}
    </style>
</head>
<body>
    <div class='header'>
        <h1>🎉 Thanh toán thành công!</h1>
        <p>Cảm ơn bạn đã thanh toán đúng hạn</p>
    </div>
    
    <div class='content'>
        <div class='success-badge'>
            ✅ Đã xác nhận thanh toán
        </div>
        
        <p>Kính gửi <strong>{invoice.TenKhachHang}</strong>,</p>
        
        <p>Chúng tôi vui mừng thông báo đã nhận được thanh toán của bạn cho hóa đơn dưới đây:</p>
        
        <table class='info-table'>
            <tr>
                <th>Mã hóa đơn</th>
                <td><strong>HD-{invoice.MaHD.ToString().PadLeft(4, '0')}</strong></td>
            </tr>
            <tr>
                <th>Số tiền</th>
                <td class='amount'>{formatCurrency(webhookData.Data.Amount)}</td>
            </tr>
            <tr>
                <th>Thời gian thanh toán</th>
                <td>{DateTime.Now:dd/MM/yyyy HH:mm:ss}</td>
            </tr>
            <tr>
                <th>Mã giao dịch</th>
                <td>{webhookData.Data.Reference}</td>
            </tr>
            <tr>
                <th>Ngân hàng</th>
                <td>{webhookData.Data.CounterAccountBankName}</td>
            </tr>
            <tr>
                <th>Vị trí</th>
                <td>{invoice.TenTN}, {invoice.TenTL}, {invoice.TenKN}</td>
            </tr>
        </table>
        
        <div class='contact-info'>
            <h3>📞 Thông tin liên hệ</h3>
            <p>
                Nếu bạn có bất kỳ thắc mắc nào, vui lòng liên hệ:<br>
                📧 Email: support@toanha.com<br>
                📱 Hotline: (028) 3123 4567<br>
                🕒 Thời gian hỗ trợ: 8:00 - 17:30 (Thứ 2 - Thứ 6)
            </p>
        </div>
        
        <p><strong>Lưu ý quan trọng:</strong></p>
        <ul>
            <li>Vui lòng lưu giữ email này làm bằng chứng thanh toán</li>
            <li>Hóa đơn của bạn đã được cập nhật trạng thái 'Đã thanh toán' trong hệ thống</li>
            <li>Nếu cần hóa đơn VAT, vui lòng liên hệ bộ phận kế toán</li>
        </ul>
    </div>
    
    <div class='footer'>
        <p>Email này được gửi tự động từ hệ thống quản lý tòa nhà</p>
        <p>© 2024 Building Management System. All rights reserved.</p>
    </div>
</body>
</html>";
        }

        #endregion
    }

    // Interface cho Email Service (cần implement)
    
}