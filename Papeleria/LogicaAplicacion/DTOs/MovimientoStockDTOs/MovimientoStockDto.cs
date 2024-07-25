using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.DTOs.MovimientoStockDTOs
{
    public class MovimientoStockDto
    {
        public int Id { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public int ArticuloId { get; set; }
        public int TipoMovimientoId { get; set; } 
        public int Cantidad { get; set; }
        public int EncargadoId { get; set; }
        public string EncargadoEmail { get; set; } 


	}
}
