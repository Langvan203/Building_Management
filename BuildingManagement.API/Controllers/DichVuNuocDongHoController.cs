using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DichVuNuocDongHoController : BaseApiController
    {
        private readonly IDichVuNuocDongHoService _nuocDongHoService;

        public DichVuNuocDongHoController(IDichVuNuocDongHoService nuocDongHoService)
        {
            _nuocDongHoService = nuocDongHoService;
        }

        [HttpGet("GetDSNuocDongHo")]
        public async Task<IActionResult> GetDSDongHo(int pageNumber)
        {
            var dsDongHo = await _nuocDongHoService.GetDSNuocDongHo(pageNumber);
            return Ok(dsDongHo);
        }

        [HttpPost("CreateNuocDongHo")]
        public async Task<IActionResult> CreateNewDongHo(CreateDongHoDto dto)
        {
            var newDongHo = await _nuocDongHoService.CreateNuocDongHo(dto, Name);
            return Ok(newDongHo);
        }

        [HttpPut("UpdateNuocDongHo")]
        public async Task<IActionResult> UpdateDongHoNuoc(UpdateDongHoDto dto)
        {
            var checkDongHoNuoc = await _nuocDongHoService.UpdateNuocDongHo(dto, Name);
            if (checkDongHoNuoc)
            {
                return Ok("Cập nhật đồng hồ nước thành công");
            }
            return BadRequest("Không thể cập nhật đồng hồ nước này");
        }

        [HttpDelete("RemoveNuocDongHo/{MaDH}")]
        public async Task<IActionResult> RemoveDongHoNuoc(int MaDH)
        {
            var checkRemove = await _nuocDongHoService.RemoveNuocDongHo(MaDH);
            if (checkRemove)
            {
                return Ok("Xóa đồng hồ nước thành công");
            }
            return BadRequest("Không thể xóa đồng hồ nước này, có thể không tồn tại");
        }

        [HttpPost("UpdateChiSoMoi")]
        public async Task<IActionResult> GhiChiSoMoi(int MaDH, int ChiSoMoi)
        {
            var checkGhiChiSo = await _nuocDongHoService.GhiChiSoMoi(MaDH, ChiSoMoi, Name);
            if (checkGhiChiSo)
            {
                return Ok("Ghi chỉ số mới thành công");
            }
            return BadRequest("Không thể ghi chỉ số mới, có thể đồng hồ không tồn tại hoặc chỉ số mới không hợp lệ");
        }

        [HttpPost("UpdateTrangThai")]
        public async Task<IActionResult> UpdateTrangThai(int MaDH, bool TrangThai)
        {
            var checkUpdateTrangThai = await _nuocDongHoService.UpdateTrangThai(MaDH, TrangThai, Name);
            if (checkUpdateTrangThai)
            {
                return Ok("Cập nhật trạng thái đồng hồ thành công");
            }
            return BadRequest("Không thể cập nhật trạng thái đồng hồ này, có thể không tồn tại");
        }
    }
}
