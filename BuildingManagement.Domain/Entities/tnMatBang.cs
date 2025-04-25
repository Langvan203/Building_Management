using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class tnMatBang : BaseEntity
    {
        [Key]
        public int MaMB { get; set; }
        public string MaVT { get; set; }
        public decimal DienTichBG { get; set; }
        public decimal DienTichThongThuy { get; set; }
        public decimal DienTichTimTuong { get; set; }
        public bool IsBanGiao { get; set; }
        public string SoHopDong { get; set; }
        public DateTime? NgayBanGiao { get; set; }
        public DateTime? NgayHetHanChoThue { get; set; }

        //FK
        public int MaTL { get; set; }
        public int? MaKH { get; set; }
        public int MaLMB { get; set; }

        // Navigation
        public dvDienDongHo dvDienDongHo { get; set; }  
        public dvNuocDongHo dvNuocDongHo { get; set; }
        public ICollection<dvgxTheXe> dvgxTheXes { get; set; }

        public mbTrangThai mbTrangThai { get; set; }

        public tnTangLau tnTangLau { get; set; }

        public tnKhachHang tnKhachHang { get; set; }
        
        public mbLoaiMB mbLoaiMB { get; set; }

        public ICollection<tnbtHeThong> tnbtHeThongs { get; set; } 
    }
}
