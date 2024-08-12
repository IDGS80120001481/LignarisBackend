using LignarisBack.Models;
using System;
using System.Collections.Generic;

namespace LignarisBack.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string Puesto { get; set; } = null!;

    public int IdPersona { get; set; }

    public string? IdUsuario { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    public virtual AppUser IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Produccion> Produccions { get; set; } = new List<Produccion>();

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
