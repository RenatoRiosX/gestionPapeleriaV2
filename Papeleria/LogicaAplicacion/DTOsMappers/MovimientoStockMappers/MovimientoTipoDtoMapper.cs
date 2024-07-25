using LogicaAplicacion.DTOs.MovimientoStockDTOs;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LogicaAplicacion.DTOsMappers.MovimientoStockMappers
{
    public class MovimientoTipoDtoMapper
    {
        public static MovimientoYTipoDto toDto(MovimientoStock movimiento)
        {
            return new MovimientoYTipoDto
            {
                Id = movimiento.Id,
                FechaMovimiento=movimiento.FechaMovimiento,
                ArticuloId = movimiento.ArticuloId,
                TipoMovimientoId = movimiento.TipoMovimientoId,
                Cantidad = movimiento.Cantidad,
                EncargadoId = movimiento.EncargadoId,
                NombreTipo=movimiento.TipoMovimientoStock.Nombre,
                EsAumentoCantidad=movimiento.TipoMovimientoStock.EsAumentoCantidad,

            };
        }
    }
}
