using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs.Request
{
    public class DongHoDTO
    {
        public int MaDH { get; set; }
        public string SoDongHo { get; set; }
        public int ChiSoSuDung { get; set; }
        public bool TrangThai { get; set; }
        public int MaMB { get; set; }
        public string MaVT { get; set; }
        public string TenKH { get; set; }
        public int MaTN { get; set; }
        public int MaTL { get; set; }
        public int MaKN { get; set; }
        public DateTime UpdatedDate { get; set; }
    }

    public class CreateDongHoDto
    {
        public string SoDongHo { get; set; }
        public int ChiSoSuDung { get; set; }
        public bool TrangThai { get; set; } = true;
        public int MaMB { get; set; }
        public int MaKH { get; set; }
        public int MaTN { get; set; }
        public int MaKN { get; set; }
        public int MaTL { get; set; }
    }

    public class UpdateDongHoDto
    {
        public int MaDH { get; set; }
        public int ChiSoSuDung { get; set; }
        public bool TrangThai { get; set; } 
    }
}
