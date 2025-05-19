using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Services.Ultility
{
    public class OTPConfiguration
    {
        public string Secret { get; set; }
        public int ExpireMinutes { get; set; }
    }
}
