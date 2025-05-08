using BuildingManagement.Application.DTOs;
using BuildingManagement.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Services
{
    public interface IDichVuDienDongHoService
    {
        Task<IEnumerable<DichVuDienDongHoDto>> GetDSDongHo();
        Task<DichVuDienDongHoDto> CreateNewDongHo(CreateDichVuDienDongHoDto dto, string name);
        Task<DichVuDienDongHoDto> GetDongHoDienByMaMB(int MaMB);
    }
}
