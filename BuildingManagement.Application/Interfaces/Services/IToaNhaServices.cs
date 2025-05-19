using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.DTOs.Response;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Services
{
    public interface IToaNhaServices
    {
        Task<List<ToaNhaDto>> GetDSToaNhaAsync();
        Task<ToaNhaDto> GetToaNhaTheoIdAsync(int id);
        Task<ToaNhaDto> TaoToaNhaAsync(CreateToaNhaDto dto, string tennv);
        Task<ToaNhaDto> UpdateToaNha(UpdateToaNhaDto dto,string tennv);
        Task<bool> XoaToaNhaAsync(int id);
        Task<SummaryTotalBuildingResponseDtoWithCompare> SummaryTotalBuildingAsync();
        Task<IEnumerable<OccupancyRate>> GetOccupancyRateAsync();
        Task<BuildingDataOverView> BuildingsData(DateTime from, DateTime to);
        Task<FinnancesData> GetFinnancesData(DateTime from, DateTime to);
        Task<ServicesData> GetServicesData(DateTime from, DateTime to);
        Task<OverViewData> GetOverViewData(int year);


    }
}
