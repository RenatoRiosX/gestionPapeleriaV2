using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.DTOs.TipoMovimientoStockDTOs
{
    public class TipoMovimientoStockDto
    {
        public string Nombre { get; set; }
        public bool EsAumentoCantidad { get; set; }
        public int Id { get; set; }
    }
}
