using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs
{
    public class CreatePaymentResponse
    {
        public string PaymentLinkId { get; set; } = string.Empty;
        public string OrderCode { get; set; } = string.Empty;
        public string QrCode { get; set; } = string.Empty;
        public string CheckoutUrl { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
