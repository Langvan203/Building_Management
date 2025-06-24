using BuildingManagement.Application.DTOs;
using BuildingManagement.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Services
{
    public interface IDichVuNuocDinhMucService
    {
        Task<List<DinhMucDTO>> GetDSDinhMucNuoc();
        Task<CreateDinhMuc> CreateNewDinhMuc(CreateDinhMuc dto, string name);
        Task<bool> UpdateNuocDinhMuc(DinhMucDTO dto, string name);
        Task<bool> RemoveNuocDinhMuc(int MaDM);
    }
}