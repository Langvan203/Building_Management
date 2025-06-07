using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiDichVuController : BaseApiController
    {
        private readonly ILoaiDichVuService _dichVuLoaiDichVuService;
        public LoaiDichVuController(ILoaiDichVuService dichVuLoaiDichVuService)
        {
            _dichVuLoaiDichVuService = dichVuLoaiDichVuService;
        }
        [HttpGet("GetDSLoaiDV")]
        public async Task<IActionResult> GetAll()
        {
            var dsLDV = await _dichVuLoaiDichVuService.GetDSLoaiDichVu();
            if (dsLDV == null)
            {
                return NotFound("Không tìm thấy danh sách loại dịch vụ nào");
            }
            return Ok(dsLDV);
        }
        [HttpPost("CreateNewLDV")]
        public async Task<IActionResult> Create(CreateLoaiDVDto createLoaiDVDto)
        {
            var result = await _dichVuLoaiDichVuService.CreateLoaiDichVu(createLoaiDVDto, Name);
            if (result == null)
            {
                return BadRequest("Tạo loại dịch vụ không thành công");
            }
            return CreatedAtAction(nameof(GetAll), new { id = result.TenLDV }, result);
        }
        [HttpDelete("DeleteLoaiDV")]
        public async Task<IActionResult> Delete(int maLDV)
        {
            var result = await _dichVuLoaiDichVuService.DeleteLoaiDichVu(maLDV);
            if (!result)
            {
                return BadRequest("Xóa loại dịch vụ không thành công");
            }
            return Ok("Xóa loại dịch vụ thành công");
        }

        [HttpGet("GetDSLoaiDVByMaTN")]
        public async Task<IActionResult> GetDSLoaiDVByMaTN(int MaTN)
        {
            var dsLDV = await _dichVuLoaiDichVuService.GetDSLoaiDichVuByMaTN(MaTN);
            if (dsLDV == null || dsLDV.Count == 0)
            {
                return NotFound("Không tìm thấy danh sách loại dịch vụ nào cho mã tài nguyên này");
            }
            return Ok(dsLDV);

        }
    }
}
