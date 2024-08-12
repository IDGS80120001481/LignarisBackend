using System;
using System.Collections.Generic;

namespace LignarisBack.Models;

public partial class MateriaPrima
{
    public int IdMateriaPrima { get; set; }

    public string? Nombre { get; set; }

    public string? TipoMedida { get; set; }

    public decimal? CantidadMinima { get; set; }

    public virtual ICollection<CompraDetalle> CompraDetalles { get; set; } = new List<CompraDetalle>();

    public virtual ICollection<MateriaProveedorIntermedium> MateriaProveedorIntermedia { get; set; } = new List<MateriaProveedorIntermedium>();

    public virtual ICollection<RecetaDetalle> RecetaDetalles { get; set; } = new List<RecetaDetalle>();
}
