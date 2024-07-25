namespace ClientePapeleria.Models
{
    public class MovimientoDeArticulos
    {
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int Pagina { get; set; }
        public IEnumerable<Articulo> Articulos { get; set; }
    }
}
