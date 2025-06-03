using BuildingManagement.Application.DTOs;
using BuildingManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanVienController : BaseApiController
    {
        private readonly INhanVienService _nhanVienService;
        public NhanVienController(INhanVienService nhanVienService)
        {
            _nhanVienService = nhanVienService;
        }

        [HttpGet("GetDSNhanVien")]
        public async Task<IActionResult> GetDSNhanVien()
        {
            var dsNV = await _nhanVienService.GetDSNhanVien();
            if (dsNV == null)
            {
                return NotFound("Không tìm thấy danh sách nhân viên nào");
            }
            return Ok(dsNV);
        }

        [HttpPost("CreateNhanVien")]
        public async Task<IActionResult> CreateNhanVien(CreateNhanVienDto dto)
        {
            try
            {
                var newNV = await _nhanVienService.CreateNewNhanVien(dto, Name);
                return Ok(newNV);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ThemNhanVienToPB")]
        public async Task<IActionResult> ThemNhanVienToPB(int manv, int MaPB)
        {
            var result = await _nhanVienService.ThemNhanVienPhongBan(manv, MaPB);
            if (!result)
            {
                return NotFound("Không tìm thấy nhân viên hoặc phòng ban để thêm");
            }
            return Ok("Thêm nhân viên vào phòng ban thành công");
        }
    }
}
