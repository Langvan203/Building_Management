using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.DTOs.Request.AuthDto;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Repositories
{
    public interface IKhachHangRepository : IRepository<tnKhachHang>
    {
        Task<List<KhachHangFilter>> GetDSKhachHang();
        Task<KhachHangDto> GetKhachHangInfo(LoginDto dto);
    }
}
