using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs
{
    public class PayOSItem
    {
        public string name { get; set; } = string.Empty;
        public int quantity { get; set; }
        public decimal price { get; set; }
    }
}
