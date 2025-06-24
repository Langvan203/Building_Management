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
    public class YeuCauBaoTriRepository : Repository<tnycYeuCauSuaChua>, IYeuCauBaoTriRepository
    {
        public YeuCauBaoTriRepository(BuildingManagementDbContext context) : base(context)
        {

        }

        public async Task<tnycYeuCauSuaChua> CheckYeuCauIncludeNhanVien(int MaYC)
        {
            var yeuCau = await _context.tnycYeuCauSuaChuas.Where(x => x.MaYC == MaYC).Include(x => x.tnNhanViens).FirstOrDefaultAsync();
            return yeuCau;
        }

        public async Task<PagedResult<YeuCauSuaChuaDTO>> GetDSYeuCauSuaChua(int pageNumber, int pageSize = 10)
        {
            var dsYeuCau = _context.tnycYeuCauSuaChuas.Select(x => new YeuCauSuaChuaDTO
            {
                MaYC = x.MaYC,
                TieuDe = x.TieuDe,
                MaTN = x.MaTN,
                MaKN = (int)x.MaKN,
                MaTL = (int)x.MaTL,
                MaMB = (int)x.MaMB,
                TenTN = x.tnToaNha.TenTN,
                TenKN = x.tnKhoiNha.TenKN,
                TenTL = x.tnTangLau.TenTL,
                MaVT = x.tnMatBang.MaVT,
                MaKH = x.tnKhachHang.MaKH,
                TenKH = x.tnKhachHang.IsCaNhan ? x.tnKhachHang.HoTen : x.tnKhachHang.CtyTen,
                NgayYeuCau = x.CreatedDate,
                MucDoYeuCau = x.MucDoYeuCau,
                IdTrangThai = x.IdTrangThai,
                TenTrangThai = x.tnycTrangThai.TenTrangThai,
                NguoiYeuCau = x.NguoiYeuCau,
                MoTa = x.MoTa,
                ImagePath = x.ImagePath,
                GhiChu = x.GhiChu,
                MaHeThong = x.MaHeThong,
                TenHeThong = x.tnbtHeThong.TenHeThong,
                NhanVienInYeuCaus = x.tnNhanViens.Select(nv => new NhanVienInYeuCau
                {
                    MaNV = nv.MaNV,
                    TenNV = nv.TenNV
                }).ToList()
            });

            var totalRecords = await dsYeuCau.CountAsync();
            var items = await dsYeuCau
                .OrderByDescending(x => x.NgayYeuCau).Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            return new PagedResult<YeuCauSuaChuaDTO>
            {
                Data = items,
                TotalCount = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }
    }
   
}
