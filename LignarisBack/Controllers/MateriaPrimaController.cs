using LignarisBack.Dto;
using LignarisBack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class MateriaPrimasController : ControllerBase
{
    private readonly LignarisPizzaContext _context;

    public MateriaPrimasController(LignarisPizzaContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("ListaMateriaPrima")]
    public async Task<ActionResult<IEnumerable<object>>> GetMateriaPrimas()
    {
        var materiaPrimas = await _context.MateriaPrimas
            .Select(m => new
            {
                m.IdMateriaPrima,
                m.Nombre,
                m.TipoMedida,
                m.CantidadMinima,
                Proveedores = m.MateriaProveedorIntermedia
                    .Select(mpi => new
                    {
                        mpi.IdProveedor,
                        NombreProveedor = mpi.IdProveedorNavigation.IdPersonaNavigation.Nombre,
                        ApellidoPaternoProveedor = mpi.IdProveedorNavigation.IdPersonaNavigation.ApellidoPaterno,
                        ApellidoMaternoProveedor = mpi.IdProveedorNavigation.IdPersonaNavigation.ApellidoMaterno
                    })
                    .ToList()
            })
            .ToListAsync();

        return Ok(materiaPrimas);
    }



    [HttpGet("{id}")]
    public async Task<ActionResult<MateriaPrima>> GetMateriaPrima(int id)
    {
        var materiaPrima = await _context.MateriaPrimas.Include(m => m.MateriaProveedorIntermedia).FirstOrDefaultAsync(m => m.IdMateriaPrima == id);
        if (materiaPrima == null)
        {
            return NotFound();
        }
        return materiaPrima;
    }

    [HttpPost]
    [Route("{idProveedor}/AgregarMateriaPrima")]
    public async Task<ActionResult> PostMateriaPrima(int idProveedor, List<MateriaPrimaDto> materiaPrimaDtos)
    {
        var proveedor = await _context.Proveedors.FindAsync(idProveedor);
        if (proveedor == null)
        {
            return NotFound();
        }

        var materiaProveedorIntermedioms = new List<MateriaProveedorIntermedium>();

        foreach (var materiaPrimaDto in materiaPrimaDtos)
        {
            var materiaPrima = new MateriaPrima
            {
                Nombre = materiaPrimaDto.Nombre,
                TipoMedida = materiaPrimaDto.TipoMedida,
                CantidadMinima = materiaPrimaDto.CantidadMinima
            };

            _context.MateriaPrimas.Add(materiaPrima);
            await _context.SaveChangesAsync();

            var materiaProveedorIntermedium = new MateriaProveedorIntermedium
            {
                IdMateriaPrima = materiaPrima.IdMateriaPrima,
                IdProveedor = idProveedor
            };

            materiaProveedorIntermedioms.Add(materiaProveedorIntermedium);
        }

        _context.MateriaProveedorIntermedia.AddRange(materiaProveedorIntermedioms);

        await _context.SaveChangesAsync();

        return NoContent();
    }



    [HttpPut]
    [Route("ModificarMateriaPrima/{id:int}")]
    public async Task<IActionResult> PutMateriaPrima(int id, [FromBody] MateriaPrimaUpdateDto updateDto)
    {
        if (id != updateDto.IdMateriaPrima)
        {
            return BadRequest();
        }

        var materiaPrima = await _context.MateriaPrimas
            .Include(mp => mp.MateriaProveedorIntermedia)
            .FirstOrDefaultAsync(mp => mp.IdMateriaPrima == id);

        if (materiaPrima == null)
        {
            return NotFound();
        }

        materiaPrima.Nombre = updateDto.Nombre;
        materiaPrima.TipoMedida = updateDto.TipoMedida;
        materiaPrima.CantidadMinima = updateDto.CantidadMinima;

        if (materiaPrima.MateriaProveedorIntermedia.Any())
        {
            var existingRelation = materiaPrima.MateriaProveedorIntermedia.First();
            existingRelation.IdProveedor = updateDto.IdProveedor;
        }
        else
        {
            _context.MateriaProveedorIntermedia.Add(new MateriaProveedorIntermedium
            {
                IdMateriaPrima = id,
                IdProveedor = updateDto.IdProveedor
            });
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MateriaPrimaExists(id))
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




    [HttpDelete]
    [Route("EliminarMateriaPrima/{id:int}")]
    public async Task<IActionResult> DeleteMateriaPrima(int id)
    {
        var materiaPrima = await _context.MateriaPrimas
            .Include(mp => mp.MateriaProveedorIntermedia)
            .FirstOrDefaultAsync(mp => mp.IdMateriaPrima == id);

        if (materiaPrima == null)
        {
            return NotFound();
        }

        _context.MateriaProveedorIntermedia.RemoveRange(materiaPrima.MateriaProveedorIntermedia);

        _context.MateriaPrimas.Remove(materiaPrima);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MateriaPrimaExists(id))
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


    private bool MateriaPrimaExists(int id)
    {
        return _context.MateriaPrimas.Any(e => e.IdMateriaPrima == id);
    }
}
