using AutoMapper;
using BuildingManagement.Application.DTOs;
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

        public async Task<dvDichVuSuDung> CheckDangKySuDung(int MaKH, int MaMB, int MaDV, DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            var dvSuDungNew = await _context.dvDichVuSuDungs.Where(x => x.MaKH == MaKH && x.MaMB == MaMB && x.MaDV == MaDV && x.TrangThaiSuDung == true && x.NgayBatDauTinhPhi == ngayBatDau && x.NgayKetThucTinhPhi == x.NgayKetThucTinhPhi).FirstOrDefaultAsync();
            if(dvSuDungNew == null)
            {
                return null;
            }
            return dvSuDungNew;
        }

        public async Task<dvDichVuSuDung> CheckDichVuSuDung(int MaDVSD)
        {
            var dvSuDung = await _context.dvDichVuSuDungs.Where(x => x.MaDVSD == MaDVSD).FirstOrDefaultAsync();
            if (dvSuDung == null)
            {
                return null;
            }
            return dvSuDung;
        }

        public async Task<dvDichVuSuDung> CheckDichVuSuDungIncludeManyTable(int MaDVSD)
        {
            var dvSuDung = await _context.dvDichVuSuDungs.Where(x => x.MaDVSD == MaDVSD).Include(x => x.dvDichVu).Include(x => x.tnKhachHang).FirstOrDefaultAsync();
            if (dvSuDung == null)
            {
                return null;
            }
            return dvSuDung;
        }

        public async Task<PagedResult<GetDSDangSuDung>> GetDSDangSuDung(int pageNumber, int pageSize = 15)
        {
            var dsDangSuDung = _context.dvDichVuSuDungs.Select(x => new GetDSDangSuDung
            {
                MaDVSD = x.MaDVSD,
                TenDV = x.dvDichVu.TenDV,
                MaVT = x.tnMatBang.MaVT,
                MaTN = (int)x.MaTN,
                MaKN = (int)x.MaKN,
                MaTL = (int)x.MaTL,
                TrangThai =(bool)x.TrangThaiSuDung,
                NgayBatDauSuDung = x.NgayBatDauTinhPhi,
                NgayDenHanThanhToan = x.NgayKetThucTinhPhi,
                MaKH = (int)x.MaKH,
                TenKH = x.tnKhachHang.IsCaNhan ? x.tnKhachHang.HoTen : x.tnKhachHang.CtyTen,
                MaLDV = x.dvDichVu.MaLDV
            });

            var totalCount = await dsDangSuDung.CountAsync();
            var totalPage = (int)Math.Ceiling((double)totalCount / pageSize);
            var items = await dsDangSuDung.OrderByDescending(x => x.NgayBatDauSuDung).Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .ToListAsync();

            return new PagedResult<GetDSDangSuDung>
            {
                Data = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPage,
            };
        }

        public async Task<PagedResult<GetDSYeuCauSuDung>> GetDSYeuCauSuDung(int pageNumber, DateTime ngayBatDau, DateTime ngayKetThuc, int pageSize = 15)
        {
            var dsYeuCauSuDung = _context.dvDichVuSuDungs.Where(x => x.IsRequestByResident == true && x.CreatedDate >= ngayBatDau && x.CreatedDate <= ngayKetThuc).Include(x => x.dvDichVu)
                .Select(x => new GetDSYeuCauSuDung
                {
                    MaDVSD = x.MaDVSD,
                    MaDV = x.MaDV,
                    TenDV = x.dvDichVu.TenDV,
                    MaMB = x.MaMB,
                    MaVT = x.tnMatBang.MaVT,
                    MaKH = (int)x.MaKH,
                    TenKH = x.tnKhachHang.IsCaNhan ? x.tnKhachHang.HoTen : x.tnKhachHang.CtyTen,
                    RequestDate = x.CreatedDate,
                    GhiChu = x.GhiChu,
                    MaTN = (int)x.MaTN,
                    MaKN = (int)x.MaKN,
                    MaTL = (int)x.MaTL,
                    TrangThai = x.IsDuyet
                });
            
            var totalCount = await dsYeuCauSuDung.CountAsync();
            var totalPage = (int)Math.Ceiling((double)totalCount / pageSize);
            var items = await dsYeuCauSuDung.OrderByDescending(x => x.RequestDate).Skip((pageNumber-1)*pageSize).Take(pageSize)
                .ToListAsync();

            return new PagedResult<GetDSYeuCauSuDung>
            {
                Data = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPage
            };

        }

        public async Task<PagedResult<GetThongKeSuDung>> GetThongKeSuDung(int pageNumber, DateTime ngayBatDau, DateTime ngayKetThuc, int pageSize = 15)
        {
            var thongKeSuDung = _context.dvDichVuSuDungs.Where(x => x.NgayBatDauTinhPhi >= ngayBatDau && x.NgayKetThucTinhPhi <= ngayKetThuc).Select(x => new GetThongKeSuDung
            {
                MaDVSD = x.MaDVSD,
                NgayBatDauSuDung = x.NgayBatDauTinhPhi,
                NgayDenHanThanhToan = x.NgayKetThucTinhPhi,
                TienVAT = x.TienVAT,
                TienBVMT = x.TienBVMT,
                ThanhTien = x.ThanhTien,
                MaDV = x.MaDV,
                TenDV = x.dvDichVu.TenDV,
                MaKH = (int)x.MaKH,
                TenKH = x.tnKhachHang.IsCaNhan ? x.tnKhachHang.HoTen : x.tnKhachHang.CtyTen,
                MaTN = (int)x.MaTN,
                MaKN = (int)x.MaKN,
                MaTL = (int)x.MaTL,
                MaMB = x.MaMB,
                MaVT = x.tnMatBang.MaVT,
                IsDuyetHoaDon = (bool)x.IsChuyenHoaDon,
                MaLDV = x.dvDichVu.MaLDV
            });
            var totalCount = await thongKeSuDung.CountAsync();
            var totalPage = (int)Math.Ceiling((double)totalCount / pageSize);
            var items = await thongKeSuDung.OrderByDescending(x => x.NgayBatDauSuDung).Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .ToListAsync();

            return new PagedResult<GetThongKeSuDung>
            {
                Data = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPage
            };
        }
    }
    
}
