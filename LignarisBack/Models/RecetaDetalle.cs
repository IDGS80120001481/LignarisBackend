using System;
using System.Collections.Generic;

namespace LignarisBack.Models;

public partial class RecetaDetalle
{
    public int IdRecetaDetalle { get; set; }

    public int? IdReceta { get; set; }

    public int IdMateriaPrima { get; set; }

    public decimal? Cantidad { get; set; }

    public virtual MateriaPrima? IdMateriaPrimaNavigation { get; set; }

    public virtual Recetum? IdRecetaNavigation { get; set; }
}
