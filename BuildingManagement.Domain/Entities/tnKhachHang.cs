using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class tnKhachHang : BaseEntity
    {
        [Key]
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

        // FK

        // Navigation
        public ICollection<tnMatBang> tnMatBangs { get; set; }
        public ICollection<tnycYeuCauSuaChua> tnycYeuCauSuaChuas { get; set; }
        public ICollection<dvgxTheXe> dvgxTheXes { get; set; }
        public ICollection<dvDichVuSuDung> dvDichVuSuDungs { get; set; }

        public dvDienDongHo dvDienDongHo { get; set; }
        public dvNuocDongHo dvNuocDongHo { get; set; }
    }
}
