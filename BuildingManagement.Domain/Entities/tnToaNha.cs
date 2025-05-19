using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class tnToaNha : BaseEntity
    {
        [Key]
        public int MaTN { get; set; }
        public string TenTN { get; set; }
        public string DiaChi { get; set; }
        public int SoTangNoi { get; set; }
        public int SoTangHam { get; set; }
        public decimal DienTichXayDung { get; set; }
        public decimal TongDienTichSan { get; set; }
        public decimal TongDienTichChoThueNET { get; set; }
        public decimal TongDienTichChoThueGross { get; set; }
        public string NganHangThanhToan { get; set; }
        public string SoTaiKhoan { get; set; }
        public string NoiDungChuyenKhoan { get; set; }
        public int? TrangThaiToaNha { get; set; }
       

        // Navigation
        public ICollection<tnNhanVien> tnNhanViens { get; set; }
        public ICollection<tnMatBang> tnMatBangs { get; set; }
        public ICollection<tnTangLau> tnTangLaus { get; set; }
        public ICollection<tnKhachHang> tnKhachHangs { get; set; }
        public ICollection<tnKhoiNha> tnKhoiNhas { get; set; }
        public ICollection<dvLoaiDV> dvLoaiDVs { get; set; }
        public ICollection<dvDichVu> dvDichVus { get; set; }
        public ICollection<dvDichVuSuDung> dvDichVuSuDungs { get; set; }
        public ICollection<dvHoaDon> dvHoaDons { get; set; }
        public ICollection<tnycYeuCauSuaChua> tnycYeuCauSuaChuas { get; set; }
        public ICollection<tnbtHeThong> tnbtHeThongs { get; set; }

    }
}
