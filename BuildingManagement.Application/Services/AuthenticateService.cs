using AutoMapper;
using BuildingManagement.Application.Common;
using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.DTOs.Request.AuthDto;
using BuildingManagement.Application.DTOs.Response;
using BuildingManagement.Application.Interfaces.Repositories;
using BuildingManagement.Application.Interfaces.Services;
using BuildingManagement.Application.Interfaces.Services.Ultility;
using BuildingManagement.Application.Services.Ultility;
using BuildingManagement.Domain.Entities;
using BuildingManagement.Infrastructure.Ultility;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace BuildingManagement.Application.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenService _JwtTokenService;
        private readonly IOTPService _OTPService;
        private readonly IMapper _mapper;
        private readonly OTPConfiguration _otpConfig;

        public AuthenticateService(IOptions<OTPConfiguration> otpConfig,IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService, IMapper mapper, IOTPService oTPService)
        {
            _unitOfWork = unitOfWork;
            _JwtTokenService = jwtTokenService;
            _OTPService = oTPService;
            _otpConfig = otpConfig.Value;
            _mapper = mapper;
        }

        public string GeneratePasswordResetToken(string otpToken)
        {
            return otpToken;
        }

        public async Task<LoginResponseDto> Login(LoginDto loginDto)
        {
            var passwordHashed = HashPassWord.HashPassword(loginDto.Password);
            var nv = await _unitOfWork.NhanViens.ThongTinNhanVien(loginDto);
            if(nv == null)
            {
                throw new UnauthorizedAccessException("Email sai hoặc mật khẩu không đúng");
            }
            var token = await _JwtTokenService.CreateToken(loginDto);
            var nvRole = nv.Roles;
            return new LoginResponseDto
            {
                AccessToken = token,
                TenNV = nv.TenNV,
                SDT = nv.SDT,
                Email = nv.Email,
                UserName = nv.UserName,
                RoleName = nvRole != null ? nvRole.Select(x => x.RoleName).ToList() : new List<string>(),
            };
        }

        public async Task<RegisterResponseDto> Register(RegisterDto registerDto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var findNV = await _unitOfWork.NhanViens.ExistsAsync(x => x.Email == registerDto.Email && x.SDT == registerDto.SDT);
                if (findNV)
                {
                    throw new Exception("Nhân viên đã tồn tại");
                }
                else
                {
                    var passwordHash = HashPassWord.HashPassword(registerDto.Password);
                    var newNV = _mapper.Map<tnNhanVien>(registerDto);
                    newNV.PasswordHash = passwordHash;
                    await _unitOfWork.NhanViens.AddAsync(newNV);
                    await _unitOfWork.SaveChangesAsync();
                    var defaultRole = await _unitOfWork.Roles.GetByIdAsync(4);
                    if(defaultRole == null)
                        throw new Exception("Không tìm thấy role mặc định (ID = 4)");
                    newNV.Roles ??= new List<Role>();
                    newNV.Roles.Add(defaultRole);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitTransactionAsync();
                    return new RegisterResponseDto
                    {
                        TenNV = newNV.TenNV,
                        UserName = newNV.UserName,
                        Email = newNV.Email,
                        SDT = newNV.SDT,
                    };
                }
            }catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

        }

        public async Task<string> RequestForgotPassword(string email)
        {
            var checkEmail = await _unitOfWork.NhanViens.ExistsAsync(x => x.Email == email);
            if(!checkEmail)
            {
                throw new UnauthorizedAccessException("Email không tồn tại");
            }
            var token = await _OTPService.GenerateAndSendOTPAsync(email);
            return token;
        }

        public async Task<bool> ResetPassword(string email, ResetPasswordModel model)
        {
            var checkEmail = await _unitOfWork.NhanViens.ExistsAsync(x => x.Email == email);
            if (!checkEmail)
            {
                throw new UnauthorizedAccessException("Email không tồn tại");
            }
            if(model.NewPassword != model.ConfirmPassword)
            {
                throw new UnauthorizedAccessException("Mật khẩu không khớp");
            }
            var passwordHash = HashPassWord.HashPassword(model.NewPassword);
            var nv = await _unitOfWork.NhanViens.GetFirstOrDefaultAsync(x => x.Email.Equals(email));
            if (nv == null)
            {
                throw new UnauthorizedAccessException("Email không tồn tại");
            }
            nv.PasswordHash = passwordHash;
            await _unitOfWork.NhanViens.UpdateAsync(nv);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public string ValidateAndGetEmailFromToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_otpConfig.Secret);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                return principal.FindFirstValue(ClaimTypes.Email);
            }
            catch
            {
                return null;
            }
        }

        public bool VerifyOTP(string token, string otp)
        {
            var isValid = _OTPService.ValidateOTPAsync(token, otp);
            if (!isValid)
            {
                throw new UnauthorizedAccessException("Mã OTP không hợp lệ");
            }
            return isValid;
        }
        
        
    }
}
