using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs
{
    public class NhanVienDto
    {
        public int MaNV { get; set; }
        public string TenNV { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string DiaChiThuongTru { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string SDT { get; set; }
    }

    public class CreateNhanVienDto
    {
        public string TenNV { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string DiaChiThuongTru { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string SDT { get; set; }
        public string Password { get; set; }
        public List<int>? MaPBs { get; set; } = new List<int>();
        public List<int>? MaTNs { get; set; } = new List<int>();
    }

    public class UpdateThongTinNhanVien
    {
        public int MaNV { get; set; }
        public string HoTen { get; set; }
        public string TenDangNhap { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
    }

    public class GetDSNhanVienDto
    {
        public int MaNV { get; set; }
        public string TenNV { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string DiaChiThuongTru { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string SDT { get; set; }
        public List<NhanVienInToaNha> toaNhas { get; set; }
        public List<NhanVienPhongBan> phongBans { get; set; }
        public List<NhanVienRoles> Roles { get; set; }
    }

    public class NhanVienInToaNha
    {
        public string MaTN { get; set; }
        public string TenTN { get; set; }
    }

    public class NhanVienPhongBan
    {
        public string MaPB { get; set; }
        public string TenPB { get; set; }
        public string TenTN { get; set; }
    }
    public class NhanVienRoles
    {
        public string RoleID { get; set; }
        public string RoleName { get; set; }
    }

    public class UpdateNhanVienToaNha
    {
        public int MaNV { get; set; }
        public List<int> dsToaNha { get; set; }
    }

    public class UpdateNhanVienPhongBan
    {
        public int MaNV { get; set; }
        public List<int> dsPhongBan { get; set; }
    }

    public class UpdateNhanVienRole
    {
        public int MaNV { get; set; }
        public List<int> dsRole { get; set; }
    }
}
