namespace LignarisBack.Dto
{
    public class DetalleVentaDto
    {
        public int? IdVentaDetalle { get; set; }
        public int? idVenta { get; set; }
        public int? IdReceta { get; set; }
        public decimal? Cantidad { get; set; }
    }
}
