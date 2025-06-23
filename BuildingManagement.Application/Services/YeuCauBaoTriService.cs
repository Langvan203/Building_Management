using BuildingManagement.Application.DTOs;
using BuildingManagement.Application.Interfaces.Repositories;
using BuildingManagement.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Services
{
    public class YeuCauBaoTriService : IYeuCauBaoTriService
    {
        private readonly IUnitOfWork _unitOfWork;

        public YeuCauBaoTriService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<PagedResult<YeuCauSuaChuaDTO>> GetDSYeuCauSuaChua(int pageNumber, int pageSize = 10)
        {
            var dsYeuCau = _unitOfWork.YeuCauBaoTris.GetDSYeuCauSuaChua(pageNumber, pageSize);
            if (dsYeuCau == null)
            {
                throw new Exception("Không tìm thấy dữ liệu yêu cầu sửa chữa.");
            }
            return dsYeuCau;
        }
    }
}
