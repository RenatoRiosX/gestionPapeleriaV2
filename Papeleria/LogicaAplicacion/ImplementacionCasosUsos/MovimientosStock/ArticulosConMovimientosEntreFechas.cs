using LogicaAplicacion.DTOs.ArticuloDTOs;
using LogicaAplicacion.DTOsMappers.ArticulosMappers;
using LogicaAplicacion.InterfacesCasosUsos.MovimientoStock;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.ImplementacionCasosUsos.MovimientosStock
{
    public class ArticulosConMovimientosEntreFechas : IArticulosConMovimientosEntreFechas
    {
        private IRepositorioMovimientoStock _repositorioMovimientoStock;
        public ArticulosConMovimientosEntreFechas(IRepositorioMovimientoStock repositorioMovimientoStock)
        {
            _repositorioMovimientoStock = repositorioMovimientoStock;
        }
        public IEnumerable<ArticuloDto> Ejecutar(DateTime fecha1, DateTime fecha2, int numPagina, int cantidadRegistros)
        {
            //validar parametros
            try
            {
                var articulos = _repositorioMovimientoStock.GetArticulosConMovimientosEntreFechas(fecha1, fecha2, numPagina, cantidadRegistros);
                return articulos.Select(a => ArticuloDtoMapper.toDto(a));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener artículos con movimientos entre fechas", ex);
            }
        }
    }
}
