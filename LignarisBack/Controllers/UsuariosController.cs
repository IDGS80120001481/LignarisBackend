using LignarisBack.Dto;
using LignarisBack.Models;
using LignarisBack.Dto;
using LignarisBack.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LignarisBack.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuariosController : ControllerBase
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly LignarisPizzaContext _basedatos;

        public UsuariosController(UserManager<AppUser> userManager, RoleManager<IdentityRole>
       roleManager, IConfiguration configuration, LignarisPizzaContext basedatos)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _basedatos = basedatos;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var person = new Persona
            {
                Nombre = registerDto.Nombre,
                ApellidoPaterno = registerDto.ApellidoPaterno,
                ApellidoMaterno = registerDto.ApellidoMaterno,
                Telefono = registerDto.Telefono,
                Direccion = registerDto.Direccion,
                Email = registerDto.Email
            };

            _basedatos.Personas.Add(person);
            await _basedatos.SaveChangesAsync();

            int idPersona = person.IdPersona;

            var user = new AppUser
            {
                Email = registerDto.Email,
                Fullname = registerDto.Nombre + " " + registerDto.ApellidoPaterno,
                UserName = registerDto.Email
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            if (registerDto.Roles is null)
            {
                await _userManager.AddToRoleAsync(user, "Cliente");
            }
            else
            {
                foreach (var role in registerDto.Roles)
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
            }
            var idUsuario = user.Id;
            var cliente = new Cliente
            {
                IdUsuario = idUsuario,
                IdPersona = idPersona,
            };
            _basedatos.Clientes.Add(cliente);
            await _basedatos.SaveChangesAsync();

            return Ok(new AuthResponseDto
            {
                IsSuccess = true,
                Message = "Cuenta creada exitosamente."
            });
        }

        [AllowAnonymous]
        [HttpPost("register_empleado")]
        public async Task<ActionResult<string>> RegisterEmpleado(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var person = new Persona
            {
                Nombre = registerDto.Nombre,
                ApellidoPaterno = registerDto.ApellidoPaterno,
                ApellidoMaterno = registerDto.ApellidoMaterno,
                Telefono = registerDto.Telefono,
                Direccion = registerDto.Direccion,
                Email = registerDto.Email
            };

            _basedatos.Personas.Add(person);
            await _basedatos.SaveChangesAsync();
            int idPersona = person.IdPersona;

            var user = new AppUser
            {
                Email = registerDto.Email,
                Fullname = registerDto.Nombre + " " + registerDto.ApellidoPaterno,
                UserName = registerDto.Email
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            if (registerDto.Roles is null)
            {
                await _userManager.AddToRoleAsync(user, "Empleado");
            }
            else
            {
                foreach (var role in registerDto.Roles)
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
            }
            var idUsuario = user.Id;
            var empleado = new Empleado
            {
                IdUsuario = idUsuario,
                IdPersona = idPersona,
            };
            _basedatos.Empleados.Add(empleado);
            await _basedatos.SaveChangesAsync();

            return Ok(new AuthResponseDto
            {
                IsSuccess = true,
                Message = "Cuenta creada exitosamente."
            });
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized(new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Usuario no encontrado con este correo."
                });
            }
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result)
            {
                return Unauthorized(new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Contraseña incorrecta"
                });
            }
            var token = GenerateToken(user);
            return Ok(new AuthResponseDto
            {
                Token = token,
                IsSuccess = true,
                Message = "Acceso correcto"
            });
        }
        private string GenerateToken(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key =
           Encoding.ASCII.GetBytes(_configuration.GetSection("JWTSetting").GetSection("securityKey").Value!);
            var roles = _userManager.GetRolesAsync(user).Result;
            List<Claim> claims = [
            new (JwtRegisteredClaimNames.Email, user.Email??""),
            new (JwtRegisteredClaimNames.Name, user.Fullname??""),
            new (JwtRegisteredClaimNames.NameId, user.Id??""),
            new (JwtRegisteredClaimNames.Aud,
            _configuration.GetSection("JWTSetting").GetSection("ValidAudience").Value!),
            new (JwtRegisteredClaimNames.Iss,
            _configuration.GetSection("JWTSetting").GetSection("ValidIssuer").Value!)
            ];
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
           SecurityAlgorithms.HmacSha256
            )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        //[Authorize]
        [HttpGet("detail")]
        public async Task<ActionResult<UserDetailDto>> GetUserDetail()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(currentUserId!);
            var persona = await _basedatos.Personas.FindAsync(user!.Id);
            if (user == null)
            {
                return NotFound(new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Usuario no encontrado"
                });
            }

            var personadto = new PersonaDto
            {
                Nombre = persona!.Nombre,
                ApellidoPaterno = persona.ApellidoPaterno,
                ApellidoMaterno = persona.ApellidoMaterno,
                Direccion = persona.Direccion,
                Email = persona.Email,
                Telefono = persona.Telefono
            };

            return Ok(new UserDetailDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.Fullname,
                Roles = [.. await _userManager.GetRolesAsync(user)],
                Persona = personadto
            });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetailDto>>> GetUsers()
        {
            var users = await _userManager.Users.Select(u => new UserDetailDto
            {
                Id = u.Id,
                Email = u.Email,
                FullName = u.Fullname,
                Roles = _userManager.GetRolesAsync(u).Result.ToArray()
            }).ToListAsync();
            return Ok(users);
        }

    }
}
