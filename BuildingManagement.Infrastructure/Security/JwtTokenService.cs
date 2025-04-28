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
            var roles = nv.Roles.Select(x => x.RoleName).ToList();
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, nv.MaNV.ToString()),
                new Claim(ClaimTypes.Email, nv.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Name, nv.TenNV.ToString())
            };
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                claims = claims,
                expires: DateTime.UtcNow.AddHours(_jwtConfig.ExpiryHours),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token); 
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _jwtConfig.Issuer,
                ValidateActor = true,
                ValidAudience = _jwtConfig.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret)),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            return tokenHandler.ValidateToken(token, parameters, out _);
        }
    }
}
