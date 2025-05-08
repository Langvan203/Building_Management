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
        Task<IEnumerable<DichVuNuocDongHoDto>> GetDSDongHo();
        Task<DichVuNuocDongHoDto> CreateNewDongHo(CreateDichVuNuocDongHoDto dto, string name);
        Task<DichVuNuocDongHoDto> GetDongHoNuocByMaMB(int MaMB);
    }
}
