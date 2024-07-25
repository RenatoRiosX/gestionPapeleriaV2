using LogicaAplicacion.DTOs.MovimientoStockDTOs;
using LogicaAplicacion.DTOs.TipoMovimientoStockDTOs;
using LogicaAplicacion.DTOsMappers.MovimientoStockMappers;
using LogicaAplicacion.DTOsMappers.TipoMovimientoStockMapper;
using LogicaAplicacion.InterfacesCasosUsos.MovimientoStock;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.ImplementacionCasosUsos.MovimientosStock
{
    public class GetMovimientoStockTipoArticulo : IGetMovimientoStockTipoArticulo
    {
        private IRepositorioMovimientoStock _repositorioMovimientoStock;
        public GetMovimientoStockTipoArticulo(IRepositorioMovimientoStock repositorioMovimientoStock)
        {
            _repositorioMovimientoStock = repositorioMovimientoStock;
        }
        public IEnumerable<MovimientoYTipoDto> Ejecutar(int idArticulo, int idTipoMovimiento, int numPagina, int cantidadRegistros)
        {
            try
            {
                if (idArticulo == 0 && idTipoMovimiento == 0)
                {
                    throw new TipoMovimientoNoValidoException("Tipo no válido");
                }

                var movimientos = _repositorioMovimientoStock.GetPorArticuloYTipo(idArticulo, idTipoMovimiento, numPagina, cantidadRegistros);
                return movimientos.Select(t => MovimientoTipoDtoMapper.toDto(t));
            }
            catch (TipoMovimientoNoValidoException ex)
            {
                throw ex;
            }
        }
    }
}
