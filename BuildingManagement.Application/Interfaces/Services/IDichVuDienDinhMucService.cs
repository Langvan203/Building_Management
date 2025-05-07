using BuildingManagement.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Services
{
    public interface IDichVuDienDinhMucService
    {
        Task<IEnumerable<DichVuDienDinhMucDto>> GetDSDinhMucDien();
        Task<DichVuDienDinhMucDto> CreateNewDinhMuc(CreateDichVuDienDinhMucDto dto, string name);
    }
}
