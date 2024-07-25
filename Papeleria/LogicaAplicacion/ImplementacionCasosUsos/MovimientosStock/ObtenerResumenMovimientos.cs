using LogicaAplicacion.DTOs.MovimientoStockDTOs;
using LogicaAplicacion.InterfacesCasosUsos.MovimientoStock;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.ImplementacionCasosUsos.MovimientosStock
{
    public class ObtenerResumenMovimientos : IObtenerResumenMovimientos
    {
        private readonly IRepositorioMovimientoStock _repositorioMovimientoStock;

        public ObtenerResumenMovimientos(IRepositorioMovimientoStock repositorioMovimientoStock)
        {
            _repositorioMovimientoStock = repositorioMovimientoStock;
        }

        public IEnumerable<ResumenMovimientoDto> Ejecutar()
        {
            try
            {
                var movimientos = _repositorioMovimientoStock.GetAll();

                var resumen = movimientos
                    .GroupBy(m => new { m.FechaMovimiento.Year, m.TipoMovimientoId })
                    .Select(g => new ResumenMovimientoDto
                    {
                        Anio = g.Key.Year,
                        TipoMovimientoId = g.Key.TipoMovimientoId,
                        CantidadTotal = g.Sum(m => Math.Abs(m.Cantidad))
                    })
                    .ToList();

                return resumen;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el resumen de movimientos", ex);
            }
        }
    }
}
