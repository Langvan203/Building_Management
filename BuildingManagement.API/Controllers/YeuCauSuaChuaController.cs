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
    }
}
