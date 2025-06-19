using AutoMapper;
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
    public class DichVuSuDungRepository : Repository<dvDichVuSuDung>, IDichVuSuDungRepository
    {
        private readonly IMapper _mapper;
        public DichVuSuDungRepository(BuildingManagementDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<GetAllDSDichVuSuDung>> GetAllDSDichVuSuDungs()
        {
            var dsDichVu = await _context.dvDichVuSuDungs.Include(x => x.dvDichVu)
                .Include(x => x.tnKhachHang)
                .Include(x => x.tnToaNha)
                .Include(x => x.tnKhoiNha)
                .Include(x => x.tnTangLau)
                .Include(x => x.tnMatBang)
                .AsSingleQuery().ToListAsync();
            var dsDichVuDTO = dsDichVu.Select(x => new GetAllDSDichVuSuDung
            {
                MaDVSD = x.MaDVSD,
                MaKH = x.tnKhachHang.MaKH,
                MaLDV = x.dvDichVu.MaLDV,
                NgayBatDauTinhPhi = x.NgayBatDauTinhPhi,
                TrangThai = x.IsDuyet,
                GhiChu = x.GhiChu,
                TenDV = x.dvDichVu.TenDV,
                TenKH = x.tnKhachHang.IsCaNhan ? x.tnKhachHang.HoTen : x.tnKhachHang.CtyTen,
                MaKN = (int)x.tnKhachHang.MaKN,
                MaTL = (int)x.tnKhachHang.MaTL,
                MaTN = (int)x.tnKhachHang.MaTN,
                MaVT = x.tnMatBang.MaVT,
            }).ToList();
            return dsDichVuDTO;
        }

        public async Task<List<GetDSDichVuSuDung>> GetDSDichVuSuDungByCuDan(int MaKH)
        {
            var dsDichVu = await _context.dvDichVuSuDungs.Where(x => x.MaKH == MaKH).Include(x => x.dvDichVu).AsSingleQuery().ToListAsync();
            var dsDichVuDTO = dsDichVu.Select(x => new GetDSDichVuSuDung
            {
                MaDVSD = x.MaDVSD,
                NgayBatDauTinhPhi = x.NgayBatDauTinhPhi,
                NgayKetThucTinhPhi = x.NgayKetThucTinhPhi,
                TienBVMT = x.TienBVMT,
                TienVAT = x.TienVAT,
                TrangThai = x.IsDuyet,
                ThanhTien = x.ThanhTien,
                GhiChu = x.GhiChu,
                TenDV = x.dvDichVu.TenDV,
                DonGia = x.dvDichVu.DonGia,
                DonViTinh = x.dvDichVu.DonViTinh
            }).ToList();
            return dsDichVuDTO;
        }

        public async Task<IEnumerable<DichVuSuDungDto>> GetDSDichVuSuDungByMaKH(int MaKH)
        {
            var dsDVSuDung = await _context.dvDichVuSuDungs.Where(x => x.MaKH == MaKH).ToListAsync();
            return _mapper.Map<IEnumerable<DichVuSuDungDto>>(dsDVSuDung);
        }
    }
    
}
