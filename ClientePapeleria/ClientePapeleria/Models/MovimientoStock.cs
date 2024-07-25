namespace ClientePapeleria.Models
{
    public class MovimientoStock
    {
        public int Id { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public int ArticuloId { get; set; }
        public int TipoMovimientoId { get; set; }
        public int Cantidad { get; set; }
        public int EncargadoId { get; set; }
        public string EncargadoEmail { get; set; }

    }
}
