using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesEntidades;
using System.ComponentModel.DataAnnotations.Schema;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaNegocio.Entidades
{
    public class MovimientoStock : IEntity, IValidable
    {

        public int Id { get; set; }
        public DateTime FechaMovimiento { get; set; }

        [ForeignKey(nameof(Articulo))]
        public int ArticuloId { get; set; } // Clave foránea

        [ForeignKey(nameof(TipoMovimientoStock))]
        public int TipoMovimientoId { get; set; } // Clave foránea

        [ForeignKey(nameof(Encargado))]
        public int EncargadoId { get; set; } // Clave foránea

        public int Cantidad { get; set; }

        public Articulo Articulo { get; set; }
        public TipoMovimientoStock TipoMovimientoStock { get; set; }
        public Encargado Encargado { get; set; }

        public void EsValido()
        {
            if (ArticuloId == 0)
            {
                throw new MovimientoNoValidoException("Articulo no valido");
            }

            if (TipoMovimientoId == 0)
            {
                throw new MovimientoNoValidoException("TipoMovimiento no valido");
            }

            if (EncargadoId == 0)
            {
                throw new MovimientoNoValidoException("Encargado no valido");
            }

            if (Cantidad <= 0)
            {
                throw new MovimientoNoValidoException("Cantidad no valida");
            }

        }
    }
}
