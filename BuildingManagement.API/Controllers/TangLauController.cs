using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TangLauController : BaseApiController
    {
        private readonly ITangLauServices _TangLauService;
        public TangLauController(ITangLauServices tangLauService)
        {
            _TangLauService = tangLauService;
        }

        [HttpGet("get-ds-tang-lau")]
        public async Task<IActionResult> GetDSTangLau(int MaTN, int MaKN)
        {
            var dsTL = await _TangLauService.GetDSTangLauByMaKN(MaTN, MaKN);
            return Ok(dsTL);
        }

        [HttpPost("create-tang-lau")]
        public async Task<IActionResult> CreateNewTangLau(CreateTangLauDto dto, int MaTN, int MaKN)
        {

            var newTL = await _TangLauService.CreateTangLau(dto, MaKN, Name, MaTN);
            return Ok(newTL);
        }
    }
}
