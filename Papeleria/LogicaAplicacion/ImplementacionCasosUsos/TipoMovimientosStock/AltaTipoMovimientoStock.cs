using LogicaAplicacion.DTOs.TipoMovimientoStockDTOs;
using LogicaAplicacion.DTOsMappers.TipoMovimientoStockMapper;
using LogicaAplicacion.InterfacesCasosUsos.TipoMovimientoStock;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.ImplementacionCasosUsos.TipoMovimientosStock
{
    public class AltaTipoMovimientoStock : IAltaTipoMovimientoStock
    {
        private IRepositorioTipoMovimientoStock _RepositorioTipoMovimiento;
        public AltaTipoMovimientoStock(IRepositorioTipoMovimientoStock repositorio)
        {
            _RepositorioTipoMovimiento = repositorio;
        }
        public void Ejecutar(TipoMovimientoStockDto TipomovimientoStockDto)
        {
            try
            {
                if (TipomovimientoStockDto == null)
                {
                    throw new TipoMovimientoNoValidoException("TipoMovimientodto no valido");
                }
                TipoMovimientoStock movimiento =  TipoMovimientoStockDtoMapper.FromDto(TipomovimientoStockDto);
                _RepositorioTipoMovimiento.Add(movimiento);
            }catch (TipoMovimientoNoValidoException ex)
            {
                throw ex;
            }
        }
    }
}
