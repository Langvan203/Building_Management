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
        public DbSet<tnycTrangThai> tnycTrangThais { get; set; }
        public DbSet<nvDanhGia> nvDanhGias { get; set; }
        public DbSet<Permission> permissions { get; set; }
        public DbSet<PaymentInfo> paymentInfo { get; set; }
        public DbSet<PaymentNotification> paymentMethods { get; set; }
        public DbSet<PayOSConfiguration> payOSConfigurations { get; set; }


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

            modelBuilder.Entity<tnycTrangThai>().HasData(
                new tnycTrangThai { IdTrangThai = 1, TenTrangThai = "Chờ duyệt", NguoiTao = "Admin", NguoiSua = "", CreatedDate = new DateTime(2025, 04, 28) },
                new tnycTrangThai { IdTrangThai = 2, TenTrangThai = "Đã duyệt", NguoiTao = "Admin", NguoiSua = "", CreatedDate = new DateTime(2025, 04, 28) },
                new tnycTrangThai { IdTrangThai = 3, TenTrangThai = "Đang thực hiện", NguoiTao = "Admin", NguoiSua = "", CreatedDate = new DateTime(2025, 04, 28) },
                new tnycTrangThai { IdTrangThai = 4, TenTrangThai = "Đã hoàn thành", NguoiTao = "Admin", NguoiSua = "", CreatedDate = new DateTime(2025, 04, 28) }
                );

            modelBuilder.Entity<dvLoaiDV>().HasData(
                new dvLoaiDV { MaLDV = 1, TenLDV = "Dịch vụ điện", NguoiTao = "Admin", NguoiSua = "", CreatedDate = new DateTime(2025, 04, 28), IsEssential = true, MoTa = "Dịch vụ điện cho cư dân", Icon = "<Zap className=\"h-5 w-5 text-yellow-500\" />" },
                new dvLoaiDV { MaLDV = 2, TenLDV = "Dịch vụ nước", NguoiTao = "Admin", NguoiSua = "", CreatedDate = new DateTime(2025, 04, 28), IsEssential = true, MoTa = "Dịch vụ nước cho cư dân", Icon = "<Droplet className=\"h-5 w-5 text-blue-500\" />" },
                new dvLoaiDV { MaLDV = 3, TenLDV = "Dịch vụ Internet", Icon = "<Wifi className=\"h-5 w-5 text-purple-500\" />", NguoiTao = "Admin", NguoiSua = "", CreatedDate = new DateTime(2025, 04, 28), IsEssential = true, MoTa = "Dịch vụ Internet cho cư dân" },
                new dvLoaiDV { MaLDV = 4, TenLDV = "Dịch vụ gửi xe", Icon = "<Car className=\"h-5 w-5 text-gray-500\" />", NguoiTao = "Admin", NguoiSua = "", CreatedDate = new DateTime(2025, 04, 28), IsEssential = true, MoTa = "Dịch vụ gửi xe cho cư dân" },
                new dvLoaiDV { MaLDV = 5, TenLDV = "Dịch vụ Gym", Icon = "<Dumbbell className=\"h-5 w-5 text-green-500\" />", NguoiTao = "Admin", NguoiSua = "", CreatedDate = new DateTime(2025, 04, 28), IsEssential = false, MoTa = "Dịch vụ phòng tập Gym cho cư dân" }
                );


            modelBuilder.Entity<nkbtTrangThai>().HasData(
                new nkbtTrangThai { MaTrangThai = 1, TenTrangThai = "Chưa thực hiện", NguoiTao = "Admin", NguoiSua = "", CreatedDate = new DateTime(2025, 04, 28) },
                new nkbtTrangThai { MaTrangThai = 2, TenTrangThai = "Đang thực hiện", NguoiTao = "Admin", NguoiSua = "", CreatedDate = new DateTime(2025, 04, 28) },
                new nkbtTrangThai { MaTrangThai = 3, TenTrangThai = "Đã hoàn thành", NguoiTao = "Admin", NguoiSua = "", CreatedDate = new DateTime(2025, 04, 28) },
                new nkbtTrangThai { MaTrangThai = 4, TenTrangThai = "Đã hủy", NguoiTao = "Admin", NguoiSua = "", CreatedDate = new DateTime(2025, 04, 28) }
                );


        }
    }
}
