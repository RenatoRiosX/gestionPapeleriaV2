using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaAplicacion.InterfacesCasosUsos.Configuraciones;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.ImplementacionCasosUsos.Configuraciones
{
	public class GetValorPorNombre : IGetValorPorNombre
	{
		private readonly IRepositorioConfiguracion _repositorioConfiguracion;

		public GetValorPorNombre(IRepositorioConfiguracion repositorioConfiguracion)
		{
			_repositorioConfiguracion = repositorioConfiguracion;
		}
		public double Ejecutar(string nombre)
		{
			return _repositorioConfiguracion.GetValueByName(nombre);
		}
	}

}
