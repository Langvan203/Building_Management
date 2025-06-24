using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Services.Ultility
{
    public interface IOTPService
    {
        Task<string> GenerateAndSendOTPAsync(string email);
        bool ValidateOTPAsync(string token, string otp);
        string GenerateRandomOTPAsync();
        string GenerateOTPToken(string email, string otp);
    }
}
