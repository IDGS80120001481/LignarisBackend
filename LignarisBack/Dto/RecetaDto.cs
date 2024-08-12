using LignarisBack.Dto;

namespace LignarisBack.Dto
{
    public class RecetaDto
    {
        public int IdReceta { get; set; }
        public string? Nombre { get; set; }
        public string? Foto { get; set; }
        public int? Tamanio { get; set; }
        public double? PrecioUnitario { get; set; }
        public int? Estatus { get; set; }
        public List<RecetaDetalleDto>? RecetaDetalles { get; set; }
    }
}
