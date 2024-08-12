namespace LignarisBack.Dto
{
    public class CompraDetalleDto
    {
        public int? IdMateriaPrima { get; set; }
        public int? PrecioUnitario { get; set; }
        public decimal? Cantidad { get; set; }
        public string? NumLote { get; set; }
        public DateTime FechaCaducidad { get; set; }
    }
}
