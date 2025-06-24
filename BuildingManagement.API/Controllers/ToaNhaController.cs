using BuildingManagement.API.Filter;
using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToaNhaController : BaseApiController
    {
        private readonly IToaNhaServices _toaNhaServices;
        public ToaNhaController(IToaNhaServices toaNhaServices) 
        {
            _toaNhaServices = toaNhaServices;    
        }

        [HttpPost("them-toa-nha")]
        [Authorize(Roles = "Quản lý tòa nhà")]
        public async Task<IActionResult> CreateNewToaNha([FromBody]CreateToaNhaDto dto)
        {
            
            var newTn = await _toaNhaServices.TaoToaNhaAsync(dto, Name);
            return Ok(newTn);
            
        }

        [HttpGet("GetDSToaNha")]
        [Authorize(Policy = "ViewBuilding")]
        public async Task<IActionResult> GetDSToaNha()
        {
         
            var dstn = await _toaNhaServices.GetDSToaNhaAsync();
            return Ok(dstn);
        }

        [HttpGet("SummaryTotalData")]
        public async Task<IActionResult> SummaryData()
        {
            var data = await _toaNhaServices.SummaryTotalBuildingAsync();
            return Ok(data);
        }

        [HttpGet("GetOccupancyRate")]
        public async Task<IActionResult> GetOccupancyRate()
        {
            var data = await _toaNhaServices.GetOccupancyRateAsync();
            return Ok(data);
        }

        [HttpGet("BuildingDataOverView")]
        [ServiceFilter(typeof(ApiPerformanceFilter))]
        public async Task<IActionResult> GetBuildingDataOverView([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var data = await _toaNhaServices.BuildingsData(from,to);
            return Ok(data);
        }

        [HttpGet("GetFinnancesData")]
        [ServiceFilter(typeof(ApiPerformanceFilter))]
        public async Task<IActionResult> GetFinnancesData([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var data = await _toaNhaServices.GetFinnancesData(from, to);
            return Ok(data);
        }

        [HttpGet("GetServicesData")]
        [ServiceFilter(typeof(ApiPerformanceFilter))]
        public async Task<IActionResult> GetServicesData([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var data = await _toaNhaServices.GetServicesData(from, to);
            return Ok(data);
        }

        [HttpGet("GetOverViewData")]
        [ServiceFilter(typeof(ApiPerformanceFilter))]
        public async Task<IActionResult> GetOverViewData([FromQuery] int year)
        {
            var data = await _toaNhaServices.GetOverViewData(year);
            return Ok(data);
        }

        [HttpDelete("XoaToaNhaById")]
        [ServiceFilter(typeof(ApiPerformanceFilter))]
        public async Task<IActionResult> DeleteToaNha([FromQuery] int id)
        {
            var result = await _toaNhaServices.XoaToaNhaAsync(id);
            if (result)
            {
                return Ok("Xóa thành công");
            }
            return BadRequest("Xóa không thành công");
        }

        [HttpPut("UpdateToaNha")]
        [ServiceFilter(typeof(ApiPerformanceFilter))]
        public async Task<IActionResult> UpdateToaNha([FromBody] UpdateToaNhaDto dto)
        {
            var result = await _toaNhaServices.UpdateToaNha(dto, Name);
            return Ok(result);
        }

    }
}
