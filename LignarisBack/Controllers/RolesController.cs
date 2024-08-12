using LignarisBack.Dto;
using LignarisBack.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static LignarisBack.Dto.CrearRoleDto;

namespace LignarisBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole>? _roleManager;
        private readonly UserManager<AppUser>? _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRole createRoleDto)
        {
            if (string.IsNullOrEmpty(createRoleDto.RoleName))
            {
                return BadRequest("El nombre del rol es requerido");
            }

            var roleExist = await _roleManager!.RoleExistsAsync(createRoleDto.RoleName);

            if (roleExist)
            {
                return BadRequest("Este rol ya existe");
            }

            var roleResult = await _roleManager.CreateAsync(new IdentityRole(createRoleDto.RoleName));
            if (roleResult.Succeeded)
            {
                return Ok(new { message = "Rol creado satisfactoriamente" });
            }
            return BadRequest("Rol no pudo ser creado");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleResponseDto>>> GetRoles()
        {
            var roles = await _roleManager!.Roles.Select(r => new RoleResponseDto
            {
                Id = r.Id,
                Name = r.Name,
            }).ToListAsync();

            foreach (var role in roles)
            {
                role.TotalUsers = _userManager!.GetUsersInRoleAsync(role.Name!).Result.Count;
            }
            return Ok(roles);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager!.FindByIdAsync(id);

            if (role is null)
            {
                return NotFound("El rol no fue encontrado.");
            }
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return Ok(new { message = "El rol fue eliminado correctamente." });
            }

            return BadRequest("La eliminacion del rol fallo.");
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignRole([FromBody] RoleAssignDto roleAssignDto)
        {
            var user = await _userManager!.FindByIdAsync(roleAssignDto.UserId);

            if (user is null)
            {
                return NotFound("Usuario no encontrado.");
            }
            var role = await _roleManager!.FindByIdAsync(roleAssignDto.RoleId);

            if (role is null)
            {
                return NotFound("Rol no encontrado.");
            }

            var result = await _userManager.AddToRoleAsync(user, role.Name!);

            if (result.Succeeded)
            {
                return Ok(new { message = "Rol asignado correctamente" });
            }
            var error = result.Errors.FirstOrDefault();
            return BadRequest(error!.Description);
        }
    }
}
