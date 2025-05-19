using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs.Response
{
    public class ServicesData
    {
        public int TotalRequest { get; set; }
        public int PendingRequest { get; set; }
        public int CompletedRequest { get; set; }
        public decimal CompletionRate { get; set; }
        public decimal SatisfactionRate { get; set; }
        public List<RequestByCategory> RequestsByCategory { get; set; }
        public List<RequestByMonth> RequestsByMonth { get; set; }
        public List<RecentRequest> RecentRequests { get; set; }

    }

    public class RequestByCategory
    {
        public string Name { get; set; }
        public int Total { get; set; }
        public int Peding { get; set; }
        public int Completed { get; set; }
    }

    public class RequestByMonth
    {
        public string Month { get; set; }
        public int Requests { get; set; }
    }

    public class RecentRequest
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Apartment { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
    }
}
