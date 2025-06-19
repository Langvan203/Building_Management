using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class dvNuocDongHo : BaseEntity
    {
        [Key]
        public int MaDH { get; set; }
        public string SoDongHo { get; set; }
        public int ChiSoSuDung { get; set; }
        public bool TrangThai { get; set; }
        //FK 
        public int MaMB { get; set; }
        public int MaKH { get; set; }
        public int? MaTN { get; set; }
        public int? MaKN { get; set; }
        public int? MaTL { get; set; }
        //Navigation
        public tnMatBang tnMatBang { get; set; }
        public tnToaNha tnToaNha { get; set; }
        public tnKhoiNha tnKhoiNha { get; set; }
        public tnTangLau tnTangLau { get; set; }
        public ICollection<dvNuoc> dvNuocs { get; set; }
        public tnKhachHang tnKhachHang { get; set; }
    }
}
