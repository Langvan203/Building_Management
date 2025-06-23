using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs.Request
{
    public class DichVuSuDungDto
    {
        public int MaDVSD { get; set; }
        public string DonViTinh { get; set; }
        public int KyThanhToan { get; set; }
        public bool IsThanhToanTheoKy { get; set; }
        public DateTime NgayBatDauTinhPhi { get; set; }
        public DateTime NgayKetThucTinhPhi { get; set; }
        public decimal DonGia { get; set; }
        public decimal TyLeVAT { get; set; }
        public decimal TienVAT { get; set; }
        public bool IsDuyet { get; set; }
        public decimal TyLeBVMT { get; set; }
        public decimal TienBVMT { get; set; }
        public decimal ThanhTien { get; set; }
        public string GhiChu { get; set; }
        // FK
        public int MaDV { get; set; }
        public int MaKH { get; set; }
        public int MaMB { get; set; }
    }

    public class CreateDichVuSuDungDto
    {
        public DateTime NgayBatDauTinhPhi { get; set; }
        public DateTime NgayKetThucTinhPhi { get; set; }
        public bool IsDuyet { get; set; } = true;
        public bool TrangThaiSuDung { get; set; } = true;
        public decimal ThanhTien { get; set; }
        public string GhiChu { get; set; }
        public decimal TienBVMT { get; set; }
        public decimal TienVAT { get; set; }
        // FK
        public int MaDV { get; set; }
        public int MaKH { get; set; }
        public int MaMB { get; set; }
        public int MaKN { get; set; }
        public int MaTL { get; set; }
        public int MaTN { get; set; }
    }

    public class GetDSDichVuSuDung
    {
        public int MaDVSD { get; set; }
        public DateTime NgayBatDauTinhPhi { get; set; }
        public DateTime NgayKetThucTinhPhi { get; set; }
        public decimal TienVAT { get; set; }
        public decimal TienBVMT { get; set; }
        public decimal ThanhTien { get; set; }
        public string GhiChu { get; set; }
        public string TenDV { get; set; }
        public decimal DonGia { get; set; }
        public string DonViTinh { get; set; }
        public bool TrangThai { get; set; }
    }

    public class GetDSDangSuDung
    {
        public int MaDVSD { get; set; }
        public string TenDV { get; set; }
        public DateTime NgayBatDauSuDung { get; set; }
        public DateTime NgayDenHanThanhToan { get; set; }
        public bool TrangThai { get; set; }
        public int MaTN { get; set; }
        public int MaKN { get; set; }
        public int MaTL { get; set; }
        public string MaVT { get; set; }
        public int MaKH { get; set; }
        public string TenKH { get; set; }
        public int MaLDV { get; set; }
    }

    public class GetDSYeuCauSuDung
    {
        public int MaDVSD { get; set; }
        public DateTime RequestDate { get; set; }
        public string TenDV { get; set; }
        public int MaDV { get; set; }
        public int TrangThai { get; set; }
        public string GhiChu { get; set; }
        public int MaMB { get; set; }
        public string MaVT { get; set; }
        public int MaKH { get; set; }
        public string TenKH { get; set; }
        public int MaTN { get; set; }
        public int MaKN { get; set; }
        public int MaTL { get; set; }
    }

    public class GetThongKeSuDung
    {
        public int MaDVSD { get; set; }
        public DateTime NgayBatDauSuDung { get; set; }
        public DateTime NgayDenHanThanhToan { get; set; }
        public decimal TienVAT { get; set; }
        public decimal TienBVMT { get; set; }
        public decimal ThanhTien { get; set; }
        public int MaDV { get; set; }
        public string TenDV { get; set; }
        public int MaKH { get; set; }
        public string TenKH { get; set; }
        public int MaTN { get; set; }
        public int MaMB { get; set; }
        public int MaKN { get; set; }
        public int MaTL { get; set; }
        public string MaVT { get; set; }
        public bool IsDuyetHoaDon { get; set; }
        public int MaLDV { get; set; }
    }

    public class DuyetSangHoaDon
    {
        public decimal ThueVAT { get; set; }
        public decimal ThueBVMT { get; set; }
        public decimal TienTruocVAT { get; set; }
        public decimal DaThanhToan { get; set; } = 0;
        public decimal ConNo { get; set; }
        public decimal TienBVMT { get; set; }
        public decimal TienVAT { get; set; }
        public decimal PhaiThu { get; set; }
        public bool IsThanhToan { get; set; } = false;
        public string GhiChu { get; set; }
        public int MaDVSD { get; set; }
        public int MaTN { get; set; }
        public int MaKN { get; set; }
        public int MaTL { get; set; }
        public int MaKH { get; set; }
        public int MaMB { get; set; }
    }
}
