using System.ComponentModel.DataAnnotations;

namespace LignarisBack.Dto
{
    public class CrearRoleDto
    {
        public class CreateRole
        {
            [Required(ErrorMessage = "El nombre del rol es requerido")]
            public string RoleName { get; set; } = null!;
        }
    }
}
