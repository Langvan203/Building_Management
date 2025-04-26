using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class dvDichVuSuDung : BaseEntity
    {
        [Key]
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
        //Navigation 
        public dvDichVu dvDichVu { get; set; }
        public ICollection<dvHoaDon> dvHoaDons { get; set; }
        public tnKhachHang tnKhachHang { get; set; }
        public tnMatBang tnMatBang { get; set; }
    }
}
