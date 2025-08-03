using Microsoft.AspNetCore.Mvc;
using PokemonGameAPI.Contracts.DTOs.Role;
using PokemonGameAPI.Contracts.Services;

namespace PokemonGameAPI.Presentation.Controllers
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

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            return Ok(await _roleService.GetAllRolesAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(string id)
        {
            return Ok(await _roleService.GetRoleByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleCreateDto roleCreateDto)
        {
            await _roleService.CreateRoleAsync(roleCreateDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] RoleAssignDto roleAssignDto)
        {
            await _roleService.AssignRoleAsync(roleAssignDto);
            return Ok("Role assigned successfully.");
        }

    }
}
