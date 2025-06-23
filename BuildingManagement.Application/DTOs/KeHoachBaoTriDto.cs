using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs
{
    public class KeHoachBaoTriDto
    {
        public int MaKeHoach { get; set; }
        public string TenKeHoach { get; set; }
        public int MaHeThong { get; set; }
        public string TenHeThong { get; set; }
        public DateTime NgayBaoTri { get; set; }
        public int TanSuat { get; set; }
        public int LoaiBaoTri { get; set; } // 0: Bảo trì định kỳ, 1: Bảo trì đột xuất
        public List<NhanVienInBaoTri> nhanVienInBaoTris { get; set; } = new List<NhanVienInBaoTri>();
        public int TrangThai { get; set; }
        // detail
        public string MoTa { get; set; }
        public List<ChiTietInKeHoachBaoTri> chiTietInKeHoachBaoTris { get; set; } = new List<ChiTietInKeHoachBaoTri>();
        public List<LichSuBaoTriKeHoach> lichSuBaoTriKeHoaches { get; set; } = new List<LichSuBaoTriKeHoach>();

    }

    public class CreateKeHoachBaoTriDto
    {
        public string TenKeHoach { get; set; }
        public int LoaiBaoTri { get; set; } // 0: Bảo trì định kỳ, 1: Bảo trì đột xuất
        public int MaHeThong { get; set; }
        public int MaTrangThai { get; set; }
        public int TanSuat { get; set; }
        public string MoTaCongViec { get; set; }
        public DateTime NgayBaoTri { get; set; }

        public List<CreateChiTietBaoTriDto>? ChiTietBaoTris { get; set; } = new List<CreateChiTietBaoTriDto>();
        public List<int> DanhSachNhanVien { get; set; } = new List<int>();
    }

    public class NhanVienInBaoTri
    {
        public int MaNV { get; set; }
        public string TenNV { get; set; }
    }

    public class ChiTietInKeHoachBaoTri
    {
        public string GhiChu { get; set; }
        public int MaTrangThai { get; set; } = 0;
    }

    public class LichSuBaoTriKeHoach
    {
        public string TieuDe { get; set; }
        public DateTime NgayCapNhat { get; set; }
        public string NoiDung { get; set; }
    }

    public class GiaoViecChoNhanVien 
    {
        public List<int> MaNV { get; set; }
        public int MaKeHoach { get; set; }
        public bool IsThongBaoNhanVienCu { get; set; } = true; // Thông báo cho nhân viên cũ
        public bool IsThongBaoNhanVienMoi { get; set; } = true; // Thông báo cho nhân viên mới
    }
}
