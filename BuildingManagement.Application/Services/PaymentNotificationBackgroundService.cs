using BuildingManagement.Application.Interfaces.Services;
using BuildingManagement.Application.Interfaces;
using BuildingManagement.Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Services
{
    
    public class PaymentNotificationBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<PaymentNotificationBackgroundService> _logger;

        public PaymentNotificationBackgroundService(
            IServiceProvider serviceProvider,
            ILogger<PaymentNotificationBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
                    var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();
                    var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<PaymentNotificationHub>>();

                    // Cleanup old notifications every hour
                    await notificationService.CleanupOldNotificationsAsync();

                    // Check for expired payments every 5 minutes
                    await CheckExpiredPayments(scope.ServiceProvider, hubContext);

                    _logger.LogInformation("Payment notification background service executed successfully");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in payment notification background service");
                }

                // Run every 5 minutes
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }

        private async Task CheckExpiredPayments(IServiceProvider serviceProvider, IHubContext<PaymentNotificationHub> hubContext)
        {
            try
            {
                var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
                var notificationService = serviceProvider.GetRequiredService<INotificationService>();

                var expiredPayments = await context.PaymentInfos
                    .Include(p => p.HoaDon)
                    .Where(p => p.Status == "PENDING" &&
                                p.ExpiredAt.HasValue &&
                                p.ExpiredAt.Value < DateTime.UtcNow)
                    .ToListAsync();

                foreach (var payment in expiredPayments)
                {
                    // Update payment status to EXPIRED
                    payment.Status = "EXPIRED";

                    // Create notification
                    var notification = new PaymentNotification
                    {
                        Id = Guid.NewGuid().ToString(),
                        MaHD = payment.MaHD,
                        OrderCode = payment.OrderCode,
                        Amount = payment.Amount,
                        CustomerName = payment.HoaDon?.TenKhachHang ?? "Unknown",
                        Status = "EXPIRED",
                        IsRead = false,
                        CreatedAt = DateTime.UtcNow
                    };

                    await notificationService.CreateNotificationAsync(notification);

                    // Send real-time notification
                    await hubContext.SendPaymentNotificationAsync(notification);

                    _logger.LogInformation($"Payment {payment.OrderCode} marked as expired");
                }

                if (expiredPayments.Any())
                {
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking expired payments");
            }
        }
    }
    
}
