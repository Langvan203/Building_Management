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
    public class DichVuNuocDongHoRepository : Repository<dvNuocDongHo>, IDichVuNuocDongHoRepository
    {
        public DichVuNuocDongHoRepository(BuildingManagementDbContext context) : base(context)
        {
        }

        public async Task<dvNuocDongHo> CheckDongHo(int MaDongHo)
        {
            var dongHo = await _context.dvNuocDongHos.FirstOrDefaultAsync(dh => dh.MaDH == MaDongHo);
            if (dongHo == null)
            {
                return null;
            }
            return dongHo;
        }

        public async Task<bool> CheckThemDongHoNuoc(CreateDongHoDto dto)
        {
            var checkDongHo = await _context.dvNuocDongHos.FirstOrDefaultAsync(dh => dh.SoDongHo == dto.SoDongHo && dh.MaMB == dto.MaMB);
            if (checkDongHo == null)
            {
                return true;
            }
            return false;
        }

        public async Task<PagedResult<DongHoDTO>> GetDSDongHoNuocPaged(int pageNumber, int pageSize = 15)
        {
            var dsDongHo = _context.dvNuocDongHos.Include(x => x.tnMatBang).Select(dh => new DongHoDTO
            {
                MaDH = dh.MaDH,
                SoDongHo = dh.SoDongHo,
                MaMB = (int)dh.MaMB,
                MaVT = dh.tnMatBang.MaVT,
                TenKH = dh.tnKhachHang.IsCaNhan ? dh.tnKhachHang.HoTen : dh.tnKhachHang.CtyTen,
                ChiSoSuDung = dh.ChiSoSuDung,
                TrangThai = dh.TrangThai,
                MaTN = dh.tnMatBang.MaTN,
                MaTL = dh.tnMatBang.MaTL,
                MaKN = (int)dh.tnMatBang.MaKN,
                UpdatedDate = dh.UpdatedDate
            });

            var dsDongHoNuoc = await dsDongHo.Skip((pageNumber - 1)*pageSize).Take(pageSize).ToListAsync();
            var totalCount = await dsDongHo.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var pagedResult = new PagedResult<DongHoDTO>
            {
                Data = dsDongHoNuoc,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
            return pagedResult;
        }

    }
    
}
