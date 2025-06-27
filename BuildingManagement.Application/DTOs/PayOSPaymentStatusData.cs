using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs
{
    public class PayOSPaymentStatusData
    {
        public string id { get; set; } = string.Empty;
        public long orderCode { get; set; }
        public decimal amount { get; set; }
        public decimal amountPaid { get; set; }
        public decimal amountRemaining { get; set; }
        public string status { get; set; } = string.Empty;
        public DateTime createdAt { get; set; }
        public List<PayOSTransaction> transactions { get; set; } = new();
    }
}
