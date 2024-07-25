using AccesoDatos.RepositorioEF;
using LogicaAplicacion.DTOs.PedidoDTOs;
using LogicaAplicacion.ImplementacionCasosUsos.Pedidos;
using LogicaAplicacion.InterfacesCasosUsos.Pedidos;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        IRepositorioPedidos _repositorioPedidos;
        IObtenerPedidosAnulados _obtenerPedidosAnulados;

        public PedidoController(IRepositorioPedidos repositorioPedidos, IObtenerPedidosAnulados obtenerPedidosAnulados)
        {
            _obtenerPedidosAnulados = obtenerPedidosAnulados;
            _repositorioPedidos = repositorioPedidos;
        }

        /// <summary>
        /// Obtiene una lista de pedidos anulados.
        /// </summary>
        /// <returns>
        ///     Una lista de pedidos anulados. 
        ///     Retorna 200 OK en caso de éxito, 204 No Content si no se encuentran pedidos anulados, 
        ///     o 400 Bad Request si ocurre un error.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<PedidoParaListaDto> GetPedidosAnulados()
        {
            try
            {
                IEnumerable<PedidoParaListaDto> pedidos = _obtenerPedidosAnulados.Ejecutar();
                if (pedidos.Any())
                {
                    return Ok(pedidos);
                }
                return NoContent();
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
