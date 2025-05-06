using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs.Request
{
    public class LoaiDVDto
    {
        public int MaLDV { get; set; }
        public string TenLDV { get; set; }
    }

    public class CreateLoaiDVDto
    {
        public string TenLDV { get; set; }
    }
}
