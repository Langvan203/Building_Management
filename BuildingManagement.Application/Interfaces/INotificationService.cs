using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces
{
    public interface INotificationService
    {
        Task<PaymentNotification> CreateNotificationAsync(PaymentNotification notification);
        Task<List<PaymentNotification>> GetNotificationsAsync(string? userId = null, int limit = 50);
        Task<List<PaymentNotification>> GetUnreadNotificationsAsync(string? userId = null);
        Task<bool> MarkAsReadAsync(string notificationId);
        Task<bool> MarkAllAsReadAsync(string? userId = null);
        Task<int> GetUnreadCountAsync(string? userId = null);
        Task<bool> DeleteNotificationAsync(string notificationId);
        Task CleanupOldNotificationsAsync(int daysToKeep = 30);
    }
}
