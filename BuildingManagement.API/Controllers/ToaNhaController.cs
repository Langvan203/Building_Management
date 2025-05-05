using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToaNhaController : BaseApiController
    {
        private readonly IToaNhaServices _toaNhaServices;
        public ToaNhaController(IToaNhaServices toaNhaServices) 
        {
            _toaNhaServices = toaNhaServices;    
        }

        [HttpPost("them-toa-nha")]
        [Authorize(Roles = "Nhân viên tòa nhà")]
        public async Task<IActionResult> CreateNewToaNha([FromBody]CreateToaNhaDto dto)
        {
            
            var newTn = await _toaNhaServices.TaoToaNhaAsync(dto, Name);
            return Ok(newTn);
            
        }

        [HttpGet("lay-ds-toa-nha")]
        [Authorize(Roles = "Nhân viên tòa nhà")]
        public async Task<IActionResult> GetDSToaNha()
        {
         
            var dstn = await _toaNhaServices.GetDSToaNhaAsync();
            return Ok(dstn);
            
        }
    }
}
