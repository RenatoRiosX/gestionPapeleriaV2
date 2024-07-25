using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.RepositorioEF
{
    public class RepositorioTipoMovimientoEF : IRepositorioTipoMovimientoStock
    {
        private Context _db;

        public RepositorioTipoMovimientoEF()
        {
            _db = new Context();
        }
        public void Add(TipoMovimientoStock unObjeto)
        {
            try
            {
                if (unObjeto == null)
                {
                    throw new TipoMovimientoNoValidoException("TipoMovimiento no valido");
                }
                unObjeto.EsValido();
                _db.TipoMovimientosStock.Add(unObjeto);
                _db.SaveChanges();
            }
            catch (TipoMovimientoNoValidoException ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TipoMovimientoStock> GetAll()
        {
            return _db.TipoMovimientosStock.ToList();
        }

        public TipoMovimientoStock GetById(int id)
        {
            return _db.TipoMovimientosStock.FirstOrDefault(x => x.Id == id);
        }

        public void Remove(int id)
        {
            try
            {
               
                if (id != 0)
                {
                    bool hayMovimientoConTipoRecibido = _db.MovimientosStock.Any(m => m.TipoMovimientoId == id);

                    if (hayMovimientoConTipoRecibido)
                    {
                        throw new TipoMovimientoNoValidoException("El Tipo de Movimiento esta siendo utilizado en un Movimiento de Stock.");
                    }

                    var tipo = _db.TipoMovimientosStock.FirstOrDefault(x => x.Id == id);
                    _db.TipoMovimientosStock.Remove(tipo);
                    _db.SaveChanges();
                }
               
                
            }
            catch (TipoMovimientoNoValidoException ex)
            {
                throw ex;
            }
          
        }

        public void Remove(TipoMovimientoStock unObjeto)
        {
            if (unObjeto == null)
                throw new TipoMovimientoNoValidoException("El tipo movimiento es nulo");

            try
            {
                _db.TipoMovimientosStock.Remove(unObjeto);
                _db.SaveChanges();
            }
            catch (TipoMovimientoNoValidoException ex)
            {
                throw ex;
            }
        }
        
        public void Update(TipoMovimientoStock unObjeto)
        {
            try
            {
                if (unObjeto == null)
                {
                    throw new TipoMovimientoNoValidoException("El Tipo de Movimiento no puede ser null.");
                }
                unObjeto.EsValido();

                bool hayMovimientoConTipoRecibido = _db.MovimientosStock.Any(m => m.TipoMovimientoId == unObjeto.Id);

                if (hayMovimientoConTipoRecibido)
                {
                    throw new TipoMovimientoNoValidoException("El Tipo de Movimiento esta siendo utilizado en un Movimiento de Stock.");
                }

                var tipoMovmimientoExistente = _db.TipoMovimientosStock.Find(unObjeto.Id);
                if (tipoMovmimientoExistente == null)
                {
                    throw new Exception("Tipo de movimiento no encontrado.");
                }

                _db.Entry(tipoMovmimientoExistente).CurrentValues.SetValues(unObjeto);
                _db.SaveChanges();
            }
            catch (TipoMovimientoNoValidoException ex)
            {
                throw ex;
            }
        }
            
        
    }
}
