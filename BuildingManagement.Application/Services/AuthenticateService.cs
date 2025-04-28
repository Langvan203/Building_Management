using AutoMapper;
using BuildingManagement.Application.Common;
using BuildingManagement.Application.DTOs.Request.AuthDto;
using BuildingManagement.Application.DTOs.Response;
using BuildingManagement.Application.Interfaces.Repositories;
using BuildingManagement.Application.Interfaces.Services;
using BuildingManagement.Domain.Entities;
using BuildingManagement.Infrastructure.Ultility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BuildingManagement.Application.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenService _JwtTokenService;
        private readonly IMapper _mapper;

        public AuthenticateService(IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _JwtTokenService = jwtTokenService;
            _mapper = mapper;
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
            return new LoginResponseDto
            {
                Token = token,
                TenNV = nv.TenNV,
                SDT = nv.SDT,
                Email = nv.Email,
                UserName = nv.UserName
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
    }
}
