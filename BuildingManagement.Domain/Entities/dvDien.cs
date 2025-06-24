using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class dvDien : BaseEntity
    {
        [Key]
        public int MaDien { get; set; }
        public decimal ChiSoDau { get; set; }
        public decimal ChiSoCuoi { get; set; }
        public decimal SoTieuThu { get; set; }
        public decimal ThanhTien { get; set; }
        public bool IsThanhToan { get; set; }
        public DateTime NgayBatDauSuDung { get; set; }
        public DateTime NgayDenHang { get; set; }
        //FK
        public int MaDH { get; set; }
        public int MaDM { get; set; }
        //Navigation 
        public dvDienDongHo dvDienDongHo { get; set; }
        public dvDienDinhMuc dvDienDinhMuc { get; set; }
    }
}
