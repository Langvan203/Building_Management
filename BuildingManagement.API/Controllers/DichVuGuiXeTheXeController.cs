using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DichVuGuiXeTheXeController : BaseApiController
    {
        private readonly IDichVuGuiXeTheXeService _dichVuGuiXeTheXeService;

        public DichVuGuiXeTheXeController(IDichVuGuiXeTheXeService dichVuGuiXeTheXeService)
        {
            _dichVuGuiXeTheXeService = dichVuGuiXeTheXeService;
        }

        [HttpGet("GetDSTheXe")]
        public async Task<IActionResult> GetDSTheXe()
        {
            var dsTheXe = await _dichVuGuiXeTheXeService.GetDSTheXe();
            return Ok(dsTheXe);
        }

        [HttpGet("GetDSTheXeByMaKH/{MaKH}")]
        public async Task<IActionResult> GetDSTheXeByMaKH(int MaKH)
        {
            var dsTheXe = await _dichVuGuiXeTheXeService.GetDSTheXeByMaKH(MaKH);
            return Ok(dsTheXe);
        }

        [HttpGet("GetTheXeByMaLX/{MaLX}")]
        public async Task<IActionResult> GetTheXeByMaLX(int MaLX)
        {
            var dsTheXe = await _dichVuGuiXeTheXeService.GetTheXeByMaLX(MaLX);
            return Ok(dsTheXe);
        }

        [HttpGet("GetTheXeByMaMB/{MaMB}")]
        public async Task<IActionResult> GetTheXeByMaMB(int MaMB)
        {
            var dsTheXe = await _dichVuGuiXeTheXeService.GetTheXeByMaMB(MaMB);
            return Ok(dsTheXe);
        }

        [HttpPost("CreateNewTheXe")]
        public async Task<IActionResult> CreateNewTheXe([FromBody] CreateDichVuGuiXeTheXeDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }
            var newTheXe = await _dichVuGuiXeTheXeService.CreateNewTheXe(dto,Name);
            return CreatedAtAction(nameof(GetDSTheXe), new { id = newTheXe.MaTX }, newTheXe);
        }

    }
}
