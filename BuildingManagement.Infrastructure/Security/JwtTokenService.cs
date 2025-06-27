using BuildingManagement.Application.Common;
using BuildingManagement.Infrastructure.Data.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BuildingManagement.Infrastructure.Data.Repositories;
using BuildingManagement.Infrastructure.Data.Context;
using BuildingManagement.Application.DTOs.Request.AuthDto;
using BuildingManagement.Application.Interfaces.Repositories;

namespace BuildingManagement.Infrastructure.Security
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtConfiguration _jwtConfig;
        private readonly IUnitOfWork _unitOfWork;
        public JwtTokenService(JwtConfiguration jwtConfig, IUnitOfWork unitOfWork)
        {
            _jwtConfig = jwtConfig;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> CreateToken(LoginDto loginDto)
        {
            var nv = await _unitOfWork.NhanViens.ThongTinNhanVien(loginDto);
            if(nv != null)
            {
                var roles = nv.Roles.Select(x => x.RoleName).ToList();
                var permissions = nv.Roles.SelectMany(x => x.Permissions).ToList();
                var permissionName = permissions.Select(x => x.PermissionName).ToList();
                var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, nv.MaNV.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, nv.MaNV.ToString()),
                new Claim(ClaimTypes.Email, nv.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, nv.TenNV.ToString())

            };
                claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
                foreach(var item in permissionName)
                {
                    claims.Add(new Claim("permissions", item));
                }    
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: _jwtConfig.Issuer,
                    audience: _jwtConfig.Audience,
                    claims = claims,
                    expires: DateTime.UtcNow.AddMonths(1),
                    signingCredentials: creds
                    );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
            {
                var kh = await _unitOfWork.KhachHangs.GetKhachHangInfo(loginDto);
                var claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, kh.MaKH.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, kh.MaKH.ToString()),
                    new Claim(ClaimTypes.Email, kh.Email),
                    new Claim(ClaimTypes.Name, kh.HoTen.ToString())
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: _jwtConfig.Issuer,
                    audience: _jwtConfig.Audience,
                    claims = claims,
                    expires: DateTime.UtcNow.AddMonths(1),
                    signingCredentials: creds
                    );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }    
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _jwtConfig.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtConfig.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret)),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                
            };
            return tokenHandler.ValidateToken(token, parameters, out _);
        }
    }
}
