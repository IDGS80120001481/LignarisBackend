using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LignarisBack.Dto;
using LignarisBack.Models;

namespace Lignarias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private readonly LignarisPizzaContext _context;

        public ProveedoresController(LignarisPizzaContext context)
        {
            _context = context;
        }

        // Método GET para obtener la lista de ProveedorMateriaPrima
        [HttpGet]
        [Route("ProveedoresMateriaPrima")]
        public async Task<ActionResult<IEnumerable<ProveedorMateriaPrimaDto>>> GetProveedoresMateriaPrima()
        {
            var proveedores = await _context.Proveedors
                .Include(p => p.IdPersonaNavigation)
                .Select(p => new ProveedorMateriaPrimaDto
                {
                    IdProveedor = p.IdProveedor,
                    Nombre = p.IdPersonaNavigation.Nombre,
                    ApellidoPaterno = p.IdPersonaNavigation.ApellidoPaterno,
                    ApellidoMaterno = p.IdPersonaNavigation.ApellidoMaterno
                })
                .ToListAsync();
            return Ok(proveedores);
        }


        // GET: api/Proveedores/ConMateriasPrimas
        [HttpGet]
        [Route("ConMateriasPrimas")]
        public async Task<ActionResult<IEnumerable<object>>> GetProveedoresConMateriasPrimas()
        {
            var proveedores = await _context.Proveedors
                .Include(p => p.IdPersonaNavigation)
                .Include(p => p.MateriaProveedorIntermedia)
                .Select(p => new
                {
                    p.IdProveedor,
                    p.Estatus,
                    Persona = new
                    {
                        p.IdPersonaNavigation.IdPersona,
                        p.IdPersonaNavigation.Nombre,
                        p.IdPersonaNavigation.ApellidoPaterno,
                        p.IdPersonaNavigation.ApellidoMaterno,
                        p.IdPersonaNavigation.Telefono,
                        p.IdPersonaNavigation.Direccion,
                        p.IdPersonaNavigation.Email
                    },
                    MateriasPrimas = p.MateriaProveedorIntermedia.Select(mp => new
                    {
                        mp.IdMateriaPrima,
                        mp.IdProveedor
                    })
                })
                .ToListAsync();

            return Ok(proveedores);
        }

        // GET: api/Proveedores
        [HttpGet]
        [Route("ListaProveedores")]
        public async Task<ActionResult<IEnumerable<object>>> GetProveedores()
        {
            var proveedores = await _context.Proveedors
                .Include(p => p.IdPersonaNavigation)
                .Select(p => new
                {
                    p.IdProveedor,
                    p.Estatus,
                    Persona = new
                    {
                        IdPersona = p.IdPersonaNavigation.IdPersona,
                        Nombre = p.IdPersonaNavigation.Nombre,
                        ApellidoPaterno = p.IdPersonaNavigation.ApellidoPaterno,
                        ApellidoMaterno = p.IdPersonaNavigation.ApellidoMaterno,
                        Telefono = p.IdPersonaNavigation.Telefono,
                        Direccion = p.IdPersonaNavigation.Direccion,
                        Email = p.IdPersonaNavigation.Email
                    }
                })
                .ToListAsync();

            return Ok(proveedores);
        }


        // GET: api/Proveedores/id
        [HttpGet]
        [Route("ListaProveedores/{id:int}")]
        public async Task<ActionResult<Proveedor>> GetProveedor(int id)
        {
            var proveedor = await _context.Proveedors
                .Include(p => p.IdPersonaNavigation)
                .Include(p => p.MateriaProveedorIntermedia)
                .FirstOrDefaultAsync(p => p.IdProveedor == id);

            if (proveedor == null)
            {
                return NotFound();
            }

            return Ok(proveedor);
        }


        // POST: api/Proveedores
        [HttpPost]
        [Route("AgregarProveedor")]
        public async Task<ActionResult<Proveedor>> PostProveedor(ProveedorDto proveedorDTO)
        {
            var persona = new Persona
            {
                Nombre = proveedorDTO.Persona.Nombre,
                ApellidoPaterno = proveedorDTO.Persona.ApellidoPaterno,
                ApellidoMaterno = proveedorDTO.Persona.ApellidoMaterno,
                Telefono = proveedorDTO.Persona.Telefono,
                Direccion = proveedorDTO.Persona.Direccion,
                Email = proveedorDTO.Persona.Email
            };

            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();

            var proveedor = new Proveedor
            {
                Estatus = proveedorDTO.Estatus,
                IdPersona = persona.IdPersona
            };

            _context.Proveedors.Add(proveedor);
            await _context.SaveChangesAsync();

            int id_provedor = proveedor.IdProveedor;

            return Ok("Se ha insertado el proveedor satisfactoriamente: " + id_provedor);
        }

        // PUT: api/Proveedores/id
        [HttpPut]
        [Route("ModificarProveedor/{id:int}")]
        public async Task<IActionResult> PutProveedor(int id, ProveedorDto proveedorUpdateDto)
        {
            if (id != proveedorUpdateDto.IdProveedor)
            {
                return BadRequest();
            }

            var proveedor = await _context.Proveedors
                .Include(p => p.IdPersonaNavigation)
                .FirstOrDefaultAsync(p => p.IdProveedor == id);

            if (proveedor == null)
            {
                return NotFound();
            }

            proveedor.Estatus = proveedorUpdateDto.Estatus;

            if (proveedorUpdateDto.Persona != null)
            {
                proveedor.IdPersonaNavigation.Nombre = proveedorUpdateDto.Persona.Nombre;
                proveedor.IdPersonaNavigation.ApellidoPaterno = proveedorUpdateDto.Persona.ApellidoPaterno;
                proveedor.IdPersonaNavigation.ApellidoMaterno = proveedorUpdateDto.Persona.ApellidoMaterno;
                proveedor.IdPersonaNavigation.Telefono = proveedorUpdateDto.Persona.Telefono;
                proveedor.IdPersonaNavigation.Direccion = proveedorUpdateDto.Persona.Direccion;
                proveedor.IdPersonaNavigation.Email = proveedorUpdateDto.Persona.Email;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProveedorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // DELETE: api/Proveedores/id
        [HttpDelete]
        [Route("EliminarProveedor/{id:int}")]
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            var proveedor = await _context.Proveedors.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            _context.Proveedors.Remove(proveedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProveedorExists(int id)
        {
            return _context.Proveedors.Any(e => e.IdProveedor == id);
        }
    }
}