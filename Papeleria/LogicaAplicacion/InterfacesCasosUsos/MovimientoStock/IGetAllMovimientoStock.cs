using LogicaAplicacion.DTOs.ArticuloDTOs;
using LogicaAplicacion.DTOs.MovimientoStockDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosUsos.MovimientoStock
{
    public interface IGetAllMovimientoStock
    {
        public IEnumerable<MovimientoStockDto> Ejecutar();
    }
}
