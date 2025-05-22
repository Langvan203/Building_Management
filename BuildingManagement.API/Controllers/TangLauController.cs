using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TangLauController : BaseApiController
    {
        private readonly ITangLauServices _TangLauService;
        public TangLauController(ITangLauServices tangLauService)
        {
            _TangLauService = tangLauService;
        }

        [HttpGet("get-ds-tang-lau")]
        public async Task<IActionResult> GetDSTangLau(int MaTN, int MaKN)
        {
            var dsTL = await _TangLauService.GetDSTangLauByMaKN(MaTN, MaKN);
            return Ok(dsTL);
        }

        [HttpPost("CreateTangLau")]
        public async Task<IActionResult> CreateNewTangLau(CreateTangLauDto dto)
        {

            var newTL = await _TangLauService.CreateTangLau(dto, Name);
            return Ok(newTL);
        }

        [HttpGet("GetDSTangLau")]
        public async Task<IActionResult> GetDSTangLau()
        {
            var dsTL = await _TangLauService.GetDSTangLau();
            return Ok(dsTL);
        }

        [HttpPut("UpdateTangLau")]
        public async Task<IActionResult> UpdateTangLau(UpdateTangLauDto tangLauDto)
        {
            var result = await _TangLauService.UpdateTangLau(tangLauDto, Name);
            if (result)
            {
                return Ok("Cập nhật thành công");
            }
            return BadRequest("Cập nhật không thành công");
        }

        [HttpDelete("DeleteTangLau")]
        public async Task<IActionResult> DeleteTangLau(int MaTL)
        {
            var result = await _TangLauService.DeleteTangLau(MaTL);
            if (result)
            {
                return Ok("Xóa thành công");
            }
            return BadRequest("Xóa không thành công");
        }

        [HttpGet("GetDSTangLauByMaKN")]
        public async Task<IActionResult> GetDSTangLauByMaKN(int MaKN)
        {
            var dsTL = await _TangLauService.GetDSTangLauByMaKN(MaKN);
            return Ok(dsTL);
        }

        [HttpGet("GetDSTangLauFilter")]
        public async Task<IActionResult> GetDSTangLauFilter()
        {
            var dsTL = await _TangLauService.GetTangLauFilter();
            return Ok(dsTL);
        }
    }
}
