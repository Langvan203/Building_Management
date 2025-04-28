using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs.Response
{
    public class LoginResponseDto
    {
        public string TenNV { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string SDT { get; set; }
        public string Token { get; set; }
    }
}
