using System.ComponentModel.DataAnnotations;

namespace ClientePapeleria.Models
{
    public class Articulo
    {
        public int Id { get; set; }

        [StringLength(200, MinimumLength = 10, ErrorMessage = "Largo del nombre: entre 10 y 200 caracteres")]

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        [StringLength(13, MinimumLength = 13, ErrorMessage = "Largo del código: 13 caracteres")]
        public string Codigo { get; set; }

        public double PrecioActual { get; set; }
    }
}
