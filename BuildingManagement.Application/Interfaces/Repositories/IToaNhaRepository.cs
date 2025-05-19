using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.DTOs.Response;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Repositories
{
    public interface IToaNhaRepository : IRepository<tnToaNha>
    {
        Task<SummaryTotalBuildingResponseDtoWithCompare> SummaryTotalBuildingAsync();
        Task<IEnumerable<OccupancyRate>> GetOccupancyRateAsync();
        Task<BuildingDataOverView> BuildingsData(DateTime from, DateTime to);
        Task<FinnancesData> GetFinnancesDataAsync(DateTime form, DateTime to);
        Task<ServicesData> GetServicesData(DateTime from, DateTime to);
        Task<OverViewData> GetOverViewData(int year);
        Task<List<ToaNhaDto>> GetToaNhaDtoAsync();
    }
}
