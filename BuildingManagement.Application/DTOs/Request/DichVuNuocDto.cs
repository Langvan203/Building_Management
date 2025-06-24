using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs.Request
{
    public class DichVuNuocDto
    {
        public int MaNuoc { get; set; }
        public decimal ChiSoDau { get; set; }
        public decimal ChiSoCuoi { get; set; }
        public decimal SoTieuThu { get; set; }
        public decimal ThanhTien { get; set; }
        public bool IsThanhToan { get; set; }

        public DateTime NgayBatDauSuDung { get; set; }
        public DateTime NgayDenHang { get; set; }
        //FK
        public int MaDM { get; set; }
        public int MaDH { get; set; }
    }

    public class CreateDichVuNuocDto
    {
        public decimal ChiSoDau { get; set; }
        public decimal ChiSoCuoi { get; set; }
        public decimal SoTieuThu { get; set; }
        public decimal ThanhTien { get; set; }
        public bool IsThanhToan { get; set; } = false;

        public DateTime NgayBatDauSuDung { get; set; }
        public DateTime NgayDenHang { get; set; }
        //FK
        public int MaDM { get; set; }
        public int MaDH { get; set; }
    }
}
