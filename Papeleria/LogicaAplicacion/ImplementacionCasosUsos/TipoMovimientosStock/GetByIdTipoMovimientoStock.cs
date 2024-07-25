using LogicaAplicacion.DTOs.TipoMovimientoStockDTOs;
using LogicaAplicacion.DTOsMappers.ClienteMappers;
using LogicaAplicacion.DTOsMappers.TipoMovimientoStockMapper;
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
    public class GetByIdTipoMovimientoStock : IGetByIdTipoMovimientoStock
    {
        private IRepositorioTipoMovimientoStock _RepositorioTipoMovimiento;
        public GetByIdTipoMovimientoStock(IRepositorioTipoMovimientoStock repositorioTipoMovimiento)
        {
            _RepositorioTipoMovimiento = repositorioTipoMovimiento;
        }

        public TipoMovimientoStockDto Ejecutar(int id)
        {
            TipoMovimientoStock tipoMovimientoStock= _RepositorioTipoMovimiento.GetById(id);
            return TipoMovimientoStockDtoMapper.toDto(tipoMovimientoStock);

        }
    }
}
