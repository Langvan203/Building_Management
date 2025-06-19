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
    public class DichVuNuocDinhMucRepository : Repository<dvNuocDinhMuc>, IDichVuNuocDinhMucRepository
    {
        public DichVuNuocDinhMucRepository(BuildingManagementDbContext context) : base(context)
        {
        }

        public async Task<dvNuocDinhMuc> CheckByID(int MaDM)
        {
            var nuocDinhMucCheck = await _context.dvNuocDinhMucs.Where(x => x.MaDM == MaDM).FirstOrDefaultAsync();
            if (nuocDinhMucCheck != null)
                return nuocDinhMucCheck;
            return null;
        }

        public async Task<dvNuocDinhMuc> CheckDinhMuc(CreateDinhMuc dto)
        {
            var dinhMucCheck = await _context.dvNuocDinhMucs.Where(x => x.TenDM == dto.TenDM && x.ChiSoDau == dto.ChiSoDau
                                                                    && x.ChiSoCuoi == dto.ChiSoCuoi)
                                                            .FirstOrDefaultAsync();
            if (dinhMucCheck != null)
                return dinhMucCheck;
            return null;
        }

        public async Task<List<DinhMucDTO>> GetDSNuocDinhMuc()
        {
            var dsDienDinhMuc = await _context.dvNuocDinhMucs.Select(x => new DinhMucDTO
            {
                MaDM = x.MaDM,
                TenDM = x.TenDM,
                DonGia = x.DonGiaDinhMuc,
                ChiSoDau = x.ChiSoDau,
                ChiSoCuoi = x.ChiSoCuoi,
                Description = x.Description ?? "No description"
            }).ToListAsync();
            return dsDienDinhMuc;
        }
    }
    
}
