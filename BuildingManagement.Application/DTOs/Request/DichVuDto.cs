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
        public int MaLDV { get; set; }
        public int MaTN { get; set; }
    }
}
