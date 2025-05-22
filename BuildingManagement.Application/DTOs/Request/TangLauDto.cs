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
        public int MaKN { get; set; }
        public int MaTN { get; set; }
        public List<ListMatBangInTanLau> listMatBangInTanLaus { get; set; }
    }

    public class CreateTangLauDto
    {
        public string TenTL { get; set; }
        public decimal DienTichSan { get; set; }
        public decimal DienTichKhuVucDungChung { get; set; }
        public decimal DienTichKyThuaPhuTro { get; set; }
        public int MaKN { get; set; }
        public int MaTN { get; set; }
    }

    public class UpdateTangLauDto
    {
        public int MaTL { get; set; }
        public string TenTL { get; set; }
        public decimal DienTichSan { get; set; }
        public decimal DienTichKhuVucDungChung { get; set; }
        public decimal DienTichKyThuaPhuTro { get; set; }
    }

    public class TangLauFilter
    {
        public int MaTL { get; set; }
        public int MaKN { get; set; }
        public string TenTL { get; set; }
    }

        public class ListMatBangInTanLau
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int FloorId { get; set; }
        public decimal Area { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
    }
}
