using LogicaAplicacion.DTOs.MovimientoStockDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosUsos.MovimientoStock
{
    public interface IGetByIdMovimientoStock
    {
        MovimientoStockDto Ejecutar(int id);

    }
}
