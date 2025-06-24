using BuildingManagement.Application.DTOs;
using BuildingManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NKBTKeHoachBaoTriController : BaseApiController
    {
        private readonly INKBTKeHoachBaoTriService _keHoachBaoTriService;
        public NKBTKeHoachBaoTriController(INKBTKeHoachBaoTriService keHoachBaoTriService)
        {
            _keHoachBaoTriService = keHoachBaoTriService;
        }
        [HttpPost("CreateKeHoachBaoTri")]
        public async Task<IActionResult> CreateKeHoachBaoTri([FromBody] CreateKeHoachBaoTriDto dto)
        {
            var result = await _keHoachBaoTriService.CreateKeHoachBaoTri(dto, Name);
            return Ok(result);
        }

        [HttpGet("GetDSKeHoachBaoTri")]
        public async Task<IActionResult> GetDSKeHoachBaoTri(int pageNumber, int pageSize = 15)
        {
            var result = await _keHoachBaoTriService.GetDSKeHoachBaoTri(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpPost("GiaoViecChoNhanVien")]
        public async Task<IActionResult> GiaoViecChoNhanVien([FromBody] GiaoViecChoNhanVien dto)
        {
            var result = await _keHoachBaoTriService.GiaoViecChoNhanVien(dto, Name);
            if (result)
            {
                return Ok("Giao việc thành công");
            }
            return BadRequest("Giao việc không thành công");
        }

        [HttpPost("BatDauKeHoach")]
        public async Task<IActionResult> BatDauKeHoach(int MaKeHoach)
        {
            var result = await _keHoachBaoTriService.BatDauKeHoach(MaKeHoach, Name);
            if (result)
            {
                return Ok("Bắt đầu kế hoạch thành công");
            }
            return BadRequest("Bắt đầu kế hoạch không thành công");
        }
        [HttpPost("HoanThanhKeHoach")]
        public async Task<IActionResult> HoanThanhKeHoach(int MaKeHoach)
        {
            var result = await _keHoachBaoTriService.HoanThanhKeHoach(MaKeHoach, Name);
            if (result)
            {
                return Ok("Hoàn thành kế hoạch thành công");
            }
            return BadRequest("Hoàn thành kế hoạch không thành công");
        }
        [HttpPost("HuyKeHoach")]
        public async Task<IActionResult> HuyKeHoach(int MaKeHoach)
        {
            var result = await _keHoachBaoTriService.HuyKeHoach(MaKeHoach, Name);
            if (result)
            {
                return Ok("Hủy kế hoạch thành công");
            }
            return BadRequest("Hủy kế hoạch không thành công");
        }

        [HttpPost("XoaKeHoach")]
        public async Task<IActionResult> XoaKeHoach(int MaKeHoach)
        {
            var result = await _keHoachBaoTriService.XoaKeHoach(MaKeHoach);
            if (result)
            {
                return Ok("Xóa kế hoạch thành công");
            }
            return BadRequest("Xóa kế hoạch không thành công");
        }
    }
}
