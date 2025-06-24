using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs.Request
{
    public class DichVuDto
    {
        public int MaDV { get; set; }
        public string TenDV { get; set; }
        public int MaLDV { get; set; }
        public decimal TyLeBVMT { get; set; }
        public decimal DonGia { get; set; }
        public decimal TyLeVAT { get; set; }
    }

    public class CreateDichVuDto
    {
        public string TenDV { get; set; }
        public decimal TyLeBVMT { get; set; }
        public decimal DonGia { get; set; }
        public decimal TyLeVAT { get; set; }
        public string DonViTinh { get; set; }
        public int KyThanhToan { get; set; }
        public bool IsThanhToanTheoKy { get; set; }
        public int MaLDV { get; set; }
    }

    public class GetDSDichVu
    {
        public int id { get; set; }
        public string tenDV { get; set; }
        public int maLDV { get; set; }
        public decimal donGia { get; set; }
        public decimal tyLeBVMT { get; set; }
        public decimal tyLeVAT { get; set; }
        public string donViTinh { get; set; }
        public int kyThanhToan { get; set; }
        public bool isThanhToanTheoKy { get; set; }
    }
}
