using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs.Request
{
    public class KhachHangDto
    {
        public int MaKH { get; set; }
        public string HoTen { get; set; }
        public string CCCD { get; set; }
        public DateTime NgayCap { get; set; }
        public string NoiCap { get; set; }
        public bool GioiTinh { get; set; }
        public string TaiKhoanCuDan { get; set; }
        public string MatKhauMaHoa { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public bool IsCaNhan { get; set; }
        public string MaSoThue { get; set; }
        public string DiaChiThuongTru { get; set; }
        public string QuocTich { get; set; }
        public string CtyTen { get; set; }
        public string SoFax { get; set; }
    }

    public class CreateKhachHangDto
    {
        public int MaKH { get; set; }
        public string HoTen { get; set; }
        public string CCCD { get; set; }
        public DateTime NgayCap { get; set; }
        public string NoiCap { get; set; }
        public bool GioiTinh { get; set; }
        public string TaiKhoanCuDan { get; set; }
        public string MatKhauMaHoa { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public bool IsCaNhan { get; set; }
        public string MaSoThue { get; set; }
        public string DiaChiThuongTru { get; set; }
        public string QuocTich { get; set; }
        public string CtyTen { get; set; }
        public string SoFax { get; set; }
        public int? MaTN { get; set; }
        public int? MaKN { get; set; }
        public int? MaTL { get; set; }
    }
}
