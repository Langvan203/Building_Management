using BuildingManagement.Application.DTOs;
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
        Task<List<DinhMucDTO>> GetDSDinhMucDien();
        Task<CreateDinhMuc> CreateNewDinhMuc(CreateDinhMuc dto, string name);
        Task<bool> UpdateDienDinhMuc(DinhMucDTO dto, string name);
        Task<bool> RemoveDienDinhMuc(int MaDM);
    }
}
