using BuildingManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet("GetDSRole")]
        public async Task<IActionResult> GetDSRole()
        {
            var dsRole = await _roleService.GetDSRole();
            if (dsRole == null || !dsRole.Any())
            {
                return NotFound("Không tìm thấy danh sách vai trò nào");
            }
            return Ok(dsRole);
        }
    }
}
