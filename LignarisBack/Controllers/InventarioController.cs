using LignarisBack.Models;
using LignarisBack.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace Lignaris_Pizza_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : Controller
    {
        private readonly LignarisPizzaContext _baseDatos;

        public InventarioController(LignarisPizzaContext baseDatos)
        {
            _baseDatos = baseDatos;
        }

        // Método GET ListaInventario que devuelve la lista de todas las tareas en la BD
        [HttpGet]
        [Route("ListaInventario")]
        public async Task<IActionResult> Lista()
        {
            var listaMateriasPrimas = await _baseDatos.Inventarios
                .Join(_baseDatos.CompraDetalles,
                      inventario => inventario.IdCompraDetalle,
                      compraDetalle => compraDetalle.IdCompraDetalle,
                      (inventario, compraDetalle) => new { inventario, compraDetalle })
                .Join(_baseDatos.Compras,
                      inventarioYdetalle => inventarioYdetalle.compraDetalle.IdCompra,
                      compra => compra.IdCompra,
                      (inventarioYdetalle, compra) => new { inventarioYdetalle.inventario, inventarioYdetalle.compraDetalle, compra })
                .Join(_baseDatos.MateriaPrimas,
                      inventarioYdetalleYcompra => inventarioYdetalleYcompra.compraDetalle.IdMateriaPrima,
                      materiaPrima => materiaPrima.IdMateriaPrima,
                      (inventarioYdetalleYcompra, materiaPrima) => new InventarioDetalleDto
                      {
                          IdInventario = inventarioYdetalleYcompra.inventario.IdInventario,
                          Nombre = materiaPrima.Nombre,
                          CantidadDisponible = inventarioYdetalleYcompra.inventario.CantidadDisponible,
                          FechaCompra = inventarioYdetalleYcompra.compra.FechaCompra,
                          FechaCaducidad = inventarioYdetalleYcompra.compraDetalle.FechaCaducidad,
                          NumLote = inventarioYdetalleYcompra.compraDetalle.NumLote,
                          Estatus = inventarioYdetalleYcompra.inventario.Estatus
                      })
                .ToListAsync();

            return Ok(listaMateriasPrimas);
        }
    }
}
