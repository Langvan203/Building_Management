using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs.Response
{
    public class BuildingDataOverView
    {
        public int totalBuildings { get; set; }
        public int totalUnits { get; set; }
        public int occupiedUnits { get; set; }
        public decimal occupancyRate { get; set; }
        public int maintanceIssues { get; set; }
        public decimal totalRevenue { get; set; }
        public decimal totalRevenueGroth { get; set; }
        public int NewCustomers { get; set; }
        public decimal NewCustomersGroth { get; set; }
        public int totalRequest { get; set; }
        public decimal totalRequestGroth { get; set; }
        public decimal TotalCompletedRequest { get; set; }
        public decimal TotalCompletedRequestGroth { get; set; }
        public List<BuildingDetails> buildingDetails { get; set; }
        public List<occupancyBuilding> occupancyBuildings { get; set; }
    }

    public class BuildingDetails
    {
        public string BuildingName { get; set; }
        public int Units { get; set; }
        public int Occupied { get; set; }
        public int vacant { get; set; }
    }

    public class occupancyBuilding
    {
        public string BuildingName { get; set; }
        public decimal value { get; set;}
    }
}