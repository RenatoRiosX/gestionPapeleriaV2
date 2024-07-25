using LogicaAplicacion.DTOs.ArticuloDTOs;
using LogicaAplicacion.DTOs.MovimientoStockDTOs;
using LogicaAplicacion.DTOs.TipoMovimientoStockDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosUsos.MovimientoStock
{
    public interface IArticulosConMovimientosEntreFechas
    {
        public IEnumerable<ArticuloDto> Ejecutar(DateTime fecha1,DateTime fecha2,int numPagina, int cantidadRegistros);

    }
}
