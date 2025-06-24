using BuildingManagement.Application.DTOs;
using BuildingManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NKBTHeThongController : BaseApiController
    {
        private readonly INKBTHeThongSerivce _nkbtHeThongService;
        public NKBTHeThongController(INKBTHeThongSerivce nkbtHeThongService)
        {
            _nkbtHeThongService = nkbtHeThongService;
        }
        [HttpGet("GetDSHeThong")]
        public async Task<IActionResult> GetDSHeThong(int pageNumber, int pageSize = 15)
        {
            var result = await _nkbtHeThongService.GetDSHeThong(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpPut("UpdateHeThong")]
        public async Task<IActionResult> UpdateHeThong([FromBody] UpdateHeThongDto updateHeThongDto)
        {
            var result = await _nkbtHeThongService.UpdateHeThong(updateHeThongDto, Name);
            if (!result)
            {
                return NotFound("He thong not found.");
            }
            return Ok("He thong updated successfully.");
        }

        [HttpDelete("DeleteHeThong")]
        public async Task<IActionResult> DeleteHeThong(int MaHeThong)
        {
            var result = await _nkbtHeThongService.DeleteHeThong(MaHeThong);
            if (!result)
            {
                return NotFound("He thong not found.");
            }
            return Ok("He thong deleted successfully.");
        }

        [HttpPost("CreateHeThong")]
        public async Task<IActionResult> CreateHeThong([FromBody] CreateHeThong createHeThong)
        {
            var result = await _nkbtHeThongService.CreteNewHeThong(createHeThong, Name);
            if (result == null)
            {
                return BadRequest("He thong already exists.");
            }
            return CreatedAtAction(nameof(GetDSHeThong), new { pageNumber = 1, pageSize = 15 }, result);
        }
    }
}
