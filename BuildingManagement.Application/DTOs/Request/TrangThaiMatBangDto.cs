using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs.Request
{
    public class TrangThaiMatBangDto
    {
        public int MaTrangThai { get; set; }
        public string TenTrangThai { get; set; }
    }
    public class CreateNewTrangThaiMatBangDto
    {
        public string TenTrangThai { get; set; }
    }
}
