using LogicaAplicacion.DTOs.MovimientoStockDTOs;
using LogicaAplicacion.DTOs.TipoMovimientoStockDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosUsos.MovimientoStock
{
    public interface IGetMovimientoStockTipoArticulo
    {
        public IEnumerable<MovimientoYTipoDto> Ejecutar(int idArticulo,int idTipoMovimiento, int numPagina, int cantidadRegistros);

    }
}
