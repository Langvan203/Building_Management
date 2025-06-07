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

        [HttpPut("UpdateNhanVienInPhongBan")]
        public async Task<IActionResult> UpdateNhanVienInPhongBan(UpdateNhanVienPhongBan dto)
        {
            var result = await _nhanVienService.UpdatePhongBanNhanVien(dto.dsPhongBan, dto.MaNV);
            if (!result)
            {
                return NotFound("Không tìm thấy nhân viên hoặc phòng ban để cập nhật");
            }
            return Ok("Cập nhật phòng ban cho nhân viên thành công");
        }

        [HttpPut("UpdateNhanVienToaNha")]
        public async Task<IActionResult> UpdateNhanVienToaNha(UpdateNhanVienToaNha dto)
        {
            var result = await _nhanVienService.UpdateToaNhaNhanVien(dto.dsToaNha, dto.MaNV);
            if (!result)
            {
                return NotFound("Không tìm thấy nhân viên hoặc tòa nhà để cập nhật");
            }
            return Ok("Cập nhật tòa nhà cho nhân viên thành công");
        }

        [HttpPut("UpdateNhanVienRole")]
        public async Task<IActionResult> UpdateNhanVienRole(UpdateNhanVienRole dto)
        {
            var result = await _nhanVienService.UpdateRoleNhanVien(dto.dsRole, dto.MaNV);
            if (!result)
            {
                return NotFound("Không tìm thấy nhân viên hoặc vai trò để cập nhật");
            }
            return Ok("Cập nhật vai trò cho nhân viên thành công");
        }

        [HttpPut("UpdateThongTinNhanVien")]
        public async Task<IActionResult> UpdateThongTinNhanVien(UpdateThongTinNhanVien dto)
        {
            try
            {
                var result = await _nhanVienService.UpdateThongTinNhanVien(dto, Name);
                if (result)
                {
                    return Ok("Cập nhật thông tin nhân viên thành công");
                }
                else
                {
                    return NotFound("Không tìm thấy nhân viên để cập nhật");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
