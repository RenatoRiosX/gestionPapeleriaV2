using LogicaAplicacion.DTOs.TipoMovimientoStockDTOs;
using LogicaAplicacion.DTOsMappers.ClienteMappers;
using LogicaAplicacion.DTOsMappers.TipoMovimientoStockMapper;
using LogicaAplicacion.InterfacesCasosUsos.TipoMovimientoStock;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.ImplementacionCasosUsos.TipoMovimientosStock
{
    public class GetAllTipoMovimientoStock : IGetAllTipoMovimientoStock
    {
        private IRepositorioTipoMovimientoStock _RepositorioTipoMovimiento;
        public GetAllTipoMovimientoStock(IRepositorioTipoMovimientoStock repositorioTipoMovimiento)
        {
            _RepositorioTipoMovimiento = repositorioTipoMovimiento;
        }
        public IEnumerable<TipoMovimientoStockDto> Ejecutar()
        {
            return _RepositorioTipoMovimiento.GetAll().Select(t => TipoMovimientoStockDtoMapper.toDto(t));
        }
    }
}
