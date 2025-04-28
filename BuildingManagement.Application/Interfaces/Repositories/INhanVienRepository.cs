using BuildingManagement.Application.DTOs.Request.AuthDto;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Repositories
{
    public interface INhanVienRepository : IRepository<tnNhanVien>
    {
        Task<tnNhanVien> ThongTinNhanVien(LoginDto loginDto);
    }
}
