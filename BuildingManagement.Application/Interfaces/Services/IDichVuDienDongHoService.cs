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
        Task<PagedResult<DongHoDTO>> GetDSDienDongHo(int pageNumber);
        Task<CreateDongHoDto> CreateDienDongHo(CreateDongHoDto dto, string name);
        Task<bool> UpdateDienDongHo(UpdateDongHoDto dto, string name);
        Task<bool> RemoveDienDongHo(int MaDH);
    }
}
