using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Services;
using BuildingManagement.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhoiNhaController : BaseApiController
    {
        private readonly IKhoiNhaService _khoiNhaService;
        public KhoiNhaController(IKhoiNhaService khoiNhaService)
        {
            _khoiNhaService = khoiNhaService;   
        }

        [HttpPost("them-khoi-nha")]
        public async Task<IActionResult> ThemKhoiNha(CreateKhoiNhaDto dto)
        {
            
            var newKN = await _khoiNhaService.CreateKhoiNha(dto, Name);
            return Ok(newKN);
            
        }
        [HttpGet("get-ds-khoi-nha")]
        public async Task<IActionResult> LayDSKhoiNhaTheoMaTN(int MaTN)
        {
            
            var dsKN = await _khoiNhaService.GetKhoiNhaByMaTN(MaTN);
            return Ok(dsKN);
           
        }
        
    }
}
