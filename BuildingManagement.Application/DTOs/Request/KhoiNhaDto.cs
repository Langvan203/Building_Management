using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs.Request
{
    public class KhoiNhaDto
    {
        public int MaKN { get; set; }
        public string TenKN { get; set; }
    }

    public class CreateKhoiNhaDto
    {
        public string TenKN { get; set; }
        public int MaTN { get; set; }
    }
}
