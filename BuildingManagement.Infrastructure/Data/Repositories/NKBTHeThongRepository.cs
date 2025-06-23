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
    public class NKBTHeThongRepository : Repository<tnbtHeThong>, INKBTHeThongRepository
    {
        public NKBTHeThongRepository(BuildingManagementDbContext context) : base(context)
        {
        }

        public async Task<tnbtHeThong> CheckHeThong(int MaHeThong)
        {
            var heThong = await _context.tnbtHeThongs.Where(x => x.MaHeThong == MaHeThong).FirstOrDefaultAsync();
            if(heThong == null)
            {
                return null;
            }
            return heThong;
        }

        public async Task<PagedResult<HeThongDTO>> GetDSHeThong(int pageNumber, int pageSize = 15)
        {
            var dsHeThong = _context.tnbtHeThongs.Select(x => new HeThongDTO
            {
                MaHeThong = x.MaHeThong,
                TenHeThong = x.TenHeThong,
                NhanHieu = x.NhanHieu,
                Model = x.Model,
                TrangThai = x.TrangThai,
                SerialNumber = x.SerialNumber,
                GhiChu = x.GhiChu,
                InstallationDate = x.CreatedDate,
                LastMaintenanceDate = x.nkbtLichSuBaoTris
                    .OrderByDescending(y => y.CreatedDate)
                    .Select(y => y.CreatedDate)
                    .FirstOrDefault(),
                NextMaintenanceDate = x.nkbtKeHoachBaoTris.OrderByDescending(y => y.CreatedDate)
                    .Select(y => y.CreatedDate)
                    .FirstOrDefault(),
                MaTN = x.MaTN ?? 0,
                TenTN = x.tnToaNha != null ? x.tnToaNha.TenTN : "Chưa xác định"
            });
            if(pageNumber != 0)
            {
                var items = await dsHeThong.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                                        .ToListAsync();
                var totalCount = await dsHeThong.CountAsync();
                var totalPage = (int)Math.Ceiling((decimal)totalCount / pageSize);
                return new PagedResult<HeThongDTO>
                {
                    Data = items,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = totalPage
                };
            }
            else
            {
                var items = await dsHeThong.ToListAsync();
                var totalCount = await dsHeThong.CountAsync();
                return new PagedResult<HeThongDTO>
                {
                    Data = items,
                    TotalCount = totalCount,
                    PageNumber = 1,
                    PageSize = totalCount,
                    TotalPages = 1
                };
            }
        }
    }
    
}
