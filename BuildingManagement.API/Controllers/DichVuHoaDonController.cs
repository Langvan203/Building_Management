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
        public async Task<IActionResult> GetDSHoaDon(int pageNumber, DateTime NgayBatDau, DateTime NgayKetThuc, int pageSize = 15)
        {
            var dsHoaDon = await _dichVuHoaDonService.GetDSHoaDon(pageNumber, NgayBatDau,NgayKetThuc,pageSize);
            return Ok(dsHoaDon);
        }

        [HttpGet("GetDSHoaDonByID")]
        public async Task<IActionResult> GetDSHoaDonByID(int pageNumber, DateTime NgayBatDau, DateTime NgayKetThuc, int pageSize = 15)
        {
            var dsHoaDon = await _dichVuHoaDonService.GetDSHoaDonByID(Id, pageNumber, NgayBatDau, NgayKetThuc, pageSize);
            return Ok(dsHoaDon);
        }
    }
}
