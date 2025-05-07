using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs.Request
{
    public class DichVuGuiXeLoaiXeDto
    {
        public int MaLX { get; set; }
        public string TenLX { get; set; }
        public decimal DonGia { get; set; }
    }

    public class CreateDichVuGuiXeLoaiXeDto
    {
        public string TenLX { get; set; }
        public decimal DonGia { get; set; }
    }
}
