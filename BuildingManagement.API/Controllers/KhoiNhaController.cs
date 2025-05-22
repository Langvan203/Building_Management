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

        [HttpPost("CreateKhoiNha")]
        public async Task<IActionResult> ThemKhoiNha(CreateKhoiNhaDto dto)
        {
            
            var newKN = await _khoiNhaService.CreateKhoiNha(dto, Name);
            return Ok(newKN);
            
        }
        [HttpGet("GetDSKhoiNhaByMaTN")]
        public async Task<IActionResult> LayDSKhoiNhaTheoMaTN(int MaTN)
        {
            
            var dsKN = await _khoiNhaService.GetKhoiNhaByMaTN(MaTN);
            return Ok(dsKN);
        }

        [HttpGet("GetDSKhoiNhaDetail")]
        public async Task<IActionResult> GetDSKhoiNha()
        {
            var dsKN = await _khoiNhaService.GetDSKhoiNhaDetail();
            return Ok(dsKN);
        }

        [HttpGet("GetDSKhoiNhaFilter")]
        public async Task<IActionResult> GetDSKhoiNhaFilter()
        {
            var dsKN = await _khoiNhaService.GetKhoiNhaFilter();
            return Ok(dsKN);
        }

        [HttpDelete("DeleteKhoiNha")]
        public async Task<IActionResult> XoaKhoiNha(int MaKN)
        {
            var result = await _khoiNhaService.DeleteKhoiNha(MaKN);
            if (result)
            {
                return Ok("Xóa thành công");
            }
            return BadRequest("Xóa không thành công");
        }

        [HttpPut("UpdateKhoiNha")]
        public async Task<IActionResult> UpdateKhoiNha(UpdateKhoiNhaDto dto)
        {
            var result = await _khoiNhaService.UpdateKhoiNha(dto, Name);
            if (result)
            {
                return Ok("Cập nhật thành công");
            }
            return BadRequest("Cập nhật không thành công");
        }

    }
}
