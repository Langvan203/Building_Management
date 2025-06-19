using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DichVuDienDongHoController : BaseApiController
    {
        private readonly IDichVuDienDongHoService _dichVuDienDongHoService;

        public DichVuDienDongHoController(IDichVuDienDongHoService dichVuDienDongHoService)
        {
            _dichVuDienDongHoService = dichVuDienDongHoService;
        }

        [HttpGet("GetDSDienDongHo")]
        public async Task<IActionResult> GetDSDongHo(int pageNumber)
        {
            var dsDongHo = await _dichVuDienDongHoService.GetDSDienDongHo(pageNumber);
            return Ok(dsDongHo);
        }

        [HttpPost("CreateDienDongHo")]
        public async Task<IActionResult> CreateNewDongHo(CreateDongHoDto dto)
        {
            var newDongHo = await _dichVuDienDongHoService.CreateDienDongHo(dto, Name);
            if (newDongHo != null)
            {
                return Ok("Thêm mới đồng hồ điện thành công");
            }
            return BadRequest("Thêm mới đồng hồ điện thất bại, có thể đã tồn tại đồng hồ điện với số đồng hồ và mã MB này");
        }

        [HttpPut("UpdateDienDongHo")]
        public async Task<IActionResult> UpdateDongHoDien(UpdateDongHoDto dto)
        {
            var checkDongHoDien = await _dichVuDienDongHoService.UpdateDienDongHo(dto, Name);
            if (checkDongHoDien != null)
            {
                return Ok("Cập nhật đồng hồ điện thành công");
            }
            return BadRequest("Không thể cập nhật đồng hồ điện này");
        }
        [HttpDelete("RemoveDienDongHo/{MaDH}")]
        public async Task<IActionResult> RemoveDongHoDien(int MaDH)
        {
            var checkRemove = await _dichVuDienDongHoService.RemoveDienDongHo(MaDH);
            if (checkRemove)
            {
                return Ok("Xóa đồng hồ điện thành công");
            }
            return BadRequest("Không thể xóa đồng hồ điện này, có thể không tồn tại");
        }
    }
}
