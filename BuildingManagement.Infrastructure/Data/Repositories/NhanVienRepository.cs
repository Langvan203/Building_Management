using BuildingManagement.Application.DTOs;
using BuildingManagement.Application.DTOs.Request.AuthDto;
using BuildingManagement.Application.Interfaces.Repositories;
using BuildingManagement.Domain.Entities;
using BuildingManagement.Infrastructure.Data.Context;
using BuildingManagement.Infrastructure.Ultility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Infrastructure.Data.Repositories
{
    public class NhanVienRepository : Repository<tnNhanVien>, INhanVienRepository
    {
        public NhanVienRepository(BuildingManagementDbContext context) : base(context) 
        {

        }

        public async Task<tnNhanVien> ThongTinNhanVien(LoginDto loginDto)
        {

            var nv = await _context.tnNhanViens.Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Email == loginDto.Email);
            if (nv!=null && HashPassWord.VerifyPassword(nv.PasswordHash, loginDto.Password))
            {
                return nv;
            }
            return null;
        }

        public async Task<List<GetDSNhanVienDto>> GetDSNhanVien()
        {
            var ds = await _context.tnNhanViens
             .Include(nv => nv.tnPhongBans)
             .Include(pb => pb.tnToaNhas)
             .Include(role => role.Roles)
             .AsSplitQuery()
             .ToListAsync();
            var dsNhanVien = ds.Select(nv => new GetDSNhanVienDto
            {
                MaNV = nv.MaNV,
                TenNV = nv.TenNV,
                Email = nv.Email,
                SDT = nv.SDT,
                DiaChiThuongTru = nv.DiaChiThuongTru,
                NgaySinh = nv.NgaySinh,
                UserName = nv.UserName,
                phongBans = nv.tnPhongBans.Select(pb => new NhanVienPhongBan
                {
                    MaPB = pb.MaPB.ToString(),
                    TenPB = pb.TenPB
                }).ToList(),
                toaNhas = nv.tnToaNhas.Select(tn => new NhanVienInToaNha
                {
                    MaTN = tn.MaTN.ToString(),
                    TenTN = tn.TenTN
                }).ToList(),
                Roles = nv.Roles.Select(role => new NhanVienRoles
                {
                    RoleID = role.RoleID.ToString(),
                    RoleName = role.RoleName
                }).ToList()
            }).ToList();
            return dsNhanVien;
        }

        public async Task<tnNhanVien> CheckNVInPhongBan(int MaNV, int MaPB)
        {
            var checkNV = await _context.tnNhanViens.Include(x => x.tnPhongBans)
                .FirstOrDefaultAsync(x => x.MaNV == MaNV);
            return checkNV;
        }

        public async Task<tnNhanVien> CheckNVInToaNha(int MaNV, int MaPB)
        {
            var checkNV = await _context.tnNhanViens.Include(x => x.tnPhongBans)
                .FirstOrDefaultAsync(x => x.MaNV == MaNV && x.tnPhongBans.Any(pb => pb.tnToaNha.MaTN == MaPB));
            return checkNV;
        }

        public async Task<tnNhanVien> GetNhanVienInPhongBan(int MaNV)
        {
            var nv = await _context.tnNhanViens.Include(x => x.tnPhongBans).FirstOrDefaultAsync(x => x.MaNV == MaNV);
            return nv;
        }

        public async Task<tnNhanVien> GetNhanVienInToaNha(int MaNV)
        {
            var nv = await _context.tnNhanViens.Include(x => x.tnToaNhas).FirstOrDefaultAsync(x => x.MaNV == MaNV);
            return nv;
        }

        public async Task<tnNhanVien> GetNhanVienRoles(int manv)
        {
            var nv = await _context.tnNhanViens.Include(x => x.tnToaNhas).FirstOrDefaultAsync(x => x.MaNV == manv);
            return nv;
        }
    }
}
