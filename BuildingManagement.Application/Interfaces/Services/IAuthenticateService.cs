using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.DTOs.Request.AuthDto;
using BuildingManagement.Application.DTOs.Response;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Services
{
    public interface IAuthenticateService
    {
        Task<LoginResponseDto> Login(LoginDto loginDto);
        Task<KhacHangLoginResponseDto> KhachHangLogin(LoginDto dto);
        Task<RegisterResponseDto> Register(RegisterDto registerDto);
        bool VerifyOTP(string email, string token);
        Task<string> RequestForgotPassword(string email);
        string ValidateAndGetEmailFromToken(string token);
        string GeneratePasswordResetToken(string otpToken);
        Task<bool> ResetPassword(string email,ResetPasswordModel model);
    }
}
