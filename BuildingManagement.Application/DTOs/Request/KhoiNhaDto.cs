using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs.Request
{
    public class KhoiNhaDto
    {
        public int MaTN { get; set; }
        public string TenTN { get; set; }
        public List<KhoiNhaDetailDto> KhoiNhaDetail { get; set; }
    }

    public class KhoiNhaDetailDto
    {
        public int MaKN { get; set; }
        public string TenKN { get; set; }
        public int MaTN { get; set; }
        public int Status { get; set; }
        public int TotalFloors { get; set; }
        public int TotalPremies { get; set; }
        public decimal OccupancyRate { get; set; }
        public List<ListTangLauInKhoiNha> listTangLauInKhoiNhas { get; set; }

    }

    public class CreateKhoiNhaDto
    {
        public string TenKN { get; set; }
        public int MaTN { get; set; }
        public int TrangThaiKhoiNha { get; set; }
    }

    public class ListTangLauInKhoiNha
    {
        public string TenTL { get; set; }
        public string TenTN { get; set; }
        public string TenKN { get; set; }
        public decimal DienTichSan { get; set; }
        public int TotalPremises { get; set; }
    }

    public class UpdateKhoiNhaDto
    {
        public int MaKN { get; set; }
        public string TenKN { get; set; }
        public int TrangThaiKhoiNha { get; set; }
    }

    public class KhoiNhaFilter
    {
        public int MaTN { get; set; }
        public int MaKN { get; set; }
        public string TenKN { get; set; }
    }
}
