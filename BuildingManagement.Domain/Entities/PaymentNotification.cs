using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class PaymentNotification
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public int MaHD { get; set; }

        [Required]
        public string OrderCode { get; set; } = string.Empty;

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = string.Empty;

        public string? PaymentTime { get; set; }

        public string? TransactionId { get; set; }

        public string? BankCode { get; set; }

        public bool IsRead { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // User ID để gửi thông báo cho đúng người
        public string? UserId { get; set; }
    }
}
