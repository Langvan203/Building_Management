using BuildingManagement.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Services
{
    public interface IDichVuService
    {
        Task<List<GetDSDichVu>> GetDSDichVu();
        Task<DichVuDto> CreateNewDichVu(CreateDichVuDto dto, string name);
        Task<IEnumerable<DichVuDto>> GetDVByMaLDV(int MaLDV);
        Task<bool> RemoveDichVu(int MaDV);
    }
}
