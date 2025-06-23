using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs
{
    public class YeuCauSuaChuaDTO
    {
        public string TieuDe { get; set; }

        public int MaYC { get; set; }
        public int MaTN { get; set; }
        public int MaKN { get; set; }
        public int MaTL { get; set; }
        public int MaMB { get; set; }
        public string TenTN { get; set; }
        public string TenKN { get; set; }
        public string TenTL { get; set; }
        public string MaVT { get; set; }

        public int MaKH { get; set; }
        public string TenKH { get; set; }

        public DateTime NgayYeuCau { get; set; }

        public int? MucDoYeuCau { get; set; }

        public int IdTrangThai { get; set; }
        public string TenTrangThai { get; set; }

        public string NguoiYeuCau { get; set; }

        public string MoTa { get; set; }

        public string? ImagePath { get; set; }

        public string? GhiChu { get; set; }

        public int MaHeThong { get; set; }
        public string TenHeThong { get; set; }

        public List<NhanVienInYeuCau> NhanVienInYeuCaus { get; set; } = new List<NhanVienInYeuCau>();

    }

    public class NhanVienInYeuCau
    {
        public int MaNV { get; set; }
        public string TenNV { get; set; }
    }
}
