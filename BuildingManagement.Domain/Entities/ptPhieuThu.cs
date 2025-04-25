using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class ptPhieuThu : BaseEntity
    {
        [Key]
        public int MaPT { get; set; }
        public string HinhThucThanhToan { get; set; }
        public string SoTaiKhoan { get; set; }
        public string NganHang { get; set; }
        public string GhiChu { get; set; }
        public DateTime NgayThu { get; set; }
        public decimal SoTien { get; set; }
        public string NguoiThu { get; set; }

        //FK
        public int MaHD { get; set; }

        //Navigation 
        public dvHoaDon dvHoaDon { get; set; }
    }
}
