using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs
{
    public class PayOSTransaction
    {
        public string reference { get; set; } = string.Empty;
        public decimal amount { get; set; }
        public string accountNumber { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string transactionDateTime { get; set; } = string.Empty;
        public string virtualAccountName { get; set; } = string.Empty;
        public string virtualAccountNumber { get; set; } = string.Empty;
        public string counterAccountBankId { get; set; } = string.Empty;
        public string counterAccountBankName { get; set; } = string.Empty;
        public string counterAccountName { get; set; } = string.Empty;
        public string counterAccountNumber { get; set; } = string.Empty;
    }
}
