using LignarisBack.Models;
using System;
using System.Collections.Generic;

namespace LignarisBack.Models;

public partial class Recetum
{
    public int IdReceta { get; set; }

    public string? Nombre { get; set; }

    public string? Foto { get; set; }

    public int? Tamanio { get; set; }

    public double? PrecioUnitario { get; set; }

    public string? descripcion { get; set; }

    public int? Estatus { get; set; }

    public virtual ICollection<CarritoCompras> CarritoCompras { get; set; } = new List<CarritoCompras>();
    public virtual ICollection<RecetaDetalle> RecetaDetalles { get; set; } = new List<RecetaDetalle>();

    public virtual ICollection<VentaDetalle> VentaDetalles { get; set; } = new List<VentaDetalle>();
}
