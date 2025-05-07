using BuildingManagement.Application.DTOs;
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

        [HttpGet("GetDSDongHo")]
        public async Task<IActionResult> GetDSDongHo()
        {
            var dsDongHo = await _dichVuDienDongHoService.GetDSDongHo();
            return Ok(dsDongHo);
        }

        [HttpPost("CreateNewDongHo")]
        public async Task<IActionResult> CreateNewDongHo(CreateDichVuDienDongHoDto dto)
        {
            var newDongHo = await _dichVuDienDongHoService.CreateNewDongHo(dto, Name);
            return Ok(newDongHo);
        }

        [HttpGet("GetDongHoDienByMaMB")]
        public async Task<IActionResult> GetDongHoDienByMaMB(int MaMB)
        {
            var checkDongHoDien = await _dichVuDienDongHoService.GetDongHoDienByMaMB(MaMB);
            if (checkDongHoDien != null)
            {
                return Ok(checkDongHoDien);
            }
            return NotFound("Không tìm thấy đồng hồ điện với mã MB này");
        }
    }
}
