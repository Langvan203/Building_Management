using BuildingManagement.Application.DTOs;
using BuildingManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YeuCauSuaChuaController : BaseApiController
    {
        private readonly IYeuCauBaoTriService _yeuCauBaoTriService;
        public YeuCauSuaChuaController(IYeuCauBaoTriService yeuCauBaoTriService)
        {
            _yeuCauBaoTriService = yeuCauBaoTriService;
        }

        [HttpGet("GetDSYeuCauSuaChua")]
        public async Task<IActionResult> GetDSYeuCauSuaChua(int pageNumber, int pageSize = 10)
        {
            try
            {
                var result = await _yeuCauBaoTriService.GetDSYeuCauSuaChua(pageNumber, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("DuyetYeuCau")]
        public async Task<IActionResult> DuyetYeuCau(int MaYC)
        {
            try
            {
                var result = await _yeuCauBaoTriService.DuyetYeuCau(MaYC);
                return Ok(new { success = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("TuChoiYeuCau")]
        public async Task<IActionResult> TuChoiYeuCau(int MaYC)
        {
            try
            {
                var result = await _yeuCauBaoTriService.TuChoiYeuCau(MaYC);
                return Ok(new { success = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("DanhDauHoanThanh")]
        public async Task<IActionResult> DanhDauHoanThanh(int MaYC)
        {
            try
            {
                var result = await _yeuCauBaoTriService.DanhDauHoanThanh(MaYC);
                return Ok(new { success = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("GiaoViecChoNhanVien")]
        public async Task<IActionResult> GiaoViecChoNhanVien(GiaoViecYeuCauChoNhanVien dto)
        {
            try
            {
                var result = await _yeuCauBaoTriService.GiaoViecChoNhanVien(dto, Name);
                return Ok(new { success = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
