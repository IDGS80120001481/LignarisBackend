using System;
using System.Collections.Generic;

namespace LignarisBack.Models;

public partial class Compra
{
    public int IdCompra { get; set; }

    public int IdEmpleado { get; set; }

    public int IdProveedor { get; set; }

    public DateOnly FechaCompra { get; set; }

    public virtual ICollection<CompraDetalle> CompraDetalles { get; set; } = new List<CompraDetalle>();

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;

    public virtual Proveedor IdProveedorNavigation { get; set; } = null!;
}
