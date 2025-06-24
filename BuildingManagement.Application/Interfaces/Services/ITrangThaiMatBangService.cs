using BuildingManagement.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Services
{
    public interface ITrangThaiMatBangService
    {
        Task<IEnumerable<TrangThaiMatBangDto>> GetDSTrangThaiMatBang();
        Task<TrangThaiMatBangDto> CreateNewTrangThaiMB(CreateNewTrangThaiMatBangDto dto, string HoTen);
        Task<bool> RemoveTrangThai(int TrangThaiMB);
    }
}
