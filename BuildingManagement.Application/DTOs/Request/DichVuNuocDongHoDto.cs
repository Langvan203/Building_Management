using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs.Request
{
    public class DichVuNuocDongHoDto
    {
        public int MaDH { get; set; }
        public string SoDH { get; set; }
        //FK 
        public int MaMB { get; set; }
        public int MaKH { get; set; }
    }

    public class CreateDichVuNuocDongHoDto
    {
        public string SoDH { get; set; }
        //FK 
        public int MaMB { get; set; }
        public int MaKH { get; set; }
    }
}
