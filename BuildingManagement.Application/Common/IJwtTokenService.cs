using BuildingManagement.Application.DTOs.Request.AuthDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Common
{
    public interface IJwtTokenService
    {
        Task<string> CreateToken(LoginDto loginDto);
        ClaimsPrincipal ValidateToken(string token);
    }
}
