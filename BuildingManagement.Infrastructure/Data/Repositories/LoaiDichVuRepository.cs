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
    public class LoaiDichVuRepository : Repository<dvLoaiDV>, IDichVuLoaiDichVuRepository
    {
        public LoaiDichVuRepository(BuildingManagementDbContext dbContext) : base(dbContext)
        {
            
        }

        public async Task<List<GetDSLoaiDichVu>> GetDSLoaiDichVu()
        {
            var dsDichVu = await _context.dvLoaiDVs.Select(dv => new GetDSLoaiDichVu
            {
                id = dv.MaLDV,
                name = dv.TenLDV,
                description = dv.MoTa,
                icon = dv.Icon ?? string.Empty,
                isEssential = dv.IsEssential,
                servicesCount = dv.dvDichVus.Count()
            }).ToListAsync();
            return dsDichVu;
        }

        public async Task<List<GetDSLoaiDichVu>> GetDSLoaiDichVuByMaTN(int MaTN)
        {
            var dsDichVu = await _context.dvLoaiDVs.Where(x => x.MaTN == MaTN).Select(dv => new GetDSLoaiDichVu
            {
                id = dv.MaLDV,
                name = dv.TenLDV,
                description = dv.MoTa,
                icon = dv.Icon ?? string.Empty,
                isEssential = dv.IsEssential,
                servicesCount = dv.dvDichVus.Count()
            }).ToListAsync();
            return dsDichVu;
        }
    }
    
}
