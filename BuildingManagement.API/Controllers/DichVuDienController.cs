using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DichVuDienController : BaseApiController
    {
        private readonly IDichVuDienService _dichVuDienService;

        public DichVuDienController(IDichVuDienService dichVuDienService)
        {
            _dichVuDienService = dichVuDienService;
        }

        [HttpGet("GetDVDienByMonthAndYear/{month}/{year}")]
        public async Task<IActionResult> GetDVDienByMonthAndYear(int month, int year)
        {
            var dsDVDien = await _dichVuDienService.GetDVDienByMonthAndYear(month, year);
            return Ok(dsDVDien);
        }

        [HttpGet("GetDVDienByMonthYearAndMaDH/{month}/{year}/{MaDH}")]
        public async Task<IActionResult> GetDVDienByMonthYearAndMaDH(int month, int year, int MaDH)
        {
            var dsDVDien = await _dichVuDienService.GetDVDienByMonthYearAndMaDH(month, year, MaDH);
            return Ok(dsDVDien);
        }

        [HttpPost("CreateNewSDDien")]
        public async Task<IActionResult> CreateNewSDDien([FromBody] CreateDichVuDienDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }
            var newSDDien = await _dichVuDienService.CreateNewSDDien(dto, Name);
            return CreatedAtAction(nameof(GetDVDienByMonthAndYear), new { id = newSDDien.MaDien }, newSDDien);
        }
    }
}
