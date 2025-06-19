using BuildingManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class tnKhoiNha : BaseEntity
    {
        [Key]
        public int MaKN { get; set; }
        public string TenKN { get; set; }
        public int? TrangThaiKhoiNha { get; set; }
        //FK
        public int MaTN { get; set; }
        
        //Navigation
        public tnToaNha tnToaNha { get; set; }

        public ICollection<tnTangLau> tnTangLaus { get; set; }
        public ICollection<tnKhachHang> tnKhachHangs { get; set; }
        public ICollection<dvDichVuSuDung> dvDichVuSuDungs { get; set; }
        public ICollection<dvHoaDon> dvHoaDons { get; set; }
        public ICollection<tnMatBang> tnMatBangs { get; set; }
        public ICollection<dvNuocDongHo> dvNuocDongHos { get; set; }
        public ICollection<dvDienDongHo> dvDienDongHos { get; set; }


    }
}
