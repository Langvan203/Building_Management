using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs.Request
{
    public class PhongBanDto
    {
        public int MaPB { get; set; }
        public string TenPB { get; set; }
        public int MaTN { get; set; }
        public string TenTN { get; set; }
        public List<NhanVienInPhongBan> NhanVienInPhongBans { get; set; }
    }

    public class NhanVienInPhongBan
    {
        public int MaNV { get; set; }
        public string TenNV { get; set; }
        public string Email { get; set; }
        public string SDT { get; set; }
    }

    public class CreatePhongBanDto
    {
        public string TenPB { get; set; }
        public int MaTN { get; set; }
    }

    public class UpdateDatePhongBanDto
    {
        public int MaPB { get; set; }
        public string TenPB { get; set; }
        public int MaTN { get; set; }
    }
}
