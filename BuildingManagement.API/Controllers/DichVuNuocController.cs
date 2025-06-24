using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DichVuNuocController : BaseApiController
    {
        private readonly IDichVuNuocService _dichVuNuocService;

        public DichVuNuocController(IDichVuNuocService dichVuNuocService)
        {
            _dichVuNuocService = dichVuNuocService;
        }

        [HttpGet("GetDSNuocByYearAndMonth")]
        public async Task<IActionResult> GetDSNuocByYearAndMonth(int year, int month)
        {
            var dsNuoc = await _dichVuNuocService.GetDVNuocByMonthAndYear(year, month);
            return Ok(dsNuoc);
        }

        [HttpGet("GetDSNuocByMonthAndYearAndMaDH")]
        public async Task<IActionResult> GetDSNuocByMonthAndYearAndMaDH(int year, int month, int maDH)
        {
            var dsNuoc = await _dichVuNuocService.GetDVNuocByMonthYearAndMaDH(year, month, maDH);
            return Ok(dsNuoc);
        }

        [HttpPost("CreateNewDVNuoc")]
        public async Task<IActionResult> CreateNewDVNuoc(CreateDichVuNuocDto dto)
        {
            var newDVNuoc = await _dichVuNuocService.CreateNewSDNuoc(dto, Name);
            return Ok(newDVNuoc);
        }


    }
}
