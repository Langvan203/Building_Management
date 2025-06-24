using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs
{
    public class PayOSPaymentStatusResponse
    {
        public string code { get; set; } = string.Empty;
        public string desc { get; set; } = string.Empty;
        public PayOSPaymentStatusData data { get; set; } = new();
    }
}
