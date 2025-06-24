using BuildingManagement.Application.DTOs;
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
    public class NKBTTrangThaiBaoTriRepository : Repository<nkbtTrangThai>, INKBTTrangThaiBaoTriRepository
    {
        public NKBTTrangThaiBaoTriRepository(BuildingManagementDbContext context) : base(context)
        {
        }

        public async Task<List<TrangThaiBaoTriDto>> GetDSTrangThai()
        {
            var dsTrangThai = await _context.nkbtTrangThais
                .Select(tt => new TrangThaiBaoTriDto
                {
                    MaTrangThai = tt.MaTrangThai,
                    TenTrangThai = tt.TenTrangThai
                }).ToListAsync();
            return dsTrangThai;
        }

        public async Task<List<TrangThaiBaoTriDto>> GetDSTrangThaiYeuCau()
        {
            var dsTrangThai = await _context.tnycTrangThais
                .Select(tt => new TrangThaiBaoTriDto
                {
                    MaTrangThai = tt.IdTrangThai,
                    TenTrangThai = tt.TenTrangThai
                }).ToListAsync();
            return dsTrangThai;
        }
    }
    
}
