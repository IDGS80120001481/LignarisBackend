using System;
using System.Collections.Generic;

namespace LignarisBack.Models;

public partial class CompraDetalle
{
    public int IdCompraDetalle { get; set; }

    public int? IdCompra { get; set; }

    public int? IdMateriaPrima { get; set; }

    public int? PrecioUnitario { get; set; }

    public decimal? Cantidad { get; set; }

    public string? NumLote { get; set; }

    public DateOnly? FechaCaducidad { get; set; }

    public virtual Compra? IdCompraNavigation { get; set; }

    public virtual MateriaPrima? IdMateriaPrimaNavigation { get; set; }

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
