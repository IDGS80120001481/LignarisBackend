using System;
using System.Collections.Generic;

namespace LignarisBack.Models;

public partial class Proveedor
{
    public int IdProveedor { get; set; }

    public int IdPersona { get; set; }

    public int? Estatus { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    public virtual ICollection<MateriaProveedorIntermedium> MateriaProveedorIntermedia { get; set; } = new List<MateriaProveedorIntermedium>();
}
