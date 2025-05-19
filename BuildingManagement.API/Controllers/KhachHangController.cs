using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : BaseApiController
    {
        private readonly IKhacHangService _khachHangService;
        public KhachHangController(IKhacHangService khachHangService)
        {
            _khachHangService = khachHangService;
        }

        [HttpGet("GetDSKhachHang")]
        public async Task<IActionResult> GetDSKhachHang()
        {
            var dsKhachHang = await _khachHangService.GetDSKhachHang();
            return Ok(dsKhachHang);
        }

        [HttpPost("CreateNewKhachHang")]
        public async Task<IActionResult> CreateNewKhachHang(CreateKhachHangDto dto)
        {
            var newKhachHang = await _khachHangService.CreateNewKhachHang(dto, Name);
            return Ok(newKhachHang);
        }
    }
    
}
