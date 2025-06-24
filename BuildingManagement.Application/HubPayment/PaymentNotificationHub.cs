using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BuildingManagement.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace BuildingManagement.Application
{
    [Authorize]
    public class PaymentNotificationHub : Hub
    {
        private readonly ILogger<PaymentNotificationHub> _logger;

        public PaymentNotificationHub(ILogger<PaymentNotificationHub> logger)
        {
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = Context.User?.FindFirst(ClaimTypes.Name)?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                // Thêm user vào group của chính họ
                await Groups.AddToGroupAsync(Context.ConnectionId, $"User_{userId}");

                // Thêm vào group chung cho admin
                if (IsAdmin())
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, "Administrators");
                }

                _logger.LogInformation($"User {userName} ({userId}) connected to PaymentNotificationHub");
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = Context.User?.FindFirst(ClaimTypes.Name)?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"User_{userId}");

                if (IsAdmin())
                {
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Administrators");
                }

                _logger.LogInformation($"User {userName} ({userId}) disconnected from PaymentNotificationHub");
            }

            await base.OnDisconnectedAsync(exception);
        }

        // Client có thể gọi để join specific groups
        public async Task JoinPaymentGroup(string paymentId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Payment_{paymentId}");
            _logger.LogInformation($"Connection {Context.ConnectionId} joined payment group {paymentId}");
        }

        public async Task LeavePaymentGroup(string paymentId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Payment_{paymentId}");
            _logger.LogInformation($"Connection {Context.ConnectionId} left payment group {paymentId}");
        }

        // Client gọi để đánh dấu đã đọc thông báo
        public async Task MarkNotificationAsRead(string notificationId)
        {
            // Gửi thông báo đến tất cả clients của user này
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                await Clients.Group($"User_{userId}").SendAsync("NotificationMarkedAsRead", notificationId);
            }
        }

        // Kiểm tra xem user có phải admin không
        private bool IsAdmin()
        {
            return Context.User?.IsInRole("Administrator") == true ||
                   Context.User?.IsInRole("Manager") == true;
        }

        // Method để gửi thông báo test (chỉ admin)
        [Authorize(Roles = "Administrator,Manager")]
        public async Task SendTestNotification(string message)
        {
            await Clients.All.SendAsync("TestNotification", new
            {
                Message = message,
                Timestamp = DateTime.UtcNow,
                From = Context.User?.FindFirst(ClaimTypes.Name)?.Value
            });
        }
    }

    // Extension methods cho Hub
    public static class PaymentNotificationHubExtensions
    {
        public static async Task SendPaymentNotificationAsync(
            this IHubContext<PaymentNotificationHub> hubContext,
            PaymentNotification notification,
            string? specificUserId = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(specificUserId))
                {
                    // Gửi cho user cụ thể
                    await hubContext.Clients.Group($"User_{specificUserId}")
                        .SendAsync("PaymentNotification", notification);
                }
                else
                {
                    // Gửi cho tất cả administrators
                    await hubContext.Clients.Group("Administrators")
                        .SendAsync("PaymentNotification", notification);
                }
            }
            catch (Exception ex)
            {
                // Log error but don't throw to avoid breaking the main flow
                Console.WriteLine($"Error sending SignalR notification: {ex.Message}");
            }
        }

        public static async Task SendPaymentStatusUpdateAsync(
            this IHubContext<PaymentNotificationHub> hubContext,
            string orderCode,
            string newStatus,
            string? userId = null)
        {
            var updateData = new
            {
                OrderCode = orderCode,
                Status = newStatus,
                Timestamp = DateTime.UtcNow
            };

            if (!string.IsNullOrEmpty(userId))
            {
                await hubContext.Clients.Group($"User_{userId}")
                    .SendAsync("PaymentStatusUpdate", updateData);
            }
            else
            {
                await hubContext.Clients.Group("Administrators")
                    .SendAsync("PaymentStatusUpdate", updateData);
            }
        }

        public static async Task SendInvoiceStatusUpdateAsync(
            this IHubContext<PaymentNotificationHub> hubContext,
            int maHD,
            bool isPaid,
            string? userId = null)
        {
            var updateData = new
            {
                MaHD = maHD,
                IsPaid = isPaid,
                Timestamp = DateTime.UtcNow
            };

            if (!string.IsNullOrEmpty(userId))
            {
                await hubContext.Clients.Group($"User_{userId}")
                    .SendAsync("InvoiceStatusUpdate", updateData);
            }
            else
            {
                await hubContext.Clients.Group("Administrators")
                    .SendAsync("InvoiceStatusUpdate", updateData);
            }
        }
    }
}
