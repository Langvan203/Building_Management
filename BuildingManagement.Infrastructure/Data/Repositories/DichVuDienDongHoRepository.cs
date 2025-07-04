﻿using BuildingManagement.Application.DTOs;
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
    public class DichVuDienDongHoRepository : Repository<dvDienDongHo>, IDichVuDienDongHoRepository
    {
        public DichVuDienDongHoRepository(BuildingManagementDbContext context) : base(context)
        {
        }

        public async Task<dvDienDongHo> CheckDongHo(int MaDongHo)
        {
            var dongHo = await _context.dvDienDongHos.FirstOrDefaultAsync(dh => dh.MaDH == MaDongHo);
            if(dongHo == null)
            {
                return null;
            }
            return dongHo;
        }

        public async Task<bool> CheckThemDongHoDien(CreateDongHoDto dto)
        {
            var checkDongHo = await _context.dvDienDongHos.FirstOrDefaultAsync(dh => dh.SoDongHo == dto.SoDongHo && dh.MaMB == dto.MaMB);
            if(checkDongHo == null)
            {
                return true;
            }
            return false;
        }

        public async Task<PagedResult<DongHoDTO>> GetDSDongHoDienPaged(int pageNumber, int pageSize = 15)
        {
            var dsDongHo = _context.dvDienDongHos
                .Include(dh => dh.tnMatBang)
                .Select(dh => new DongHoDTO
                {
                    MaDH = dh.MaDH,
                    SoDongHo = dh.SoDongHo,
                    MaMB = dh.MaMB == null ? 0 : (int)dh.MaMB,
                    MaVT = dh.MaMB == null ? "Chưa có  vị trí" : dh.tnMatBang.MaVT,
                    TenKH = dh.MaKH != null ? dh.tnKhachHang.IsCaNhan ? dh.tnKhachHang.HoTen : dh.tnKhachHang.CtyTen : "Chưa có khách hàng",
                    ChiSoSuDung = dh.ChiSoSuDung,
                    TrangThai = dh.TrangThai,
                    MaTN = dh.MaMB != null ? (int)dh.MaTN : 0,
                    MaTL = dh.MaMB != null ? (int)dh.MaTL : 0,
                    MaKN = dh.MaMB != null ? (int)dh.MaKN : 0,
                    UpdatedDate = dh.UpdatedDate
                });

            int totalCount = await dsDongHo.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var data = await dsDongHo.Skip((pageNumber -1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PagedResult<DongHoDTO>
            {
                Data = data,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }
    }
   
}
