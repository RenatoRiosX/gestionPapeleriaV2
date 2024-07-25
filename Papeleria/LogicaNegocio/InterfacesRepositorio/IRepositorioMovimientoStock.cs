using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioMovimientoStock
    {
        void Add(MovimientoStock ObjetoMS);

        MovimientoStock GetById(int id);
        IEnumerable<MovimientoStock> GetAll();
        IEnumerable<MovimientoStock> GetPorArticuloYTipo(int idArticulo, int idTipoMovimiento, int numPagina, int cantRegistros);
        IEnumerable<Articulo> GetArticulosConMovimientosEntreFechas(DateTime fecha1, DateTime fecha2, int numPagina, int cantidadRegistros);



    }
}
