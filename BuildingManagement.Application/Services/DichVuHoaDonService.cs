using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.DTOs.Response;
using BuildingManagement.Application.Interfaces.Repositories;
using BuildingManagement.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Services
{
    public class DichVuHoaDonService : IDichVuHoaDonService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DichVuHoaDonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<HoaDonDTO>> GetDSHoaDon()
        {
            var dsHoaDon = await _unitOfWork.HoaDons.GetDSHoaDon();
            return dsHoaDon;
        }

        public Task<IEnumerable<RevenueSummaryResponseDto>> GetRevenueSummariesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<RevenueSummaryOverview>> GetRevenueSummariesOverviewAsync()
        {
            throw new NotImplementedException();
        }
    }
}
