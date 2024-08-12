using LignarisBack.Dto;
using LignarisBack.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LignarisBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoCompraController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly LignarisPizzaContext _basedatos;

        public CarritoCompraController(UserManager<AppUser> userManager, RoleManager<IdentityRole>
       roleManager, IConfiguration configuration, LignarisPizzaContext basedatos)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _basedatos = basedatos;
        }

        /*
         * Metodo para obtener las recetas para el carrito de compra para ser mostradas en la interfaz del
         * carrito de compras
         */
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecetaVentasDto>>> GetRecetasVenta()
        {

            return await _basedatos.Receta
                .Select(x => mapRecetaDto(x))
                .ToListAsync();
        }

        /*
         * Metodo para obtener un solo elemento del carrito de compras
         */
        [HttpGet("{id}")]
        public async Task<ActionResult<RecetaVentasDto>> GetRecetaIDVenta(int id)
        {
            var receta = await _basedatos.Receta.FindAsync(id);

            if (receta == null)
            {
                return NotFound();
            }

            return mapRecetaDto(receta);
        }

        private static RecetaVentasDto mapRecetaDto(Recetum receta) =>
           new RecetaVentasDto
           {
               IdReceta = receta.IdReceta,
               Nombre = receta.Nombre,
               Descripcion = receta.descripcion,
               Foto = receta.Foto,
               Tamanio = receta.Tamanio,
               PrecioUnitario = receta.PrecioUnitario,
               Estatus = receta.Estatus
           };


        /*
         * Metodo para agregar elementos a mi carrito de compras
         */
        [HttpPost]
        public async Task<IActionResult> AgregarCarritoCompra(string idUsuario, int IdReceta)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;

            if (string.IsNullOrEmpty(idUsuario))
            {
                return BadRequest("El ID de usuario no puede estar vacío.");
            }

            try
            {
                // Se busca al usuario para agregar al carrito
                var cliente = await _basedatos.Clientes
                                             .Where(c => c.IdUsuario!.Contains(idUsuario))
                                             .FirstOrDefaultAsync();

                if (cliente == null)
                {
                    return NotFound("Cliente no encontrado.");
                }

                int Cantidad = 1;
                var compras = await _basedatos.CarritoCompras
                                              .Where(c => c.IdCliente == cliente.IdCliente)
                                              .Where(c => c.IdRecetas == IdReceta)
                                              .FirstOrDefaultAsync();

                if (compras == null)
                {
                    var carrito = new CarritoCompras
                    {
                        IdRecetas = IdReceta,
                        IdCliente = cliente.IdCliente,
                        Cantidad = Cantidad
                    };

                    await _basedatos.CarritoCompras.AddAsync(carrito);
                    await _basedatos.SaveChangesAsync();
                    return Ok("Se ha insertado un nuevo objeto al carrito de compras");
                }
                else
                {
                    compras!.Cantidad += 1;
                    await _basedatos.SaveChangesAsync();
                    return Ok("Se ha modifica un objeto en el carrito de compras");
                }

                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Error interno del servidor.");
            }
        }


        [HttpPut]
        public async Task<IActionResult> AumentarCarritoCompras([FromBody] string idUsuario, int IdReceta)
        {
            var user = await _userManager.FindByIdAsync(idUsuario);
            var cliente = await _basedatos.Clientes.Where(c => c.IdUsuario == user!.Id).ToListAsync();

            var compras = await _basedatos.CarritoCompras.
                Where(c => c.IdCliente == cliente[0].IdCliente).
                Where(c => c.IdRecetas == IdReceta).
                ToListAsync();

            compras[0].Cantidad = compras[0].Cantidad;

            var carrito = new CarritoCompras
            {
                IdRecetas = compras[0].IdRecetas,
                IdCliente = compras[0].IdCliente,
                Cantidad = compras[0].Cantidad
            };

            _basedatos.SaveChanges();

            return Ok("Se ha aumentado un elemento en el carrito");
        }

        /*[HttpGet]
        [Route("searchproduct{IdReceta}/{IdCliente}")]
        public async Task<IActionResult> SearchProduct(int IdReceta, int IdCliente)
        {
            var carrito = await _basedatos.CarritoCompras
                .Where(c => c.IdRecetas == IdReceta)
                .Where(c => c.IdCliente == IdCliente)
                                     .ToListAsync();

            if (carrito[0].IdRecetas > 0 || carrito[0].)
            {
                return Ok(new { producto = true });
            }
            else
            {
                return Ok(new { producto = false });
            }
        }*/
    }
}
