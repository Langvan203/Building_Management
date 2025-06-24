using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs.Request
{
    public class LoaiMatBangDto
    {
        public int MaLMB { get; set; }
        public string TenLMB { get; set; }
    }

    public class CreateNewLoaiMB
    {
        public string TenLMB { get; set; }
    }
}
