using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DichVuController : BaseApiController
    {
        private readonly IDichVuService _dichVuService;

        public DichVuController(IDichVuService dichVuService)
        {
            _dichVuService = dichVuService;
        }

        [HttpGet("GetDSDichVu")]
        public async Task<IActionResult> GetDSDichVu()
        {
            var dsDichVu = await _dichVuService.GetDSDichVu();
            return Ok(dsDichVu);
        }

        [HttpPost("CreateNewDichVu")]
        public async Task<IActionResult> CreateNewDichVu(CreateDichVuDto dto)
        {
            var newDichVu = await _dichVuService.CreateNewDichVu(dto, Name);
            return Ok(newDichVu);
        }

        [HttpGet("GetDichVuByMaLDV")]
        public async Task<IActionResult> GetDichVuByMaDV(int MaDV)
        {
            var checkDichVu = await _dichVuService.GetDVByMaLDV(MaDV);
            if (checkDichVu != null)
            {
                return Ok(checkDichVu);
            }
            return NotFound("Không tìm thấy dịch vụ với mã này");
        }

        [HttpDelete("DeleteDichVu")]

        public async Task<IActionResult> DeleteDichVu(int MaDV)
        {
            var result = await _dichVuService.RemoveDichVu(MaDV);
            if (result)
            {
                return Ok("Xóa dịch vụ thành công");
            }
            return BadRequest("Xóa dịch vụ không thành công");
        }
    }
}
