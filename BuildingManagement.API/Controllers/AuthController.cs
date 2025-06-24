using BuildingManagement.Application.Common;
using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.DTOs.Request.AuthDto;
using BuildingManagement.Application.Interfaces.Services;
using BuildingManagement.Application.Interfaces.Services.Ultility;
using BuildingManagement.Infrastructure.Data.Repositories;
using MailKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IEmailService _emailService;

        public AuthController(IAuthenticateService authenticateService, IEmailService mailService)
        {
            _authenticateService = authenticateService;
            _emailService = mailService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                var token = await _authenticateService.Login(dto);
                return Ok(token);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        [HttpPost("CustomerLogin")]
        public async Task<IActionResult> CustomerLogin([FromBody] LoginDto dto)
        {
            try
            {
                var token = await _authenticateService.KhachHangLogin(dto);
                return Ok(token);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {
                var newNV = await _authenticateService.Register(dto);
                return Ok(newNV);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        [HttpPost("Request-forgot-password")]
        public async Task<IActionResult> RequestForgotPassword(string email)
        {
            try
            {
                var checkToken = await _authenticateService.RequestForgotPassword(email);
                if (checkToken == null)
                    return BadRequest("Email không tồn tại");
                // Lưu token vào cookie
                Response.Cookies.Append("reset_token", checkToken, new CookieOptions
                {
                    HttpOnly = false,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(15) // Token có hiệu lực trong 15 phút
                });
                return Ok(new { message = "Email đã được gửi" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOTP([FromBody] OTPVerificationModel dto)
        {
            try
            {
                string token = Request.Cookies["reset_token"];
                var isValid = _authenticateService.VerifyOTP(token, dto.OTP);
                if (isValid)
                {
                    string resetToken = _authenticateService.GeneratePasswordResetToken(token);
                    Response.Cookies.Append("password_reset_token", resetToken, new CookieOptions
                    {
                        HttpOnly = false,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        MaxAge = TimeSpan.FromMinutes(15)
                    });
                    return Ok(new { message = "Mã OTP hợp lệ" });
                }
                else
                {
                    return BadRequest(new { message = "Mã OTP không hợp lệ" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel dto)
        {
            try
            {
                string resetToken = Request.Cookies["password_reset_token"];
                var email = _authenticateService.ValidateAndGetEmailFromToken(resetToken);
                if (email == null)
                    return BadRequest("Token không hợp lệ");
                var result = await _authenticateService.ResetPassword(email, dto);
                if (result)
                {
                    Response.Cookies.Delete("reset_token", new CookieOptions
                    {
                        Domain = "localhost",
                        Path = "/",
                        SameSite = SameSiteMode.None,
                        HttpOnly = false,
                        Secure = true,
                    });
                    Response.Cookies.Delete("password_reset_token", new CookieOptions
                    {
                        Domain = "localhost",
                        Path = "/",
                        SameSite = SameSiteMode.None,
                        HttpOnly = false,
                        Secure = true,
                    });
                    return Ok(new { message = "Đặt lại mật khẩu thành công" });
                }    
                else
                    return BadRequest("Đặt lại mật khẩu thất bại");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
