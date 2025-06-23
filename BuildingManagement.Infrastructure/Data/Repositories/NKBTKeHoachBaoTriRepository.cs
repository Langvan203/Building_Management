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
    public class NKBTKeHoachBaoTriRepository : Repository<nkbtKeHoachBaoTri>, INKBTKeHoachBaoTriRepository
    {
        public NKBTKeHoachBaoTriRepository(BuildingManagementDbContext context) : base(context)
        {
        }

        public async Task<nkbtKeHoachBaoTri> CheckKeHoach(int MaKeHoach)
        {
            var keHoach = await _context.nkbtKeHoachBaoTris.Include(x => x.tnNhanViens)
                .FirstOrDefaultAsync(x => x.MaKeHoach == MaKeHoach);
            return keHoach;
        }

        public async Task<PagedResult<KeHoachBaoTriDto>> GetDSKeHoachBaoTri(int pageNumber, int pageSize = 15)
        {
            var dsKeHoachBaoTri = _context.nkbtKeHoachBaoTris.Select(x => new KeHoachBaoTriDto
            {
                MaKeHoach = x.MaKeHoach,
                TenKeHoach = x.TenKeHoach,
                MaHeThong = x.MaHeThong,
                TenHeThong = x.tnbtHeThong.TenHeThong,
                NgayBaoTri = x.NgayBaoTri,
                TanSuat = x.TanSuat,
                TrangThai = x.MaTrangThai,
                LoaiBaoTri = x.LoaiBaoTri,
                nhanVienInBaoTris = x.tnNhanViens.Select(nv => new NhanVienInBaoTri
                {
                    MaNV = nv.MaNV,
                    TenNV = nv.TenNV
                }).ToList(),
                chiTietInKeHoachBaoTris = x.nkbtChiTietBaoTris.Select(ct => new ChiTietInKeHoachBaoTri
                {
                    GhiChu = ct.GhiChu,
                    MaTrangThai = ct.MaTrangThai
                }).ToList(),
                lichSuBaoTriKeHoaches = x.nkbtLichSuBaoTris.Select(ls => new LichSuBaoTriKeHoach
                {
                    TieuDe = ls.TieuDe,
                    NgayCapNhat = ls.CreatedDate,
                    NoiDung = ls.GhiChu
                }).ToList(),
            });

            var totalRecords = await dsKeHoachBaoTri.CountAsync();
            var pagedResult = await dsKeHoachBaoTri
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var tatalPage = (int)Math.Ceiling((decimal)totalRecords / pageSize);

            return new PagedResult<KeHoachBaoTriDto>
            {
                Data = pagedResult,
                TotalCount = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = tatalPage
            };
        }
    }
    
}
