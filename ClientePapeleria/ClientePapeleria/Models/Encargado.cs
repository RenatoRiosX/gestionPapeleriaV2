using System.ComponentModel.DataAnnotations;

namespace ClientePapeleria.Models
{
    public class Encargado
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public string Rol { get; set; }

        public NombreCompleto NombreCompleto { get; set; }

        public string Contrasenia { get; set; }
        public string ContraseniaSinEncriptar { get; set; }
    }
}
