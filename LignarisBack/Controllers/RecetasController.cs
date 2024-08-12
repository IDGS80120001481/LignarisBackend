using LignarisBack.Dto;
using LignarisBack.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lignaris.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecetasController : ControllerBase
    {
        private readonly LignarisPizzaContext _context;

        public RecetasController(LignarisPizzaContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("ListaRecetas")]
        public async Task<ActionResult<IEnumerable<RecetaDto>>> GetRecetas()
        {
            var recetas = await _context.Receta
                .Include(r => r.RecetaDetalles)
                .ThenInclude(rd => rd.IdMateriaPrimaNavigation)
                .ToListAsync();

            var RecetaDtos = recetas.Select(r => new RecetaDto
            {
                IdReceta = r.IdReceta,
                Nombre = r.Nombre,
                Foto = r.Foto,
                Tamanio = r.Tamanio,
                PrecioUnitario = r.PrecioUnitario,
                Estatus = r.Estatus,
                RecetaDetalles = r.RecetaDetalles.Select(rd => new RecetaDetalleDto
                {
                    IdRecetaDetalle = rd.IdRecetaDetalle,
                    IdMateriaPrima = rd.IdMateriaPrima,
                    Cantidad = rd.Cantidad
                }).ToList()
            }).ToList();

            return RecetaDtos;
        }

        [HttpGet]
        [Route("ListaRecetas/{id:int}")]
        public async Task<ActionResult<RecetaDto>> GetReceta(int id)
        {
            var receta = await _context.Receta
                .Include(r => r.RecetaDetalles)
                .ThenInclude(rd => rd.IdMateriaPrimaNavigation)
                .FirstOrDefaultAsync(r => r.IdReceta == id);

            if (receta == null)
            {
                return NotFound();
            }

            var RecetaDto = new RecetaDto
            {
                IdReceta = receta.IdReceta,
                Nombre = receta.Nombre,
                Foto = receta.Foto,
                Tamanio = receta.Tamanio,
                PrecioUnitario = receta.PrecioUnitario,
                Estatus = receta.Estatus,
                RecetaDetalles = receta.RecetaDetalles.Select(rd => new RecetaDetalleDto
                {
                    IdRecetaDetalle = rd.IdRecetaDetalle,
                    IdMateriaPrima = rd.IdMateriaPrima,
                    Cantidad = rd.Cantidad
                }).ToList()
            };

            return RecetaDto;
        }

        [HttpPost]
        [Route("AgregarReceta")]
        public async Task<ActionResult<RecetaDto>> PostReceta(RecetaDto RecetaDto)
        {
            var receta = new Recetum
            {
                Nombre = RecetaDto.Nombre,
                Foto = RecetaDto.Foto,
                Tamanio = RecetaDto.Tamanio,
                PrecioUnitario = RecetaDto.PrecioUnitario,
                Estatus = RecetaDto.Estatus,
                RecetaDetalles = RecetaDto.RecetaDetalles.Select(rd => new RecetaDetalle
                {
                    IdMateriaPrima = rd.IdMateriaPrima,
                    Cantidad = rd.Cantidad
                }).ToList()
            };

            _context.Receta.Add(receta);
            await _context.SaveChangesAsync();

            RecetaDto.IdReceta = receta.IdReceta;

            return CreatedAtAction(nameof(GetReceta), new { id = receta.IdReceta }, RecetaDto);
        }

        [HttpPut]
        [Route("ModificarReceta/{id:int}")]
        public async Task<IActionResult> PutReceta(int id, RecetaDto RecetaDto)
        {
            if (id != RecetaDto.IdReceta)
            {
                return BadRequest();
            }

            var receta = await _context.Receta
                .Include(r => r.RecetaDetalles)
                .FirstOrDefaultAsync(r => r.IdReceta == id);

            if (receta == null)
            {
                return NotFound();
            }

            receta.Nombre = RecetaDto.Nombre;
            receta.Foto = RecetaDto.Foto;
            receta.Tamanio = RecetaDto.Tamanio;
            receta.PrecioUnitario = RecetaDto.PrecioUnitario;
            receta.Estatus = RecetaDto.Estatus;

            // Update RecetaDetalles
            _context.RecetaDetalles.RemoveRange(receta.RecetaDetalles);
            receta.RecetaDetalles = RecetaDto.RecetaDetalles.Select(rd => new RecetaDetalle
            {
                IdMateriaPrima = rd.IdMateriaPrima,
                Cantidad = rd.Cantidad
            }).ToList();

            _context.Entry(receta).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("EliminarReceta/{id:int}")]
        public async Task<IActionResult> DeleteReceta(int id)
        {
            var receta = await _context.Receta
                .Include(r => r.RecetaDetalles)
                .FirstOrDefaultAsync(r => r.IdReceta == id);

            if (receta == null)
            {
                return NotFound();
            }

            _context.RecetaDetalles.RemoveRange(receta.RecetaDetalles);
            _context.Receta.Remove(receta);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}