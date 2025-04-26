using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class dvgxTheXe : BaseEntity
    {
        [Key]
        public int MaTX { get; set; }
        public DateTime NgayBatDauSuDung { get; set; } 
        public DateTime NgayHetHanSuDung { get; set; }

        //FK
        public int MaLX { get; set; }
        public int MaKH { get; set; }
        public int MaMB { get; set; }
        //Navigation
        public dvgxLoaiXe dvgxLoaiXe {  get; set; }   
        public tnKhachHang tnKhachHang { get; set; }
        public tnMatBang tnMatBang { get; set; }
    }
}
