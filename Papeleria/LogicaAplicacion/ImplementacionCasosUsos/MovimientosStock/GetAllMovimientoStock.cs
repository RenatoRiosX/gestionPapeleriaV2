using LogicaAplicacion.DTOs.MovimientoStockDTOs;
using LogicaAplicacion.DTOsMappers.MovimientoStockMappers;
using LogicaAplicacion.DTOsMappers.TipoMovimientoStockMapper;
using LogicaAplicacion.InterfacesCasosUsos.MovimientoStock;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.ImplementacionCasosUsos.MovimientosStock
{
    public class GetAllMovimientoStock : IGetAllMovimientoStock
    {
        private IRepositorioMovimientoStock _repositorioMovimientoStock;
        public GetAllMovimientoStock(IRepositorioMovimientoStock repositorioMovimientoStock)
        {
            _repositorioMovimientoStock = repositorioMovimientoStock;
        }

        public IEnumerable<MovimientoStockDto> Ejecutar()
        {
            return _repositorioMovimientoStock.GetAll().Select(t => MovimientoStockDtoMapper.toDto(t));
        }
    }
}
