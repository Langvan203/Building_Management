using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class PaymentInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string OrderCode { get; set; } = string.Empty;

        [Required]
        public int MaHD { get; set; }

        [Required]
        public string PaymentLinkId { get; set; } = string.Empty;

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = "PENDING";

        public string? QrCode { get; set; }

        public string? CheckoutUrl { get; set; }

        public string? TransactionId { get; set; }

        public string? BankCode { get; set; }

        public string? CounterAccountBankId { get; set; }

        public string? CounterAccountBankName { get; set; }

        public string? CounterAccountName { get; set; }

        public string? CounterAccountNumber { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? PaidAt { get; set; }

        public DateTime? ExpiredAt { get; set; }
        public DateTime? CancelledAt { get; set; }
        // Navigation property
        [ForeignKey("MaHD")]
        public virtual dvHoaDon? HoaDon { get; set; }
    }
}
