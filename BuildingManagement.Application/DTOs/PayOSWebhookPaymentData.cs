using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs
{
    public class PayOSWebhookPaymentData
    {
        public string OrderCode { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string TransactionDateTime { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
        public string CounterAccountBankId { get; set; } = string.Empty;
        public string CounterAccountBankName { get; set; } = string.Empty;
        public string CounterAccountName { get; set; } = string.Empty;
        public string CounterAccountNumber { get; set; } = string.Empty;
        public string VirtualAccountName { get; set; } = string.Empty;
        public string VirtualAccountNumber { get; set; } = string.Empty;
    }
}
