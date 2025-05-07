using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs
{
    public class DichVuDienDongHoDto
    {
        public int MaDH { get; set; }
        public string SoDongHo { get; set; }
    }

    public class CreateDichVuDienDongHoDto
    {
        public int MaDH { get; set; }
        public string SoDongHo { get; set; }
        public int MaMB { get; set; }
    }
}
