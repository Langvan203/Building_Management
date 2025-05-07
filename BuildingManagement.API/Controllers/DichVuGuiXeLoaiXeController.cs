using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DichVuGuiXeLoaiXeController : BaseApiController
    {
        private readonly IDichVuGuiXeLoaiXeService _dichVuGuiXeLoaiXeService;

        public DichVuGuiXeLoaiXeController(IDichVuGuiXeLoaiXeService dichVuGuiXeLoaiXeService)
        {
            _dichVuGuiXeLoaiXeService = dichVuGuiXeLoaiXeService;
        }

        [HttpGet("GetDSLoaiXe")]
        public async Task<IActionResult> GetDSLoaiXe()
        {
            var result = await _dichVuGuiXeLoaiXeService.GetDSLoaiXe();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Không tìm thấy danh sách loại xe");
        }

        [HttpPost("CreateNewLoaiXe")]
        public async Task<IActionResult> CreateNewLoaiXe([FromBody] CreateDichVuGuiXeLoaiXeDto dto)
        {
            var name = User.Identity.Name;
            var result = await _dichVuGuiXeLoaiXeService.CreateNewLoaiXe(dto, name);
            if (result != null)
            {
                return CreatedAtAction(nameof(GetDSLoaiXe), new { id = result.MaLX }, result);
            }
            return BadRequest("Tạo mới loại xe không thành công");
        }
    }
}
