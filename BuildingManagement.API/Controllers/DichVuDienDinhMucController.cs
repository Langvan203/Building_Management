using BuildingManagement.Application.DTOs;
using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DichVuDienDinhMucController : BaseApiController
    {
        private readonly IDichVuDienDinhMucService _dienDinhMucService;
        public DichVuDienDinhMucController(IDichVuDienDinhMucService dienDinhMucService)
        {
            _dienDinhMucService = dienDinhMucService;
        }

        [HttpGet("GetDSDinhMuc")]
        public async Task<IActionResult> GetDSDinhMuc()
        {
            var dsDinhMuc = await _dienDinhMucService.GetDSDinhMucDien();
            return Ok(dsDinhMuc);
        }

        [HttpPost("CreateNewDinhMuc")]
        public async Task<IActionResult> CreateNewDinhMuc(CreateDinhMuc dto)
        {
            var newDinhMuc = await _dienDinhMucService.CreateNewDinhMuc(dto, Name);
            return Ok(newDinhMuc);
        }

        [HttpPut("UpdateDinhMuc")]
        public async Task<IActionResult> UpdateDinhMuc([FromBody]DinhMucDTO dto)
        {
            var updatedDinhMuc = await _dienDinhMucService.UpdateDienDinhMuc(dto, Name);
            if (updatedDinhMuc)
            {
                return Ok("Cập nhật định mức điện thành công");
            }
            return NotFound("Dịch vụ điện định mức không tồn tại.");
        }

        [HttpDelete("RemoveDinhMuc/{MaDM}")]
        public async Task<IActionResult> RemoveDinhMuc(int MaDM)
        {
            var result = await _dienDinhMucService.RemoveDienDinhMuc(MaDM);
            if (result)
            {
                return Ok("Xóa dịch vụ điện định mức thành công.");
            }
            return NotFound("Dịch vụ điện định mức không tồn tại.");
        }
    }
}
