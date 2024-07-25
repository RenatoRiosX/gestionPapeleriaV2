using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.DTOs.MovimientoStockDTOs
{
    public class MovimientoYTipoDto
    {
        public int Id { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public int ArticuloId { get; set; } // Clave foránea
        public int TipoMovimientoId { get; set; } // Clave foránea
        public int Cantidad { get; set; }
        public int EncargadoId { get; set; }
        public string NombreTipo { get; set; }
        public bool EsAumentoCantidad { get; set; }
    }
}
