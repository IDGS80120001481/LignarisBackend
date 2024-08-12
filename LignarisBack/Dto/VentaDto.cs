namespace LignarisBack.Dto
{
    public class VentaDto
    {
        public int? IdVenta { get; set; }

        public int? IdEmpleado { get; set; }

        public int? IdCliente { get; set; }

        public int? Estatus { get; set; }

        public DateOnly? FechaVenta { get; set; }

        public double? Total { get; set; }

        public DetalleVentaDto[]? DetalleVenta { get; set; }
    }
}
