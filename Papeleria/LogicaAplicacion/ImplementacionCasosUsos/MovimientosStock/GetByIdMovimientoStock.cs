using LogicaAplicacion.DTOs.MovimientoStockDTOs;
using LogicaAplicacion.DTOsMappers.MovimientoStockMappers;
using LogicaAplicacion.DTOsMappers.TipoMovimientoStockMapper;
using LogicaAplicacion.InterfacesCasosUsos.MovimientoStock;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.ImplementacionCasosUsos.MovimientosStock
{
    public class GetByIdMovimientoStock : IGetByIdMovimientoStock
    {
        private IRepositorioMovimientoStock _repositorioMovimientoStock;
        public GetByIdMovimientoStock(IRepositorioMovimientoStock repositorioMovimientoStock)
        {
            _repositorioMovimientoStock = repositorioMovimientoStock;
        }
        public MovimientoStockDto Ejecutar(int id)
        {
            MovimientoStock MovimientoStock = _repositorioMovimientoStock.GetById(id);
            return MovimientoStockDtoMapper.toDto(MovimientoStock);
        }
    }
}
