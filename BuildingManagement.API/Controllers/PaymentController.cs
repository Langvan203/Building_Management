using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using BuildingManagement.Application.DTOs;
using BuildingManagement.Application.Interfaces.Services;
using BuildingManagement.Application.Interfaces;
using BuildingManagement.Application;
using BuildingManagement.Domain.Entities;

namespace BuildingManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly INotificationService _notificationService;
        private readonly IHubContext<PaymentNotificationHub> _hubContext;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(
            IPaymentService paymentService,
            INotificationService notificationService,
            IHubContext<PaymentNotificationHub> hubContext,
            ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _notificationService = notificationService;
            _hubContext = hubContext;
            _logger = logger;
        }

        /// <summary>
        /// Tạo liên kết thanh toán mới
        /// </summary>
        [HttpPost("CreatePaymentLink")]
        public async Task<IActionResult> CreatePaymentLink([FromBody] CreatePaymentRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _paymentService.CreatePaymentLinkAsync(request);

                _logger.LogInformation($"Payment link created for invoice {request.MaHD} by user {GetCurrentUserId()}");

                return Ok(new
                {
                    success = true,
                    data = result,
                    message = "Tạo liên kết thanh toán thành công"
                });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid argument in CreatePaymentLink");
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Invalid operation in CreatePaymentLink");
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating payment link");
                return StatusCode(500, new { success = false, message = "Lỗi hệ thống khi tạo liên kết thanh toán" });
            }
        }

        /// <summary>
        /// Kiểm tra trạng thái thanh toán
        /// </summary>
        [HttpGet("CheckPaymentStatus/{orderCode}")]
        public async Task<IActionResult> CheckPaymentStatus(string orderCode)
        {
            try
            {
                var result = await _paymentService.GetPaymentStatusAsync(orderCode);

                return Ok(new
                {
                    success = true,
                    data = result
                });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, $"Payment not found: {orderCode}");
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error checking payment status for {orderCode}");
                return StatusCode(500, new { success = false, message = "Lỗi hệ thống khi kiểm tra trạng thái thanh toán" });
            }
        }

        /// <summary>
        /// Lấy lịch sử thanh toán của hóa đơn
        /// </summary>
        [HttpGet("GetPaymentHistory/{maHD}")]
        public async Task<IActionResult> GetPaymentHistory(int maHD)
        {
            try
            {
                var result = await _paymentService.GetPaymentHistoryAsync(maHD);

                return Ok(new
                {
                    success = true,
                    data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting payment history for invoice {maHD}");
                return StatusCode(500, new { success = false, message = "Lỗi hệ thống khi lấy lịch sử thanh toán" });
            }
        }

        /// <summary>
        /// Hủy thanh toán
        /// </summary>
        [HttpPut("CancelPayment/{orderCode}")]
        public async Task<IActionResult> CancelPayment(string orderCode)
        {
            try
            {
                var result = await _paymentService.CancelPaymentAsync(orderCode);

                if (!result)
                    return BadRequest(new { success = false, message = "Không thể hủy thanh toán" });

                // Send notification about cancellation
                await _hubContext.SendPaymentStatusUpdateAsync(orderCode, "CANCELLED", GetCurrentUserId());

                _logger.LogInformation($"Payment {orderCode} cancelled by user {GetCurrentUserId()}");

                return Ok(new
                {
                    success = true,
                    message = "Hủy thanh toán thành công"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error cancelling payment {orderCode}");
                return StatusCode(500, new { success = false, message = "Lỗi hệ thống khi hủy thanh toán" });
            }
        }

        /// <summary>
        /// Lấy danh sách thông báo
        /// </summary>
        [HttpGet("GetNotifications")]
        public async Task<IActionResult> GetNotifications([FromQuery] int limit = 50)
        {
            try
            {
                var userId = GetCurrentUserId();
                var notifications = await _notificationService.GetNotificationsAsync(userId, limit);

                return Ok(new
                {
                    success = true,
                    data = notifications
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting notifications");
                return StatusCode(500, new { success = false, message = "Lỗi hệ thống khi lấy thông báo" });
            }
        }

        /// <summary>
        /// Lấy số lượng thông báo chưa đọc
        /// </summary>
        [HttpGet("GetUnreadCount")]
        public async Task<IActionResult> GetUnreadCount()
        {
            try
            {
                var userId = GetCurrentUserId();
                var count = await _notificationService.GetUnreadCountAsync(userId);

                return Ok(new
                {
                    success = true,
                    count = count
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting unread count");
                return StatusCode(500, new { success = false, message = "Lỗi hệ thống" });
            }
        }

        /// <summary>
        /// Đánh dấu thông báo đã đọc
        /// </summary>
        [HttpPut("MarkNotificationAsRead/{notificationId}")]
        public async Task<IActionResult> MarkNotificationAsRead(string notificationId)
        {
            try
            {
                var result = await _notificationService.MarkAsReadAsync(notificationId);

                if (!result)
                    return NotFound(new { success = false, message = "Không tìm thấy thông báo" });

                return Ok(new
                {
                    success = true,
                    message = "Đánh dấu đã đọc thành công"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error marking notification as read: {notificationId}");
                return StatusCode(500, new { success = false, message = "Lỗi hệ thống" });
            }
        }

        /// <summary>
        /// Đánh dấu tất cả thông báo đã đọc
        /// </summary>
        [HttpPut("MarkAllNotificationsAsRead")]
        public async Task<IActionResult> MarkAllNotificationsAsRead()
        {
            try
            {
                var userId = GetCurrentUserId();
                var result = await _notificationService.MarkAllAsReadAsync(userId);

                if (!result)
                    return BadRequest(new { success = false, message = "Không thể đánh dấu tất cả thông báo" });

                return Ok(new
                {
                    success = true,
                    message = "Đánh dấu tất cả thông báo đã đọc thành công"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking all notifications as read");
                return StatusCode(500, new { success = false, message = "Lỗi hệ thống" });
            }
        }

        /// <summary>
        /// Xóa thông báo
        /// </summary>
        [HttpDelete("DeleteNotification/{notificationId}")]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> DeleteNotification(string notificationId)
        {
            try
            {
                var result = await _notificationService.DeleteNotificationAsync(notificationId);

                if (!result)
                    return NotFound(new { success = false, message = "Không tìm thấy thông báo" });

                return Ok(new
                {
                    success = true,
                    message = "Xóa thông báo thành công"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting notification: {notificationId}");
                return StatusCode(500, new { success = false, message = "Lỗi hệ thống" });
            }
        }

        /// <summary>
        /// Test connection - chỉ dành cho development
        /// </summary>
        [HttpGet("TestConnection")]
        [AllowAnonymous]
        public IActionResult TestConnection()
        {
            return Ok(new
            {
                success = true,
                message = "Payment API is working",
                timestamp = DateTime.UtcNow,
                environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            });
        }

        /// <summary>
        /// Gửi thông báo test - chỉ dành cho admin
        /// </summary>
        [HttpPost("SendTestNotification")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> SendTestNotification([FromBody] string message)
        {
            try
            {
                var testNotification = new PaymentNotification
                {
                    Id = Guid.NewGuid().ToString(),
                    MaHD = 9999,
                    OrderCode = "TEST_" + DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    Amount = 100000,
                    CustomerName = "Test Customer",
                    Status = "PAID",
                    PaymentTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                    TransactionId = "TEST_TRANSACTION_" + Guid.NewGuid().ToString("N")[..8],
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow
                };

                await _notificationService.CreateNotificationAsync(testNotification);
                await _hubContext.SendPaymentNotificationAsync(testNotification);

                return Ok(new
                {
                    success = true,
                    message = "Test notification sent successfully",
                    notification = testNotification
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending test notification");
                return StatusCode(500, new { success = false, message = "Lỗi khi gửi thông báo test" });
            }
        }
        [HttpPost("ProcessCompletedPayment/{orderCode}")]
        public async Task<IActionResult> ProcessCompletedPayment(string orderCode)
        {
            try
            {
                _logger.LogInformation($"Processing completed payment for order {orderCode} by user {GetCurrentUserId()}");

                // 1. Xử lý thanh toán
                var result = await _paymentService.ProcessCompletedPaymentAsync(orderCode);

                if (!result)
                {
                    _logger.LogWarning($"Failed to process completed payment for order {orderCode}");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Không thể xử lý thanh toán hoặc thanh toán chưa hoàn thành"
                    });
                }

                // 2. Lấy thông tin payment để gửi SignalR
                var paymentInfo = await _paymentService.GetPaymentByOrderCodeAsync(orderCode);
                if (paymentInfo != null)
                {
                    // 3. Gửi thông báo qua SignalR
                    await _hubContext.SendPaymentStatusUpdateAsync(
                        orderCode,
                        "PAID",
                        GetCurrentUserId()
                    );

                    // 4. Gửi thông báo cập nhật hóa đơn
                    await _hubContext.SendInvoiceStatusUpdateAsync(
                        paymentInfo.MaHD,
                        true,
                        GetCurrentUserId()
                    );

                    _logger.LogInformation($"Payment {orderCode} processed successfully, notifications sent");
                }

                return Ok(new
                {
                    success = true,
                    message = "Xử lý thanh toán thành công",
                    data = new
                    {
                        orderCode = orderCode,
                        processedAt = DateTime.UtcNow
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing completed payment {orderCode}");
                return StatusCode(500, new
                {
                    success = false,
                    message = "Lỗi hệ thống khi xử lý thanh toán"
                });
            }
        }
        [HttpPost("SyncPaymentStatus/{orderCode}")]
        public async Task<IActionResult> SyncPaymentStatus(string orderCode)
        {
            try
            {
                _logger.LogInformation($"Syncing payment status for order {orderCode} by user {GetCurrentUserId()}");

                var result = await _paymentService.SyncPaymentStatusFromPayOSAsync(orderCode);

                if (!result)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Không thể đồng bộ trạng thái thanh toán"
                    });
                }

                // Lấy trạng thái mới
                var status = await _paymentService.GetPaymentStatusAsync(orderCode);

                return Ok(new
                {
                    success = true,
                    message = "Đồng bộ trạng thái thành công",
                    data = status
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error syncing payment status for order {orderCode}");
                return StatusCode(500, new
                {
                    success = false,
                    message = "Lỗi hệ thống khi đồng bộ trạng thái"
                });
            }
        }
        [HttpPost("ManualProcessPayment")]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> ManualProcessPayment([FromBody] ManualProcessPaymentRequest request)
        {
            try
            {
                _logger.LogInformation($"Manual processing payment for order {request.OrderCode} by admin {GetCurrentUserId()}");

                // 1. Kiểm tra quyền admin
                if (!IsCurrentUserAdmin())
                {
                    return Forbid("Chỉ admin mới có thể xử lý thanh toán thủ công");
                }

                // 2. Đồng bộ trạng thái từ PayOS trước
                var syncResult = await _paymentService.SyncPaymentStatusFromPayOSAsync(request.OrderCode);
                if (!syncResult)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Không thể đồng bộ trạng thái từ PayOS"
                    });
                }

                // 3. Xử lý thanh toán
                var processResult = await _paymentService.ProcessCompletedPaymentAsync(request.OrderCode);
                if (!processResult)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Không thể xử lý thanh toán. Kiểm tra lại trạng thái thanh toán."
                    });
                }

                // 4. Ghi log cho audit
                _logger.LogWarning($"Manual payment processing executed by admin {GetCurrentUserName()} for order {request.OrderCode}");

                return Ok(new
                {
                    success = true,
                    message = "Xử lý thanh toán thủ công thành công",
                    processedBy = GetCurrentUserName(),
                    processedAt = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in manual payment processing for order {request.OrderCode}");
                return StatusCode(500, new
                {
                    success = false,
                    message = "Lỗi hệ thống khi xử lý thanh toán thủ công"
                });
            }
        }
        [HttpGet("GetPendingPayments")]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> GetPendingPayments([FromQuery] int limit = 50)
        {
            try
            {
                // Lấy danh sách payments có status PAID nhưng hóa đơn chưa được cập nhật
                // Implementation phụ thuộc vào cấu trúc database

                return Ok(new
                {
                    success = true,
                    message = "Chức năng đang phát triển",
                    data = new List<object>()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting pending payments");
                return StatusCode(500, new
                {
                    success = false,
                    message = "Lỗi hệ thống"
                });
            }
        }

        [HttpPost]
        [Route("SendEmailInvoices")]
        public async Task<IActionResult> SendEmails([FromForm] string to, [FromForm] string subject,
            [FromForm] string body,
            [FromForm] IFormFile attachment)
        {
            try
            {
                if (string.IsNullOrEmpty(to) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(body))
                {
                    return BadRequest(new { success = false, message = "Thông tin email không đầy đủ" });
                }
                var result = await _paymentService.SendEmailInvoice(to, subject, body, attachment);
                if (!result)
                {
                    return StatusCode(500, new { success = false, message = "Lỗi khi gửi email" });
                }
                return Ok(new { success = true, message = "Email đã được gửi thành công" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email invoices");
                return StatusCode(500, new { success = false, message = "Lỗi hệ thống khi gửi email" });
            }
        }
        /// <summary>

        #region Private Methods

        private string GetCurrentUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
        }

        private string GetCurrentUserName()
        {
            return User.FindFirst(ClaimTypes.Name)?.Value ?? "";
        }

        private bool IsCurrentUserAdmin()
        {
            return User.IsInRole("Administrator") || User.IsInRole("Manager");
        }
        public class ManualProcessPaymentRequest
        {
            public string OrderCode { get; set; } = string.Empty;
            public string Reason { get; set; } = string.Empty;
        }
        #endregion
    }
}