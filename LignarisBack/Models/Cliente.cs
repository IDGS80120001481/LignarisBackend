using LignarisBack.Models;
using System;
using System.Collections.Generic;

namespace LignarisBack.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public int IdPersona { get; set; }

    public string? IdUsuario { get; set; }

    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    public virtual AppUser IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
    public virtual ICollection<CarritoCompras> CarritoCompras { get; set; } = new List<CarritoCompras>();
}
