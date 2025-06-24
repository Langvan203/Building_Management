using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs
{
    public class PayOSCreatePaymentRequest
    {
        public string orderCode { get; set; } = string.Empty;
        public decimal amount { get; set; }
        public string description { get; set; } = string.Empty;
        public string returnUrl { get; set; } = string.Empty;
        public string cancelUrl { get; set; } = string.Empty;
        public string signature { get; set; } = string.Empty;
        public List<PayOSItem> items { get; set; } = new();
        public string buyerName { get; set; } = string.Empty;
        public string buyerEmail { get; set; } = string.Empty;
        public string buyerPhone { get; set; } = string.Empty;
        public string buyerAddress { get; set; } = string.Empty;
        public int expiredAt { get; set; }
    }
}
