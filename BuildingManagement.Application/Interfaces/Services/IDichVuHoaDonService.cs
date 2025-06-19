using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.DTOs.Response;
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
        Task<List<HoaDonDTO>> GetDSHoaDon();
    }
}
