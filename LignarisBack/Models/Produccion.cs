using System;
using System.Collections.Generic;

namespace LignarisBack.Models;

public partial class Produccion
{
    public int IdProduccion { get; set; }

    public int? IdVenta { get; set; }

    public int? IdEmpleado { get; set; }

    public DateTime? FechaProduccion { get; set; }

    public virtual Empleado? IdEmpleadoNavigation { get; set; }

    public virtual Ventum? IdVentaNavigation { get; set; }
}
