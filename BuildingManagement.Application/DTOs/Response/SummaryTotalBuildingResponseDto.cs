using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs.Response
{
    public class SummaryTotalBuildingResponseDto
    {
        public int TotalBuilding { get; set; }
        public int TotalPremises { get; set; }
        public int TotalResidents { get; set; }
        public decimal TotalMonthlyRevenue { get; set; }
        public decimal OccupancyRate { get; set; }
        public int TotalServices { get; set; }
        public int TotalOccupied { get; set; }

    }

    public class CompareWithLastMonth
    {
        public int LastMonthTotalBuilding { get; set; }
        public int LastMonthTotalPremises { get; set; }
        public int LastMonthTotalResidents { get; set; }
        public decimal RateLastMonthRevenue { get; set; }
        public decimal RateLastMonthOccupancy { get; set; }
        public int LastMonthTotalServices { get; set; }
    }

    public class SummaryTotalBuildingResponseDtoWithCompare
    {
        public SummaryTotalBuildingResponseDto CurrentMonth { get; set; }
        public CompareWithLastMonth CompareWithLastMonth { get; set; }
    }

    public class OccupancyRate
    {
        public string buildingName { get; set; }
        public decimal Rate { get; set; }
    }
}
