using BuildingManagement.Application.DTOs;
using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DichVuNuocDinhMucController : BaseApiController
    {
        private readonly IDichVuNuocDinhMucService _nuocDinhMucService;
        public DichVuNuocDinhMucController(IDichVuNuocDinhMucService nuocDinhMucService)
        {
            _nuocDinhMucService = nuocDinhMucService;
        }
        [HttpGet("GetDSDinhMucNuoc")]
        public async Task<IActionResult> GetDSDinhMucNuoc()
        {
            var dsDinhMuc = await _nuocDinhMucService.GetDSDinhMucNuoc();
            return Ok(dsDinhMuc);
        }
        [HttpPost("CreateDinhMucNuoc")]
        public async Task<IActionResult> CreateNewDinhMuc(CreateDinhMuc dto)
        {
            var newDinhMuc = await _nuocDinhMucService.CreateNewDinhMuc(dto, Name);
            return Ok(newDinhMuc);
        }

        [HttpPut("UpdateDinhMucNuoc")]
        public async Task<IActionResult> UpdateNuocDinhMuc(DinhMucDTO dto)
        {
            var updatedDinhMuc = await _nuocDinhMucService.UpdateNuocDinhMuc(dto, Name);
            if(updatedDinhMuc)
            {
                return Ok("Cập nhật định mức nước thành công.");
            }
            return NotFound("Không tìm thấy định mức nước để cập nhật.");
        }

        [HttpDelete("RemoveDinhMucNuoc/{MaDM}")]
        public async Task<IActionResult> RemoveNuocDinhMuc(int MaDM)
        {
            var result = await _nuocDinhMucService.RemoveNuocDinhMuc(MaDM);
            if (result)
            {
                return Ok("Xóa định mức nước thành công.");
            }
            return NotFound("Không tìm thấy định mức nước.");
        }
    }
}
