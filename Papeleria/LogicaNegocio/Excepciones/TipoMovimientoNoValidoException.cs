using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Excepciones
{
    public class TipoMovimientoNoValidoException : Exception
    {
        public TipoMovimientoNoValidoException() { }
        public TipoMovimientoNoValidoException(string mensaje) : base(mensaje) { }
        public TipoMovimientoNoValidoException(string mensaje, Exception ex) : base(mensaje, ex) { }
    }
}
