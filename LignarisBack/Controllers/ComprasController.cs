using LignarisBack.Dto;
using LignarisBack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Lignaris_Pizza_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : Controller
    {
        //Creando la variable de context de BD
        private readonly LignarisPizzaContext _baseDatos;

        public ComprasController(LignarisPizzaContext baseDatos)
        {
            _baseDatos = baseDatos;
        }

        // Método GET ProveedoresParaCompras que devuelve la lista de todas las tareas en la BD
        [HttpGet]
        [Route("ProveedoresParaCompras")]
        public async Task<IActionResult> ProveedoresParaCompras()
        {
            var proveedoresParaCompras = await _baseDatos.Proveedors
                .Join(_baseDatos.Personas,
                      proveedor => proveedor.IdPersona,
                      persona => persona.IdPersona,
                      (proveedor, persona) => new ProveedorPersonaDto
                      {
                          IdProveedor = proveedor.IdProveedor,
                          IdPersona = persona.IdPersona,
                          Nombre = persona.Nombre,
                          Apellido_Paterno = persona.ApellidoPaterno,
                          Apellido_Materno = persona.ApellidoMaterno,
                          Telefono = persona.Telefono,
                          Direccion = persona.Direccion,
                          Email = persona.Email
                      })
                .ToListAsync();

            return Ok(proveedoresParaCompras);
        }

        // Método GET EmpleadosParaCompras que devuelve la lista de todos los empleados en la BD
        [HttpGet]
        [Route("EmpleadosParaCompras")]
        public async Task<IActionResult> EmpleadosParaCompras()
        {
            var empleadosParaCompras = await _baseDatos.Empleados
                .Join(_baseDatos.Personas,
                      empleado => empleado.IdPersona,
                      persona => persona.IdPersona,
                      (empleado, persona) => new EmpleadoPersonaDto
                      {
                          IdEmpleado = empleado.IdEmpleado,
                          IdPersona = persona.IdPersona,
                          Nombre = persona.Nombre,
                          Apellido_Paterno = persona.ApellidoPaterno,
                          Apellido_Materno = persona.ApellidoMaterno,
                          Telefono = persona.Telefono,
                          Direccion = persona.Direccion,
                          Email = persona.Email
                      })
                .ToListAsync();

            return Ok(empleadosParaCompras);
        }

        // Método GET ListaMateriasPrimas que devuelve la lista de todas las materias primas en la BD
        [HttpGet]
        [Route("ListaMateriasPrimas")]
        public async Task<IActionResult> ListaMateriasPrimas()
        {
            var listaMateriasPrimas = await _baseDatos.MateriaPrimas
                .Select(materiaPrima => new MateriaPrimaDto
                {
                    IdMateriaPrima = materiaPrima.IdMateriaPrima,
                    Nombre = materiaPrima.Nombre,
                    TipoMedida = materiaPrima.TipoMedida,
                    CantidadMinima = materiaPrima.CantidadMinima.HasValue ? (int)materiaPrima.CantidadMinima.Value : 0
                })
                .ToListAsync();

            return Ok(listaMateriasPrimas);
        }

        [HttpGet]
        [Route("ListaMateriasPrimasProveedor")]
        public async Task<IActionResult> ListaMateriasPrimas(int idProveedor)
        {
            var listaMateriasPrimas = await _baseDatos.MateriaProveedorIntermedia
                .Where(mpi => mpi.IdProveedor == idProveedor)
                .Select(mpi => new MateriaPrimaDto
                {
                    IdMateriaPrima = mpi.IdMateriaPrimaNavigation.IdMateriaPrima,
                    Nombre = mpi.IdMateriaPrimaNavigation.Nombre,
                    TipoMedida = mpi.IdMateriaPrimaNavigation.TipoMedida,
                    CantidadMinima = mpi.IdMateriaPrimaNavigation.CantidadMinima.HasValue ? (int)mpi.IdMateriaPrimaNavigation.CantidadMinima.Value : 0
                })
                .ToListAsync();

            return Ok(listaMateriasPrimas);
        }

        [HttpPost]
        [Route("ComprarMateriaPrima")]
        public async Task<IActionResult> ComprarMateriaPrima([FromBody] CompraDto request)
        {
            var compra = new Compra
            {
                IdEmpleado = request.IdEmpleado,
                IdProveedor = request.IdProveedor,
                FechaCompra = DateOnly.FromDateTime(request.FechaCompra)
            };

            await _baseDatos.Compras.AddAsync(compra);
            await _baseDatos.SaveChangesAsync();

            // Insertar los detalles de la compra
            var detallesCompra = request.DetallesCompra.Select(d => new CompraDetalle
            {
                IdCompra = compra.IdCompra, // ID de la compra recién insertada
                IdMateriaPrima = d.IdMateriaPrima,
                PrecioUnitario = d.PrecioUnitario,
                Cantidad = d.Cantidad,
                NumLote = d.NumLote,
                FechaCaducidad = DateOnly.FromDateTime(d.FechaCaducidad)
            }).ToList();

            await _baseDatos.CompraDetalles.AddRangeAsync(detallesCompra);
            await _baseDatos.SaveChangesAsync();

            // Recuperar los IDs generados para los detalles de la compra
            foreach (var detalle in detallesCompra)
            {
                var detalleInsertado = await _baseDatos.CompraDetalles
                    .Where(d => d.IdCompra == detalle.IdCompra && d.IdMateriaPrima == detalle.IdMateriaPrima && d.NumLote == detalle.NumLote)
                    .FirstOrDefaultAsync();
                detalle.IdCompraDetalle = detalleInsertado.IdCompraDetalle;
            }

            // Insertar el inventario nuevo
            var inventario_nuevo = detallesCompra.Select(d => new Inventario
            {
                IdCompraDetalle = d.IdCompraDetalle,
                CantidadDisponible = d.Cantidad,
                Estatus = 1
            }).ToList();

            await _baseDatos.Inventarios.AddRangeAsync(inventario_nuevo);
            await _baseDatos.SaveChangesAsync();

            return Ok("Se ha insertado un nuevo objeto al inventario");
        }
    }
}
