using BuildingManagement.Application.DTOs;
using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.DTOs.Response;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Repositories
{
    public interface IDichVuHoaDonRepository : IRepository<dvHoaDon>
    {
        Task<IEnumerable<RevenueSummaryResponseDto>> GetRevenueSummariesAsync();
        Task<List<RevenueSummaryOverview>> GetRevenueSummariesOverviewAsync();
        Task<bool> CheckTonTaiDichVuSuDung(int MaDVSD);
        Task<PagedResult<GetDSHoaDon>> GetDSHoaDon(int pageNumber, DateTime NgayBatDau, DateTime NgayKetThu, int pageSize = 15);
        Task<dvHoaDon> GetHoaDonByID(int MaHoaDon);
    }
}
