namespace LignarisBack.Dto
{
    public class CompraDto
    {
        public int IdEmpleado { get; set; }
        public int IdProveedor { get; set; }
        public DateTime FechaCompra { get; set; }
        public List<CompraDetalleDto> DetallesCompra { get; set; } = new List<CompraDetalleDto>();
        public List<InventarioDto> Inventarios { get; set; } = new List<InventarioDto>();
    }
}
