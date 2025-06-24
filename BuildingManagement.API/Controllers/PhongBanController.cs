using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhongBanController : BaseApiController
    {
        private readonly IPhongBanService _phongBanService;
        public PhongBanController(IPhongBanService phongBanService)
        {
            _phongBanService = phongBanService;
        }
        [HttpGet("GetDSPhongBan")]
        public async Task<IActionResult> GetDSPhongBan()
        {
            var dsPB = await _phongBanService.GetAllPhongBan();
            if (dsPB == null)
            {
                return NotFound("Không tìm thấy danh sách phòng ban nào");
            }
            return Ok(dsPB);
        }

        [HttpPost("CreatePhongBan")]
        public async Task<IActionResult> CreatePhongBan(CreatePhongBanDto dto)
        {
            var newPB = await _phongBanService.CreatePhongBanDto(dto, Name);
            if (newPB == null)
            {
                return BadRequest("Tạo phòng ban không thành công");
            }
            return Ok(newPB);
        }

        [HttpDelete("RemovePhongBan/{id}")]
        public async Task<IActionResult> RemovePhongBan(int id)
        {
            var result = await _phongBanService.RemovePhongBan(id);
            if (!result)
            {
                return NotFound("Không tìm thấy phòng ban để xóa");
            }
            return Ok("Xóa phòng ban thành công");
        }

        [HttpPut("UpdatePhongBan")]
        public async Task<IActionResult> UpdatePhongBan(UpdateDatePhongBanDto dto)
        {
            var result = await _phongBanService.UpdatePhongBan(dto, Name);
            if (!result)
            {
                return NotFound("Không tìm thấy phòng ban để cập nhật");
            }
            return Ok("Cập nhật phòng ban thành công");
        }

        [HttpDelete]
        [Route("RemoveNhanVienInPhongBan/{maPB}/{maNV}")]
        public async Task<IActionResult> RemoveNhanVienInPhongBan(int maPB, int maNV)
        {
            var result = await _phongBanService.RemoveNhanVienInPhongBan(maPB, maNV);
            if (!result)
            {
                return NotFound("Không tìm thấy nhân viên trong phòng ban để xóa");
            }
            return Ok("Xóa nhân viên khỏi phòng ban thành công");
        }
    }
}
