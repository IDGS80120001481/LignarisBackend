using System;
using System.Collections.Generic;

namespace LignarisBack.Models;

public partial class Ventum
{
    public int IdVenta { get; set; }

    public int? IdEmpleado { get; set; }

    public int? IdCliente { get; set; }

    public int? Estatus { get; set; }

    public DateOnly? FechaVenta { get; set; }

    public double? Total { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Empleado? IdEmpleadoNavigation { get; set; }

    public virtual ICollection<Produccion> Produccions { get; set; } = new List<Produccion>();

    public virtual ICollection<VentaDetalle> VentaDetalles { get; set; } = new List<VentaDetalle>();
}
