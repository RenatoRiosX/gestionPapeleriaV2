namespace ClientePapeleria.Models
{
	public class ArticulosYTiposDeMovimiento
	{
		public IEnumerable<Articulo> Articulos { get; set; }
		public IEnumerable<TipoMovimientoStock> TiposDeMovimientoStock { get; set; }
	}
}
