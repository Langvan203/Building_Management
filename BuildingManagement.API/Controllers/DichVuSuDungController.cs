using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DichVuSuDungController : BaseApiController
    {
        private readonly IDichVuSuDungSerivce _dichVuSuDungSerivce;

        public DichVuSuDungController(IDichVuSuDungSerivce dichVuSuDungSerivce)
        {
            _dichVuSuDungSerivce = dichVuSuDungSerivce;
        }

        [HttpGet("GetDSDichVuSuDung")]
        public async Task<IActionResult> GetDSDichVuSuDung()
        {
            var dsDichVuSuDung = await _dichVuSuDungSerivce.GetDSDichVuSuDung();
            return Ok(dsDichVuSuDung);
        }
        [HttpPost("CreateNewDichVuSuDung")]
        public async Task<IActionResult> CreateNewDichVuSuDung(CreateDichVuSuDungDto dto)
        {
            var newDichVuSuDung = await _dichVuSuDungSerivce.CreateDichVuSuDung(dto,Name);
            return Ok(newDichVuSuDung);
        }

        [HttpGet("GetDichVuSuDungByMaKH")]
        public async Task<IActionResult> GetDichVuSuDungByMaMB(int MaKH)
        {
            var checkDichVuSuDung = await _dichVuSuDungSerivce.GetDSDichVuSuDungByMaKH(MaKH);
            if (checkDichVuSuDung != null)
            {
                return Ok(checkDichVuSuDung);
            }
            return NotFound("Không tìm thấy dịch vụ sử dụng với mã MB này");
        }
    }
}
