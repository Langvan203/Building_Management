using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs
{
    public class CreatePaymentRequest
    {
        [Required]
        public int MaHD { get; set; }

        [Required]
        [Range(1000, double.MaxValue, ErrorMessage = "Số tiền phải lớn hơn 1,000 VNĐ")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Url]
        public string? ReturnUrl { get; set; }

        [Url]
        public string? CancelUrl { get; set; }

        public int ExpiredAt { get; set; } = 15; // Hết hạn sau 15 phút
    }
}
