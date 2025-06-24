using BuildingManagement.Application.Interfaces;
using BuildingManagement.Domain.Entities;
using Microsoft.Extensions.Logging;
using BuildingManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingManagement.Application.Interfaces.Repositories;

namespace BuildingManagement.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ILogger<NotificationService> _logger;
        private readonly IUnitOfWork _unitOfwork;

        public NotificationService(
            ILogger<NotificationService> logger, IUnitOfWork unitOfWork)
        {
            _unitOfwork = unitOfWork;
            _logger = logger;
        }

        public async Task<PaymentNotification> CreateNotificationAsync(PaymentNotification notification)
        {
            try
            {
                if (string.IsNullOrEmpty(notification.Id))
                    notification.Id = Guid.NewGuid().ToString();

                notification.CreatedAt = DateTime.UtcNow;

                await _unitOfwork.PaymentNotifications.AddAsync(notification);
                await _unitOfwork.SaveChangesAsync();

                _logger.LogInformation($"Notification created successfully: {notification.Id}");
                return notification;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating notification for invoice {notification.MaHD}");
                throw;
            }
        }

        public async Task<List<PaymentNotification>> GetNotificationsAsync(string? userId = null, int limit = 50)
        {
            try
            {
                var query = _unitOfwork.PaymentNotifications

                if (!string.IsNullOrEmpty(userId))
                {
                    query = query.Where(n => n.UserId == userId || string.IsNullOrEmpty(n.UserId));
                }

                var notifications = await query
                    .OrderByDescending(n => n.CreatedAt)
                    .Take(limit)
                    .ToListAsync();

                return notifications;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting notifications");
                throw;
            }
        }

        public async Task<List<PaymentNotification>> GetUnreadNotificationsAsync(string? userId = null)
        {
            try
            {
                var query = _context.PaymentNotifications
                    .Where(n => !n.IsRead);

                if (!string.IsNullOrEmpty(userId))
                {
                    query = query.Where(n => n.UserId == userId || string.IsNullOrEmpty(n.UserId));
                }

                var notifications = await query
                    .OrderByDescending(n => n.CreatedAt)
                    .ToListAsync();

                return notifications;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting unread notifications");
                throw;
            }
        }

        public async Task<bool> MarkAsReadAsync(string notificationId)
        {
            try
            {
                var notification = await _context.PaymentNotifications
                    .FirstOrDefaultAsync(n => n.Id == notificationId);

                if (notification == null)
                    return false;

                notification.IsRead = true;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error marking notification as read: {notificationId}");
                return false;
            }
        }

        public async Task<bool> MarkAllAsReadAsync(string? userId = null)
        {
            try
            {
                var query = _context.PaymentNotifications
                    .Where(n => !n.IsRead);

                if (!string.IsNullOrEmpty(userId))
                {
                    query = query.Where(n => n.UserId == userId || string.IsNullOrEmpty(n.UserId));
                }

                var notifications = await query.ToListAsync();

                foreach (var notification in notifications)
                {
                    notification.IsRead = true;
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking all notifications as read");
                return false;
            }
        }

        public async Task<int> GetUnreadCountAsync(string? userId = null)
        {
            try
            {
                var query = _context.PaymentNotifications
                    .Where(n => !n.IsRead);

                if (!string.IsNullOrEmpty(userId))
                {
                    query = query.Where(n => n.UserId == userId || string.IsNullOrEmpty(n.UserId));
                }

                return await query.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting unread count");
                return 0;
            }
        }

        public async Task<bool> DeleteNotificationAsync(string notificationId)
        {
            try
            {
                var notification = await _context.PaymentNotifications
                    .FirstOrDefaultAsync(n => n.Id == notificationId);

                if (notification == null)
                    return false;

                _context.PaymentNotifications.Remove(notification);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting notification: {notificationId}");
                return false;
            }
        }

        public async Task CleanupOldNotificationsAsync(int daysToKeep = 30)
        {
            try
            {
                var cutoffDate = DateTime.UtcNow.AddDays(-daysToKeep);

                var oldNotifications = await _context.PaymentNotifications
                    .Where(n => n.CreatedAt < cutoffDate)
                    .ToListAsync();

                if (oldNotifications.Any())
                {
                    _context.PaymentNotifications.RemoveRange(oldNotifications);
                    await _context.SaveChangesAsync();

                    
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cleaning up old notifications");
            }
        }
    }
}

