using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesEntidades;

namespace LogicaNegocio.Entidades
{
    public class TipoMovimientoStock : IEntity, IValidable
    {
        public string Nombre { get; set; }
        public bool EsAumentoCantidad { get; set; }

        public int Id { get; set; }

        public void EsValido()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new TipoMovimientoNoValidoException("El nombre del movimiento no puede ser nulo o vacio");
            }
        }
    }
}
