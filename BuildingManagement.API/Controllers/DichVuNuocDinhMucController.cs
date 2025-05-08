using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DichVuNuocDinhMucController : BaseApiController
    {
        private readonly IDichVuNuocDinhMucService _nuocDinhMucService;
        public DichVuNuocDinhMucController(IDichVuNuocDinhMucService nuocDinhMucService)
        {
            _nuocDinhMucService = nuocDinhMucService;
        }
        [HttpGet("GetDSDinhMucNuoc")]
        public async Task<IActionResult> GetDSDinhMucNuoc()
        {
            var dsDinhMuc = await _nuocDinhMucService.GetDSDinhMucNuoc();
            return Ok(dsDinhMuc);
        }
        [HttpPost("CreateNewDinhMuc")]
        public async Task<IActionResult> CreateNewDinhMuc(CreateDichVuNuocDinhMucDto dto)
        {
            var newDinhMuc = await _nuocDinhMucService.CreateNewDinhMuc(dto, Name);
            return Ok(newDinhMuc);
        }
    }
}
