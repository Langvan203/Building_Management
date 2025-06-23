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
    public class NKBTTrangThaiBaoTriService : INKBTTrangThaiBaoTriService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NKBTTrangThaiBaoTriService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TrangThaiBaoTriDto>> GetDSTrangThai()
        {
            var dsTrangThai = await _unitOfWork.TrangThaiBaoTris.GetDSTrangThai();
            if (dsTrangThai == null || !dsTrangThai.Any())
            {
                throw new Exception("Không tìm thấy trạng thái bảo trì.");
            }
            return dsTrangThai;
        }
    }
}
