using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Repositories;
using BuildingManagement.Domain.Entities;
using BuildingManagement.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Infrastructure.Data.Repositories
{
    public class PhongBanRepository : Repository<tnPhongBan>, IPhongBanRepository
    {
        public PhongBanRepository(BuildingManagementDbContext context) : base(context)
        {
        }

        public Task<bool> AddPhongBan(tnPhongBan phongBan)
        {
            throw new NotImplementedException();
        }

        public async Task<tnNhanVien> CheckNhanVienInPhongBan(int MaPB, int MaNV)
        {
            var phongBan = await _context.tnPhongBans
                .Include(pb => pb.tnNhanViens)
                .FirstOrDefaultAsync(pb => pb.MaPB == MaPB);
            if (phongBan == null)
            {
                return null; // Phòng ban không tồn tại
            }
            else
            {
                var nhanVien = phongBan.tnNhanViens.FirstOrDefault(nv => nv.MaNV == MaNV);
                return nhanVien; // Trả về phòng ban nếu nhân viên tồn tại trong danh sách
            }
        }

        public Task<bool> DeletePhongBan(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PhongBanDto>> GetAllPhongBan()
        {
            var dsPhongBan = await _context.tnPhongBans.Include(x => x.tnToaNha).Include(x => x.tnNhanViens).ToListAsync();

            var dsPB = dsPhongBan.Select(x => new PhongBanDto
            {
                MaPB = x.MaPB,
                TenPB = x.TenPB,
                MaTN = (int)x.MaTN,
                TenTN = x.tnToaNha.TenTN,
                NhanVienInPhongBans = x.tnNhanViens.Select(y => new NhanVienInPhongBan
                {
                    MaNV = y.MaNV,
                    TenNV = y.TenNV,
                    Email = y.Email,
                    SDT = y.SDT
                }).ToList()
            }).ToList();
            return dsPB;
        }

        public Task<tnPhongBan> GetPhongBanById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveNhanVienInPhongBan(int MaPB, int MaNV)
        {
            var phongBan = await _context.tnPhongBans
                .Include(pb => pb.tnNhanViens)
                .FirstOrDefaultAsync(pb => pb.MaPB == MaPB);
            if (phongBan == null)
            {
                return false; // Phòng ban không tồn tại
            }
            else
            {
                var nhanVien = phongBan.tnNhanViens.FirstOrDefault(nv => nv.MaNV == MaNV);
                if (nhanVien == null)
                {
                    return false; // Nhân viên không tồn tại trong phòng ban
                }
                phongBan.tnNhanViens.Remove(nhanVien); // Xóa nhân viên khỏi phòng ban
                await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
                return true; // Trả về phòng ban nếu nhân viên tồn tại trong danh sách
            }
        }

        public Task<bool> UpdatePhongBan(tnPhongBan phongBan)
        {
            throw new NotImplementedException();
        }
    }
   
}
