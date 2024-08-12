namespace LignarisBack.Dto
{
    public class MateriaPrimaUpdateDto
    {
        public int IdMateriaPrima { get; set; }
        public string Nombre { get; set; }
        public string TipoMedida { get; set; }
        public decimal CantidadMinima { get; set; }
        public int IdProveedor { get; set; }
    }
}
