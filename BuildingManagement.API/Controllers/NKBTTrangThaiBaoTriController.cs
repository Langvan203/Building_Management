using BuildingManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NKBTTrangThaiBaoTriController : BaseApiController
    {
        private readonly INKBTTrangThaiBaoTriService _trangThaiBaoTriService;
        public NKBTTrangThaiBaoTriController(INKBTTrangThaiBaoTriService trangThaiBaoTriService)
        {
            _trangThaiBaoTriService = trangThaiBaoTriService;
        }
        [HttpGet("GetDSTrangThai")]
        public async Task<IActionResult> GetDSTrangThai()
        {
            var result = await _trangThaiBaoTriService.GetDSTrangThai();
            return Ok(result);
        }

        [HttpGet("GetDSTrangThaiYeuCau")]
        public async Task<IActionResult> GetDSTrangThaiYeuCau()
        {
            var result = await _trangThaiBaoTriService.GetDSTrangThaiYeuCau();
            return Ok(result);
        }
    }
}
