using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class dvHoaDon : BaseEntity
    {
        [Key]
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

        //FK
        public int MaDVSD { get; set; }

        // Navigation 
        public dvDichVuSuDung dvDichVuSuDung { get; set; }
        public ICollection<ptPhieuThu> ptPhieuThus { get; set; }
    }
}
