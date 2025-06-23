using BuildingManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Services
{
    public interface INKBTKeHoachBaoTriService
    {
        Task<CreateKeHoachBaoTriDto> CreateKeHoachBaoTri(CreateKeHoachBaoTriDto dto, string name);
        Task<PagedResult<KeHoachBaoTriDto>> GetDSKeHoachBaoTri(int pageNumber, int pageSize = 15);
        Task<bool> GiaoViecChoNhanVien(GiaoViecChoNhanVien dto, string Name);
        Task<bool> BatDauKeHoach(int MaKeHoach, string Name);
        Task<bool> HoanThanhKeHoach(int MaKeHoach, string Name);
        Task<bool> HuyKeHoach(int MaKeHoach, string Name);
        Task<bool> XoaKeHoach(int MaKeHoach);
    }
}
