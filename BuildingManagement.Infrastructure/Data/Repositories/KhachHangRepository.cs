using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Repositories;
using BuildingManagement.Domain.Entities;
using BuildingManagement.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Infrastructure.Data.Repositories
{
    public class KhachHangRepository : Repository<tnKhachHang>, IKhachHangRepository
    {
        public KhachHangRepository(BuildingManagementDbContext context) : base(context)
        {
        }

        public async Task<List<KhachHangFilter>> GetDSKhachHang()
        {
            var dsKH = await _context.tnKhachHangs.Select(x => new KhachHangFilter
            {
                Id = x.MaKH,
                Name = x.IsCaNhan == true ? x.HoTen : x.CtyTen,
                Contract = x.DienThoai,
            }).ToListAsync();
            return dsKH;
        }
    }
    
}
