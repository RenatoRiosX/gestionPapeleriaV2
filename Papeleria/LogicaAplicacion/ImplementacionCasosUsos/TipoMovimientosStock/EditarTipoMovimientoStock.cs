using LogicaAplicacion.DTOs.TipoMovimientoStockDTOs;
using LogicaAplicacion.DTOs.UsuarioDTOs;
using LogicaAplicacion.DTOsMappers.TipoMovimientoStockMapper;
using LogicaAplicacion.DTOsMappers.UsuarioMappers;
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
    public class EditarTipoMovimientoStock : IEditarTipoMovimientoStock
    {
        private IRepositorioTipoMovimientoStock _RepositorioTipoMovimiento;
        public EditarTipoMovimientoStock(IRepositorioTipoMovimientoStock repositorioTipoMovimiento)
        {
            _RepositorioTipoMovimiento = repositorioTipoMovimiento;
        }
        public void Ejecutar(TipoMovimientoStockDto TipomovimientoStockDto, int id)
        {
            if (TipomovimientoStockDto != null)
            {

                TipoMovimientoStock tipoMovimientoExistente = _RepositorioTipoMovimiento.GetById(id);


                //Actualiza el Tipo de movimiento existente con los datos del DTO
                TipoMovimientoStockDtoMapper.UpdateFromDto(TipomovimientoStockDto, tipoMovimientoExistente);
                tipoMovimientoExistente.EsValido();

                _RepositorioTipoMovimiento.Update(tipoMovimientoExistente);

            }
        }
    }
}
