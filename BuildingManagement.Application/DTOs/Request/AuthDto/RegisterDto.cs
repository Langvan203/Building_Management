using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs.Request.AuthDto
{
    public class RegisterDto
    {
        public string TenNV { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string DiaChiThuongTru { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string SDT { get; set; }
        public string Password { get; set; }
    }
}
