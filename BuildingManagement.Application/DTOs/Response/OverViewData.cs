using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs.Response
{
    public class OverViewData
    {
        public List<RevenueByQuarter> RevenueByQuarter { get; set; }
        public List<BuildingStatus> BuildingStatus { get; set; }
        public List<ServiceDistribution> ServiceDistribution { get; set; }
        public List<RecentTransactions> RecentTransactions { get; set; }
        public List<IssueByPriority> IssueByPriority { get; set; }
    }

    public class RevenueByQuarter
    {
        public string Quarter { get; set; }
        public decimal Revenue { get; set; }
        public decimal Target { get; set; }
    }

    public class BuildingStatus
    {
        public string Name { get; set; }
        public decimal Occupancy { get; set; }
        public decimal Maintaince { get; set; }

    }

    public class ServiceDistribution
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
    }

    public class RecentTransactions
    {
        public string ID { get; set; }
        public string Resident { get; set; }
        public string Apartment { get; set; }
        public decimal Amount { get; set; }
        public string type { get; set; }
        public DateTime date { get; set; }
    }

    public class IssueByPriority
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
