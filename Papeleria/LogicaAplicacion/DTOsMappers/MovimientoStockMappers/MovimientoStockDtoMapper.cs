using LogicaAplicacion.DTOs.ClienteDTOs;
using LogicaAplicacion.DTOs.MovimientoStockDTOs;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.DTOsMappers.MovimientoStockMappers
{
    public class MovimientoStockDtoMapper
    {
        public static MovimientoStockDto toDto(MovimientoStock movimiento)
        {
            return new MovimientoStockDto
            {
                Id = movimiento.Id,
                ArticuloId = movimiento.ArticuloId,
                TipoMovimientoId=movimiento.TipoMovimientoId,
                Cantidad=movimiento.Cantidad,
                EncargadoId=movimiento.EncargadoId,
                EncargadoEmail= movimiento.Encargado.Email,
                FechaMovimiento=movimiento.FechaMovimiento
            };
        }

    public static MovimientoStock FromDto(MovimientoStockDto movimientoDto) 
        {
            return new MovimientoStock
            {
                Id = movimientoDto.Id,
                ArticuloId = movimientoDto.ArticuloId,
                TipoMovimientoId = movimientoDto.TipoMovimientoId,
                Cantidad = movimientoDto.Cantidad,
                EncargadoId = movimientoDto.EncargadoId,
                FechaMovimiento = movimientoDto.FechaMovimiento

            };
        }
    }
}
