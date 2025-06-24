using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs.Request
{
    public class ForgotPasswordDto
    {
        //public string Email { get; set; }
        public string to { get; set; }
        public string subject { get; set; }
        public string htmlContent { get; set; }
    }
}
