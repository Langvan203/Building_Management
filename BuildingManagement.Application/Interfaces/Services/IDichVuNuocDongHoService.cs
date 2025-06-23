using BuildingManagement.Application.DTOs;
using BuildingManagement.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Services
{
    public interface IDichVuNuocDongHoService
    {
        Task<PagedResult<DongHoDTO>> GetDSNuocDongHo(int pageNumber);
        Task<CreateDongHoDto> CreateNuocDongHo(CreateDongHoDto dto, string name);
        Task<bool> UpdateNuocDongHo(UpdateDongHoDto dto, string name);
        Task<bool> RemoveNuocDongHo(int MaDH);
        Task<bool> GhiChiSoMoi(int MaDH, int ChiSoMoi, string name);
        Task<bool> UpdateTrangThai(int MaDH, bool TrangThai, string Name);
    }
}
