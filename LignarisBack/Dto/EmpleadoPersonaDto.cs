namespace LignarisBack.Dto
{
    public class EmpleadoPersonaDto
    {
        public int IdEmpleado { get; set; }
        public int IdPersona { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido_Paterno { get; set; }
        public string? Apellido_Materno { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Email { get; set; }
    }
}
