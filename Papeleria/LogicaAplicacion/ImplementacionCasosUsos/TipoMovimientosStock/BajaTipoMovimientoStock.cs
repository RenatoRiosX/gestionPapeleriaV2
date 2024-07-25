using LogicaAplicacion.DTOs.TipoMovimientoStockDTOs;
using LogicaAplicacion.InterfacesCasosUsos.TipoMovimientoStock;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.ImplementacionCasosUsos.TipoMovimientosStock
{
    public class BajaTipoMovimientoStock : IBajaTipoMovimientoStock
    {
        private IRepositorioTipoMovimientoStock _RepositorioTipoMovimiento;
        public BajaTipoMovimientoStock(IRepositorioTipoMovimientoStock repositorio)
        {
            _RepositorioTipoMovimiento = repositorio;
        }
        public void Ejecutar(int id)
        {
                _RepositorioTipoMovimiento.Remove(id);
        }
    }
}
