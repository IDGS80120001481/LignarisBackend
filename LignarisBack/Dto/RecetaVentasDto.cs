namespace LignarisBack.Dto
{
    public class RecetaVentasDto
    {
        public int IdReceta { get; set; }

        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }

        public string? Foto { get; set; }

        public int? Tamanio { get; set; }

        public double? PrecioUnitario { get; set; }

        public int? Estatus { get; set; }
    }
}
