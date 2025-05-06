using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs.Request
{
    public class MatBangDto
    {
        public int MaMB { get; set; }
        public string MaVT { get; set; }
        public decimal DienTichBG { get; set; }
        public decimal DienTichThongThuy { get; set; }
        public decimal DienTichTimTuong { get; set; }
        public bool IsBanGiao { get; set; }
        public string SoHopDong { get; set; }
        public DateTime? NgayBanGiao { get; set; }
        public DateTime? NgayHetHanChoThue { get; set; }
    }

    public class CreateMatBangDto
    {
        public string MaVT { get; set; }
        public decimal DienTichBG { get; set; }
        public decimal DienTichThongThuy { get; set; }
        public decimal DienTichTimTuong { get; set; }
        public bool IsBanGiao { get; set; } = false;
        public string SoHopDong { get; set; }
        public DateTime? NgayBanGiao { get; set; }
        public DateTime? NgayHetHanChoThue { get; set; }
        public int MaTL { get; set; }
        public int MaLMB { get; set; }
        public int MaTrangThai { get; set; }
        public int MaTN { get; set; }
    }

    public class UpdateThongTinCoBanMatBangDto
    {
        public int MaMB { get; set; }
        public decimal DienTichBG { get; set; }
        public decimal DienTichThongThuy { get; set; }
        public decimal DienTichTimTuong { get; set; }
        public DateTime? NgayBanGiao { get; set; }
        public DateTime? NgayHetHanChoThue { get; set; }
    }

}
