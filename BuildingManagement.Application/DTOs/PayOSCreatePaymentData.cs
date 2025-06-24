using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs
{
    public class PayOSCreatePaymentData
    {
        public string bin { get; set; } = string.Empty;
        public string accountNumber { get; set; } = string.Empty;
        public string accountName { get; set; } = string.Empty;
        public decimal amount { get; set; }
        public string description { get; set; } = string.Empty;
        public string orderCode { get; set; } = string.Empty;
        public string currency { get; set; } = string.Empty;
        public string paymentLinkId { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public string checkoutUrl { get; set; } = string.Empty;
        public string qrCode { get; set; } = string.Empty;
    }
}
