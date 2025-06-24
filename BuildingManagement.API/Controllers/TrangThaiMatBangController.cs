using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrangThaiMatBangController : BaseApiController
    {
        private readonly ITrangThaiMatBangService _trangThaiMatBangService;
        public TrangThaiMatBangController(ITrangThaiMatBangService trangThaiMatBangService)
        {
            _trangThaiMatBangService = trangThaiMatBangService;
        }

        [HttpGet("GetDSTrangThaiMatBang")]
        public async Task<IActionResult> GetDSTrangThaiMB()
        {
            var dsTrangThaiMB = await _trangThaiMatBangService.GetDSTrangThaiMatBang();
            return Ok(dsTrangThaiMB);
        }

        [HttpPost("CreateTrangThaiMB")]
        public async Task<IActionResult> CreateNewTrangThai(CreateNewTrangThaiMatBangDto dto)
        {
            var newTrangThaiMB = await _trangThaiMatBangService.CreateNewTrangThaiMB(dto, Name);
            return Ok(newTrangThaiMB);
        }

        [HttpDelete("RemoveTrangThai")]
        public async Task<IActionResult> RemoveTrangThai(int maTT)
        {
            var removeTrangThaiMB = await _trangThaiMatBangService.RemoveTrangThai(maTT);
            return Ok(removeTrangThaiMB);
        }
    }
}
