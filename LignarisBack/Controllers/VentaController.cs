using LignarisBack.Models;
using LignarisBack.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace LignarisBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {

        private readonly LignarisPizzaContext _basedatos;

        public VentaController(LignarisPizzaContext basedatos)
        {
            _basedatos = basedatos;
        }

        [HttpPost]
        public async Task<ActionResult<VentaDto>> InsertVenta(VentaDto _venta)
        {
            var venta = new Ventum
            {
                IdEmpleado = _venta.IdEmpleado,
                IdCliente = _venta.IdCliente,
                Estatus = _venta.Estatus,
                FechaVenta = _venta.FechaVenta,
                Total = _venta.Total
            };

            _basedatos.Venta.Add(venta);
            await _basedatos.SaveChangesAsync();

            int idVenta = venta.IdVenta;

            for (int i = 0; i < _venta.DetalleVenta!.Length; i++)
            {
                var detalleVenta = new VentaDetalle
                {
                    IdVenta = idVenta,
                    IdReceta = _venta.DetalleVenta[i].IdReceta,
                    Cantidad = _venta.DetalleVenta[i].Cantidad
                };

                _basedatos.VentaDetalles.Add(detalleVenta);
                await _basedatos.SaveChangesAsync();
            }

            return Ok("Se ha realizado correctamente la venta: " + idVenta);
        }


    }
}
