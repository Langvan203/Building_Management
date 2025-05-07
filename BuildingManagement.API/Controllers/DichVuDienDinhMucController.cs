using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DichVuDienDinhMucController : BaseApiController
    {
        private readonly IDichVuDienDinhMucService _dienDinhMucService;
        public DichVuDienDinhMucController(IDichVuDienDinhMucService dienDinhMucService)
        {
            _dienDinhMucService = dienDinhMucService;
        }

        [HttpGet("GetDSDinhMuc")]
        public async Task<IActionResult> GetDSDinhMuc()
        {
            var dsDinhMuc = await _dienDinhMucService.GetDSDinhMucDien();
            return Ok(dsDinhMuc);
        }

        [HttpPost("CreateNewDinhMuc")]
        public async Task<IActionResult> CreateNewDinhMuc(CreateDichVuDienDinhMucDto dto)
        {
            var newDinhMuc = await _dienDinhMucService.CreateNewDinhMuc(dto, Name);
            return Ok(newDinhMuc);
        }
    }
}
