using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiMatBangController : BaseApiController
    {
        private readonly ILoaiMatBangService _loaiMatBangService;
        public LoaiMatBangController(ILoaiMatBangService loaiMatBangService)
        {
            _loaiMatBangService = loaiMatBangService;
        }

        [HttpGet("Get-ds-loaimb")]
        public async Task<IActionResult> GetDSLoaiMB()
        {
            var dsLMB = await _loaiMatBangService.GetDSLoaiMB();
            return Ok(dsLMB);
        }

        [HttpPost("Create-new-loaimb")]
        public async Task<IActionResult> CreateNewLoaiMB(CreateNewLoaiMB dto)
        {
            var newLMB = await _loaiMatBangService.CreateNewLoaiMB(dto,Name);
            return Ok(newLMB);
        }

        [HttpDelete("Remove-loaimb")]
        public async Task<IActionResult> RemoveLoaiMB(int MaLMB)
        {
            var removeLMB = await _loaiMatBangService.DeleteLoaiMB(MaLMB);
            return Ok(removeLMB);
        }
    }
}
