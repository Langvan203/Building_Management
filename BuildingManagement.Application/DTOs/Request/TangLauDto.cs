using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs.Request
{
    public class TangLauDto
    {
        public int MaTL { get; set; }
        public string TenTL { get; set; }
        public decimal DienTichSan { get; set; }
        public decimal DienTichKhuVucDungChung { get; set; }
        public decimal DienTichKyThuaPhuTro { get; set; }
    }

    public class CreateTangLauDto
    {
        public string TenTL { get; set; }
        public decimal DienTichSan { get; set; }
        public decimal DienTichKhuVucDungChung { get; set; }
        public decimal DienTichKyThuaPhuTro { get; set; }
    }
}
