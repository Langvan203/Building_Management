using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class tnycYeuCauSuaChua : BaseEntity
    {
        [Key]
        public int MaYC { get; set; }
        public string NguoiYeuCau { get; set; }
        public int? MucDoYeuCau { get; set; }

        //FK
        public int MaHeThong { get; set; }
        public int MaKH { get; set; }
        public int MaTN { get; set; }
        public int IdTrangThai { get; set; }
        public int? MaMB { get; set; }

        //Navigation
        public tnKhachHang tnKhachHang { get; set; }
        public tnbtHeThong tnbtHeThong { get; set; }    
        public tnToaNha tnToaNha { get; set; }
        public tnycTrangThai tnycTrangThai { get; set; }
        public tnMatBang tnMatBang { get; set; } 
    }
}
