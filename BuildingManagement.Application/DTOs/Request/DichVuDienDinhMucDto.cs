using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs.Request
{
    public class DichVuDienDinhMucDto
    {
        public int MaDM { get; set; }
        public string TenDM { get; set; }
        public decimal DonGiaDinhMuc { get; set; }
    }

    public class CreateDichVuDienDinhMucDto
    {
        public string TenDM { get; set; }
        public decimal DonGiaDinhMuc { get; set; }
    }
}
