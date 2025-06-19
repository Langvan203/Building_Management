using BuildingManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DichVuHoaDonController : BaseApiController
    {
        private readonly IDichVuHoaDonService _dichVuHoaDonService;
        public DichVuHoaDonController(IDichVuHoaDonService dichVuHoaDonService)
        {
            _dichVuHoaDonService = dichVuHoaDonService;
        }

        [HttpGet("GetDSHoaDon")]
        public async Task<IActionResult> GetDSHoaDon()
        {
            var dsHoaDon = await _dichVuHoaDonService.GetDSHoaDon();
            return Ok(dsHoaDon);
        }
    }
}
