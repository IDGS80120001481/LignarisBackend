using LignarisBack.Models;

namespace LignarisBack.Models
{
    public class CarritoCompras
    {
        public int? IdCarritoCompras { get; set; }
        public int Cantidad { get; set; }

        public int IdRecetas { get; set; }

        public int? IdCliente { get; set; }

        public virtual Cliente? Cliente { get; set; }

        public virtual Recetum? Recetum { get; set; }
    }
}
