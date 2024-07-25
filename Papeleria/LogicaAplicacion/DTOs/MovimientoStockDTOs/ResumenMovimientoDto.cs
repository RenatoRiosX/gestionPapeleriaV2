using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.DTOs.MovimientoStockDTOs
{
    public record ResumenMovimientoDto
    {
        
            public int Anio { get; set; }
            public int TipoMovimientoId { get; set; }
            public int CantidadTotal { get; set; }
        
    }
}
