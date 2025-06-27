using BuildingManagement.Application.DTOs;
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
    public interface IDichVuHoaDonService
    {
        Task<IEnumerable<RevenueSummaryResponseDto>> GetRevenueSummariesAsync();
        Task<List<RevenueSummaryOverview>> GetRevenueSummariesOverviewAsync();
        Task<PagedResult<GetDSHoaDon>> GetDSHoaDon(int pageNumber, DateTime NgayBatDau, DateTime NgayKetThuc, int pageSize = 15);
        Task<PagedResult<GetDSHoaDon>> GetDSHoaDonByID(int id, int pageNumber, DateTime NgayBatDau, DateTime NgayKetThuc, int pageSize = 15);
        Task<dvHoaDon> GetHoaDonByID(int MaHoaDon);
        Task<IEnumerable<dvHoaDon>> GetDSHoaDonByMaKH(int MaKH);
        Task<bool> CapNhatTrangThaiThanhToan(int MaHoaDon, bool TrangThaiThanhToan);
        Task<bool> CapNhatDanhSachHoaDonTrangThaiThanhToan(IEnumerable<dvHoaDon> hoaDOn, bool TrangThaiThanhToan);

    }
}
