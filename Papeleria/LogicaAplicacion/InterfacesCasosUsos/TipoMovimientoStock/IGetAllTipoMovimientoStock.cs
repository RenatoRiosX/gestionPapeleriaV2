using LogicaAplicacion.DTOs.MovimientoStockDTOs;
using LogicaAplicacion.DTOs.TipoMovimientoStockDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosUsos.TipoMovimientoStock
{
    public interface IGetAllTipoMovimientoStock
    {
        public IEnumerable<TipoMovimientoStockDto> Ejecutar();

    }
}
