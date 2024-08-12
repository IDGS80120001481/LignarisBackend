using System;
using System.Collections.Generic;

namespace LignarisBack.Models;

public partial class Inventario
{
    public int IdInventario { get; set; }

    public int? IdCompraDetalle { get; set; }

    public decimal? CantidadDisponible { get; set; }

    public int? Estatus { get; set; }

    public virtual CompraDetalle? IdCompraDetalleNavigation { get; set; }
}
