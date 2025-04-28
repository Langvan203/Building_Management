using BuildingManagement.Application.DTOs.Request.AuthDto;
using BuildingManagement.Application.Interfaces.Repositories;
using BuildingManagement.Domain.Entities;
using BuildingManagement.Infrastructure.Data.Context;
using BuildingManagement.Infrastructure.Ultility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Infrastructure.Data.Repositories
{
    public class NhanVienRepository : Repository<tnNhanVien>, INhanVienRepository
    {
        public NhanVienRepository(BuildingManagementDbContext context) : base(context) 
        {

        }

        public async Task<tnNhanVien> ThongTinNhanVien(LoginDto loginDto)
        {

            var nv = await _context.tnNhanViens.Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Email == loginDto.Email);
            if (nv!=null && HashPassWord.VerifyPassword(nv.PasswordHash, loginDto.Password))
            {
                return nv;
            }
            return null;
        }

    }
}
