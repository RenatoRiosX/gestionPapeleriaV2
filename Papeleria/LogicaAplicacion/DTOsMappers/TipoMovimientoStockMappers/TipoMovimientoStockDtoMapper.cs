using LogicaAplicacion.DTOs.MovimientoStockDTOs;
using LogicaAplicacion.DTOs.TipoMovimientoStockDTOs;
using LogicaAplicacion.DTOs.UsuarioDTOs;
using LogicaNegocio.Entidades;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.DTOsMappers.TipoMovimientoStockMapper
{
    public class TipoMovimientoStockDtoMapper
    {
        public static TipoMovimientoStockDto toDto(TipoMovimientoStock TipoMovimiento)
        {
            return new TipoMovimientoStockDto
            {
                Id = TipoMovimiento.Id,
                Nombre = TipoMovimiento.Nombre,
                EsAumentoCantidad= TipoMovimiento.EsAumentoCantidad,
            };
        }

        public static TipoMovimientoStock FromDto(TipoMovimientoStockDto TipoMovimientoDto)
        {
            return new TipoMovimientoStock
            {
                Id = TipoMovimientoDto.Id,
                Nombre = TipoMovimientoDto.Nombre,
                EsAumentoCantidad = TipoMovimientoDto.EsAumentoCantidad,

            };
        }
        public static void UpdateFromDto(TipoMovimientoStockDto TipoMovimientoDto, TipoMovimientoStock TipoMovimientoExistente)
        {

            TipoMovimientoExistente.Nombre = TipoMovimientoDto.Nombre;
            TipoMovimientoExistente.EsAumentoCantidad = TipoMovimientoDto.EsAumentoCantidad;
        }
    }
}
