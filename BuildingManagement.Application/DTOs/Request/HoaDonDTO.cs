using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs.Request
{
    public class HoaDonDTO
    {
        public int MaHD { get; set; }
        public decimal ThueVAT { get; set; }
        public decimal ThueBVMT { get; set; }
        public decimal TienTruocVAT { get; set; }
        public decimal DaThanhToan { get; set; }
        public decimal ConNo { get; set; }
        public decimal TienBVMT { get; set; }
        public decimal PhaiThu { get; set; }
        public bool IsThanhToan { get; set; }
        public string GhiChu { get; set; }
        public int MaDVSD { get; set; }
        public string TenDVSD { get; set; }
        public int? MaTN { get; set; }
        public string TenTN { get; set; }
        public string NganHangThanhToan { get; set; }
        public string SoTaiKhoan { get; set; }
        public int? MaKH { get; set; }
        public int? MaMB { get; set; }
        public List<DichVuSuDungHoaDon> dichVuSuDungHoaDons { get; set; } = new List<DichVuSuDungHoaDon>();
        public List<DichVuNuoc> dichVuNuocs { get; set; } = new List<DichVuNuoc>();
    }

    public class DichVuSuDungHoaDon
    {
        public int MaDVSD { get; set; }
        public DateTime NgayBatDauTinhPhi { get; set; }
        public DateTime NgayKetThucTinhPhi { get; set; }
        public decimal TienVAT { get; set; }
        public bool IsDuyet { get; set; }
        public decimal TienBVMT { get; set; }
        public decimal ThanhTien { get; set; }
        public string GhiChu { get; set; }
        // FK
        public int MaDV { get; set; }
        public string TenDV { get; set; }
        public int? MaKH { get; set; }
        public int MaMB { get; set; }
    }

    public class DichVuNuoc
    {
        public int MaNuoc { get; set; }
        public int MaDM { get; set; }
        public int ChiSoDau { get; set; }
        public int ChiSoCuoi { get; set; }
        public int SoTieuThu { get; set; }
        public decimal ThanhTien { get; set; }

    }
    public class DichVuDien
    {
        public int MaDien { get; set; }
        public int MaDM { get; set; }
        public int ChiSoDau { get; set; }
        public int ChiSoCuoi { get; set; }
        public int SoTieuThu { get; set; }
        public decimal ThanhTien { get; set; }

    }

    public class GetHoaDon
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
        public string TenDVSD { get; set; }
        public int MaTN { get; set; }
        public int MaKN { get; set; }
        public int MaTL { get; set; }
        public int MaKH { get; set; }
        public int MaMB { get; set; }
        public string TenTN { get; set; }
        public string TenKN { get; set; }
        public string TenTL { get; set; }
        public string TenKH { get; set; }
        public string MaVT { get; set; }
        public DateTime NgayTaoHoaDon { get; set; } = DateTime.Now;
        public string SoTaiKhoan { get; set; }
        public string NganHangThanhToan { get; set; }
    }
}
