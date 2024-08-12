using System.ComponentModel.DataAnnotations;

namespace LignarisBack.Dto
{
    public class RegisterDto
    {
        public int IdPersona { get; set; }
        public int IdCliente { get; set;}
        public int IdUsuario { get; set;}

        [Required]
        public string Nombre { get; set; } = null!;
        [Required]
        public string ApellidoPaterno { get; set; } = null!;

        public string ApellidoMaterno { get; set; } = null!;

        public string? Telefono { get; set; }

        public string? Direccion { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public List<string>? Roles { get; set; }
    }
}
