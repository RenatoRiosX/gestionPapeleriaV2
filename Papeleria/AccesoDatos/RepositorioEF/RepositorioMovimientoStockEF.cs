using Azure;
using LogicaAplicacion.DTOs.ArticuloDTOs;
using LogicaAplicacion.DTOs.MovimientoStockDTOs;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.RepositorioEF
{
    public class RepositorioMovimientoStockEF : IRepositorioMovimientoStock
    {
        private Context _db;

        public RepositorioMovimientoStockEF()
        {
            _db = new Context();
        }
        public void Add(MovimientoStock movimientoStock)
        {
            try
            {
                if (movimientoStock == null)
                {
                    throw new MovimientoNoValidoException("MovimientoStock no válido");
                }

                movimientoStock.EsValido();

                if (movimientoStock.TipoMovimientoStock != null)
                {
                    _db.Entry(movimientoStock.TipoMovimientoStock).State = EntityState.Unchanged;
                }

                if (movimientoStock.Articulo != null)
                {
                    _db.Entry(movimientoStock.Articulo).State = EntityState.Unchanged;
                }

                if (movimientoStock.Encargado != null)
                {
                    _db.Entry(movimientoStock.Encargado).State = EntityState.Unchanged;
                }

                // Validar que Articulo y TipoMovimiento existan
                var articulo = _db.Articulos.Find(movimientoStock.ArticuloId);
                var tipoMovimiento = _db.TipoMovimientosStock.Find(movimientoStock.TipoMovimientoId);
                var usuario = _db.Usuarios.Find(movimientoStock.EncargadoId);

                if (articulo == null)
                {
                    throw new MovimientoNoValidoException("Articulo no válido");
                }

                if (tipoMovimiento == null)
                {
                    throw new MovimientoNoValidoException("Tipo de movimiento no válido");
                }

                if (usuario == null || usuario is not Encargado)
                {
                    throw new MovimientoNoValidoException("Encargado no válido.");
                }

				if (!tipoMovimiento.EsAumentoCantidad)
                {
                    movimientoStock.Cantidad = movimientoStock.Cantidad * -1;
                }
                
                _db.MovimientosStock.Add(movimientoStock);
                _db.SaveChanges();
            }
            catch (MovimientoNoValidoException ex)
            {
                throw ex;
            }
        }


        public IEnumerable<MovimientoStock> GetAll()
        {
            return _db.MovimientosStock.ToList();
        }

        public MovimientoStock GetById(int id)
        {
            return _db.MovimientosStock.FirstOrDefault(x => x.Id == id);
        }

        //Dados un id de artículo y un tipo de movimiento, todos los movimientos de ese tipo
        //realizados sobre ese artículo.Deberán estar ordenados descendentemente por fecha y en
        //forma ascendente por la cantidad de unidades involucradas en el movimiento Se deberá
        //mostrar todos los datos del movimiento, incluyendo todos los datos de su tipo.
        public IEnumerable<MovimientoStock> GetPorArticuloYTipo(int idArticulo, int idTipoMovimiento, int numPagina, int cantidadRegistros)
        {
            try
            {
                int numRegistrosAnteriores = cantidadRegistros * (numPagina - 1);
                var resultado = _db.MovimientosStock
                    .Include(x => x.TipoMovimientoStock)
                    .Where(x => x.ArticuloId == idArticulo && x.TipoMovimientoId == idTipoMovimiento)
                    .OrderByDescending(x => x.FechaMovimiento)
                    .ThenBy(x => x.Cantidad)
                    .Skip(numRegistrosAnteriores)
                    .Take(cantidadRegistros)
                    .ToList();

                if (resultado == null || !resultado.Any())
                {
                    throw new NoHayValoresParaLaPaginaException("No hay valores para la página seleccionada.");
                }

                return resultado;
            }
            catch(NoHayValoresParaLaPaginaException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                return new List<MovimientoStock>();
            }
        }
        //Dado un rango de fechas, seleccionar los artículos que hayan tenido movimientos (al
        //menos uno) entre esas fechas inclusive.
        public IEnumerable<Articulo> GetArticulosConMovimientosEntreFechas(DateTime fecha1, DateTime fecha2, int numPagina, int cantidadRegistros)
        {
            try
            {
                int numRegistrosAnteriores = cantidadRegistros * (numPagina - 1);
                var resultado = (from m in _db.MovimientosStock
                        join a in _db.Articulos on m.ArticuloId equals a.Id
                        where m.FechaMovimiento >= fecha1 && m.FechaMovimiento <= fecha2
                        select a)
                    .Distinct()
                    .Skip(numRegistrosAnteriores)
                    .Take(cantidadRegistros)
                    .ToList();
                if (resultado == null || !resultado.Any())
                {
                    throw new NoHayValoresParaLaPaginaException("No hay valores para la página seleccionada.");
                }

                return resultado;
            }
            catch (NoHayValoresParaLaPaginaException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
