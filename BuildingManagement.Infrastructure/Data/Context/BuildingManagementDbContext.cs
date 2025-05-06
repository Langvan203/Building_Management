using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BuildingManagement.Infrastructure.Data.Context
{
    public class BuildingManagementDbContext : DbContext
    {
        public BuildingManagementDbContext(DbContextOptions<BuildingManagementDbContext> options) : base(options) 
        {
            
        }

        public DbSet<dvDichVu> dvDichVus { get; set; }
        public DbSet<dvDichVuSuDung> dvDichVuSuDungs { get; set; }
        public DbSet<dvDien> dvDiens { get; set; }
        public DbSet<dvDienDinhMuc> dvDienDinhMucs { get; set; }
        public DbSet<dvDienDongHo> dvDienDongHos { get; set; }
        public DbSet<dvgxLoaiXe> dvgxLoaiXes { get; set; }
        public DbSet<dvgxTheXe> dvgxTheXes { get; set; }
        public DbSet<dvHoaDon> dvHoaDons { get; set; }
        public DbSet<dvLoaiDV> dvLoaiDVs { get; set; }
        public DbSet<dvNuoc> dvNuocs { get; set; }
        public DbSet<dvNuocDinhMuc> dvNuocDinhMucs { get; set; }
        public DbSet<dvNuocDongHo> dvNuocDongHos { get; set; }
        public DbSet<mbLoaiMB> mbLoaiMBs { get; set; }
        public DbSet<mbTrangThai> mbTrangThais { get; set; }
        public DbSet<nkbtChiTietBaoTri> nkbtChiTietBaoTris { get; set; }
        public DbSet<nkbtKeHoachBaoTri> nkbtKeHoachBaoTris { get; set; }
        public DbSet<nkbtLichSuBaoTri> nkbtLichSuBaoTris { get; set; }
        public DbSet<nkbtTrangThai> nkbtTrangThais { get; set; }
        public DbSet<ptPhieuThu> ptPhieuThus { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<tnbtHeThong> tnbtHeThongs { get; set; }
        public DbSet<tnKhachHang> tnKhachHangs { get; set; }
        public DbSet<tnKhoiNha> tnKhoiNhas { get; set; }
        public DbSet<tnMatBang> tnMatBangs { get; set; }
        public DbSet<tnNhanVien> tnNhanViens { get; set; }
        public DbSet<tnPhongBan> tnPhongBans { get; set; }
        public DbSet<tnTangLau> tnTangLaus { get; set; }
        public DbSet<tnToaNha> tnToaNhas { get; set; }
        public DbSet<tnycYeuCauSuaChua> tnycYeuCauSuaChuas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BuildingManagementDbContext).Assembly);

            modelBuilder.Entity<Role>().HasData(
                new Role { RoleID = 1, RoleName = "Admin", NguoiTao = "Admin", NguoiSua = "", CreatedDate = new DateTime(2025,04,28) },
                new Role { RoleID = 2, RoleName = "Quản lý tòa nhà", NguoiTao = "Admin", NguoiSua = "", CreatedDate = new DateTime(2025,04,28) },
                new Role { RoleID = 3, RoleName = "Nhân viên lễ tân", NguoiTao = "Admin", NguoiSua = "", CreatedDate = new DateTime(2025,04,28) },
                new Role { RoleID = 4, RoleName = "Nhân viên tòa nhà", NguoiTao = "Admin", NguoiSua = "", CreatedDate = new DateTime(2025,04,28) }
                );


            modelBuilder.Entity<mbTrangThai>().HasData(
                new mbTrangThai { MaTrangThai = 1, TenTrangThai = "Chưa bàn giao", NguoiTao = "Admin", NguoiSua = "", CreatedDate = new DateTime(2025, 04, 28) },
                new mbTrangThai { MaTrangThai = 2, TenTrangThai = "Đang sử dụng", NguoiTao = "Admin", NguoiSua = "", CreatedDate = new DateTime(2025, 04, 28) },
                new mbTrangThai { MaTrangThai = 3, TenTrangThai = "Đã thanh lý", NguoiTao = "Admin", NguoiSua = "", CreatedDate = new DateTime(2025, 04, 28) }
                );

        }
    }
}
