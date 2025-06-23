using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatBangController : BaseApiController
    {
        private readonly IMatBangService _matBangService;
        public MatBangController(IMatBangService matBangService)
        {
            _matBangService = matBangService;
        }


        [HttpGet("GetDSMatBang")]
        public async Task<IActionResult> GetDSMatBang()
        {
            var dsMB = await _matBangService.GetDSMatBang();
            if (dsMB == null)
            {
                return NotFound("Không tìm thấy danh sách mặt bằng nào");
            }
            return Ok(dsMB);
        }

        [HttpGet("GetDSMatBangByMaKH")]
        public async Task<IActionResult> GetDSMatBangByMaKH(int MaKH)
        {
            var dsMB = await _matBangService.GetDSMatBangByMaKH(MaKH);
            if (dsMB == null)
            {
                return NotFound("Không tìm thấy danh sách mặt bằng nào");
            }
            return Ok(dsMB);
        }

        [HttpGet("GetDSMatBangByMaLMB")]
        public async Task<IActionResult> GetDSMatBangByMaLMB(int MaLMB, int MaTN)
        {
            var dsMB = await _matBangService.GetDSMatBangByMaLMB(MaLMB, MaTN);
            if (dsMB == null)
            {
                return NotFound("Không tìm thấy danh sách mặt bằng nào");
            }
            return Ok(dsMB);
        }
        [HttpGet("GetDSMatBangByMaTL")]
        public async Task<IActionResult> GetDSMatBangByMaTL(int MaTL, int MaTN)
        {
            var dsMB = await _matBangService.GetDSMatBangByMaTL(MaTL, MaTN);
            if (dsMB == null)
            {
                return NotFound("Không tìm thấy danh sách mặt bằng nào");
            }
            return Ok(dsMB);
        }

        [HttpGet("GetDSMatBangByMaTT")]
        public async Task<IActionResult> GetDSMatBangByMaTT(int MaTT, int MaTN)
        {
            var dsMB = await _matBangService.GetDSMatBangByMaTT(MaTT, MaTN);
            if (dsMB == null)
            {
                return NotFound("Không tìm thấy danh sách mặt bằng nào");
            }
            return Ok(dsMB);
        }

        [HttpPost("CreateMatBang")]
        public async Task<IActionResult> CreateMatBang([FromBody] CreateMatBangDto dto)
        {
            var newMB = await _matBangService.CreateMatBang(dto, Name);
            if (newMB == null)
            {
                return BadRequest("Tạo mặt bằng không thành công");
            }
            return Ok(newMB);
        }

        [HttpPut("UpdateMatBang")]
        public async Task<IActionResult> UpdateMatBang([FromBody] UpdateThongTinCoBanMatBangDto dto)
        {
            var updatedMB = await _matBangService.UpdateMatBang(dto, Name);
            if (updatedMB == null)
            {
                return NotFound("Cập nhật mặt bằng không thành công");
            }
            return Ok(updatedMB);
        }

        [HttpDelete("RemoveMatBang/{MaMB}")]
        public async Task<IActionResult> RemoveMatBang(int MaMB)
        {
            var result = await _matBangService.RemoveMatBang(MaMB);
            if (!result)
            {
                return NotFound("Xóa mặt bằng không thành công");
            }
            return Ok("Xóa mặt bằng thành công");
        }

        [HttpGet("GetDSMatBangByMaTN")]
        public async Task<IActionResult> GetDSMatBangByMaTN(int MaTN)
        {
            var dsMB = await _matBangService.GetDSMatBangByMaTN(MaTN);
            if (dsMB == null)
            {
                return NotFound("Không tìm thấy danh sách mặt bằng nào");
            }
            return Ok(dsMB);
        }

        [HttpPost("BanGiaoMatBang/{MaMB}/{MaKH}")]
        public async Task<IActionResult> BanGiaoMatBang(int MaMB, int MaKH)
        {
            var result = await _matBangService.BanGiaoMatBang(MaMB, MaKH);
            if (result == null)
            {
                return NotFound("Bàn giao mặt bằng không thành công");
            }
            return Ok(result);
        }

        [HttpGet("GetDanhSachMatBangForFilters")]
        public async Task<IActionResult> GetDanhSachMatBangForFilters()
        {
            var dsMatBang = await _matBangService.GetDanhSachMatBangForFilters();
            if (dsMatBang == null)
            {
                return NotFound("Không tìm thấy danh sách mặt bằng nào");
            }
            return Ok(dsMatBang);
        }
    }
}
