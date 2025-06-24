using BuildingManagement.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Services
{
    public interface ILoaiDichVuService
    {
        Task<List<GetDSLoaiDichVu>> GetDSLoaiDichVu();
        Task<List<GetDSLoaiDichVu>> GetDSLoaiDichVuByMaTN(int MaTN);
        Task<CreateLoaiDVDto> CreateLoaiDichVu(CreateLoaiDVDto dto, string tennv);
        Task<bool> DeleteLoaiDichVu(int MaLDV);
    }
}
