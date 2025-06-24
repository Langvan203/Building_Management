using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs.Response
{
    public class RevenueSummaryResponseDto
    {
        public string ServiceName { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class RevenueSummaryOverview
    {
        public string Month { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
