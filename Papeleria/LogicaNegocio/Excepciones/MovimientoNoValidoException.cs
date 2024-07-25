using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Excepciones
{
    public class MovimientoNoValidoException:Exception
    {
        public MovimientoNoValidoException() { }
        public MovimientoNoValidoException(string mensaje) : base(mensaje) { }
        public MovimientoNoValidoException(string mensaje, Exception ex) : base(mensaje, ex) { }
    }
}
