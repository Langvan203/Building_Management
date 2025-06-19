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
        Task<IEnumerable<DongHoDTO>> GetDSDongHo();
        Task<CreateDongHoDto> CreateNewDongHo(CreateDongHoDto dto, string name);
        Task<DongHoDTO> GetDongHoNuocByMaMB(int MaMB);
    }
}
