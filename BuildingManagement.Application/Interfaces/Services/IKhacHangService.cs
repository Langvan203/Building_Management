using BuildingManagement.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Services
{
    public interface IKhacHangService
    {
        Task<IEnumerable<KhachHangDto>> GetDSKhachHang();
        Task<KhachHangDto> CreateNewKhachHang(CreateKhachHangDto dto, string name);
    }
}
