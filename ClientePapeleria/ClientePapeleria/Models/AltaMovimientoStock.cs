namespace ClientePapeleria.Models
{
    public class AltaMovimientoStock
    {
        public IEnumerable<Articulo> Articulos { get; set; }
        public IEnumerable<TipoMovimientoStock> TiposDeMovimientos { get; set; }

        public int IdArticulo { get; set; }
        public int IdTipoMovimiento { get; set; }
        public int IdEncargado { get; set; }
        public int Cantidad { get; set; }

    }
}
