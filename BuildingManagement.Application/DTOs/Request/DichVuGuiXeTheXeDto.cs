using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs.Request
{
    public class DichVuGuiXeTheXeDto
    {
        public int MaTX { get; set; }
        public DateTime NgayBatDauSuDung { get; set; }
        public DateTime NgayHetHanSuDung { get; set; }
    }

    public class CreateDichVuGuiXeTheXeDto
    {
        public int MaTX { get; set; }
        public DateTime NgayBatDauSuDung { get; set; }
        public DateTime NgayHetHanSuDung { get; set; }

        //FK
        public int MaLX { get; set; }
        public int MaKH { get; set; }
        public int MaMB { get; set; }
    }
}
