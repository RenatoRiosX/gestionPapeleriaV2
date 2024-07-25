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
using LogicaAplicacion.InterfacesCasosUsos.Configuraciones;

namespace LogicaAplicacion.ImplementacionCasosUsos.MovimientosStock
{
    public class AltaMovimientoStock : IAltaMovimientoStock
    {
        private readonly IRepositorioMovimientoStock _repositorioMovimientoStock;

        public AltaMovimientoStock(IRepositorioMovimientoStock repositorioMovimientoStock)
        {
            _repositorioMovimientoStock = repositorioMovimientoStock;

        }
        public void Ejecutar(MovimientoStockDto movimientoStockDto)
        {
            try
            {
                if (movimientoStockDto == null)
                {
                    throw new MovimientoNoValidoException("Movimiento Stock no valido");
                }

                MovimientoStock movimiento = MovimientoStockDtoMapper.FromDto(movimientoStockDto);
                _repositorioMovimientoStock.Add(movimiento);
            }
            catch (MovimientoNoValidoException ex)
            {
                throw ex;
            }
        }
    }
}
