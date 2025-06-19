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
        public async Task<IActionResult> GetDSDongHo()
        {
            var dsDongHo = await _nuocDongHoService.GetDSDongHo();
            return Ok(dsDongHo);
        }

        [HttpPost("CreateNuocDongHo")]
        public async Task<IActionResult> CreateNewDongHo(CreateDongHoDto dto)
        {
            var newDongHo = await _nuocDongHoService.CreateNewDongHo(dto, Name);
            return Ok(newDongHo);
        }

        [HttpGet("GetDongHoNuocByMaMB")]
        public async Task<IActionResult> GetDongHoNuocByMaMB(int MaMB)
        {
            var checkDongHoNuoc = await _nuocDongHoService.GetDongHoNuocByMaMB(MaMB);
            if (checkDongHoNuoc != null)
            {
                return Ok(checkDongHoNuoc);
            }
            return NotFound("Không tìm thấy đồng hồ nước với mã MB này");
        }
    }
}
