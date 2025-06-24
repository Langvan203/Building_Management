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
    public class DichVuDienDinhMucRepository : Repository<dvDienDinhMuc>, IDichVuDienDinhMucRepository
    {
        public DichVuDienDinhMucRepository(BuildingManagementDbContext context) : base(context)
        {

        }

        public async Task<dvDienDinhMuc> CheckByID(int MaDM)
        {
            var dienDinhMucCheck = await _context.dvDienDinhMucs.Where(x => x.MaDM == MaDM).FirstOrDefaultAsync();
            if (dienDinhMucCheck != null)
                return dienDinhMucCheck;
            return null;
        }

        public async Task<dvDienDinhMuc> CheckDinhMuc(CreateDinhMuc dto)
        {
            var dinhMucCheck = await _context.dvDienDinhMucs.Where(x => x.TenDM == dto.TenDM && x.ChiSoDau == dto.ChiSoDau
                                                                    && x.ChiSoCuoi == dto.ChiSoCuoi)
                                                            .FirstOrDefaultAsync();
            if (dinhMucCheck != null)
                return dinhMucCheck;
            return null;
        }

        public async Task<List<DinhMucDTO>> GetDSDienDinhMuc()
        {
            var dsDienDinhMuc = await _context.dvDienDinhMucs.Select(x => new DinhMucDTO
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
