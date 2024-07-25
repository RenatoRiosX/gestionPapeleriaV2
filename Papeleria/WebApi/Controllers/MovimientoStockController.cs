using LogicaAplicacion.DTOs.ArticuloDTOs;
using LogicaAplicacion.DTOs.MovimientoStockDTOs;
using LogicaAplicacion.DTOs.TipoMovimientoStockDTOs;
using LogicaAplicacion.ImplementacionCasosUsos.Configuraciones;
using LogicaAplicacion.ImplementacionCasosUsos.MovimientosStock;
using LogicaAplicacion.InterfacesCasosUsos.Articulos;
using LogicaAplicacion.InterfacesCasosUsos.Configuraciones;
using LogicaAplicacion.InterfacesCasosUsos.MovimientoStock;
using LogicaAplicacion.InterfacesCasosUsos.TipoMovimientoStock;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Utils;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Encargado")]
    public class MovimientoStockController : ControllerBase
    {
        private readonly IAltaMovimientoStock _altaMovimientoStock;
        private readonly IGetMovimientoStockTipoArticulo _getMovimientoStockTipoArticulo;
        private readonly IGetAllArticulos _getAllArticulos;
        private readonly IGetAllTipoMovimientoStock _getAllTiposMovimientoStock;
        private readonly IGetValorPorNombre _getValorPorNombre;
        private readonly IArticulosConMovimientosEntreFechas _articulosConMovimientosEntreFechas;
        private readonly IObtenerResumenMovimientos _obtenerResumenMovimientos;

        public MovimientoStockController(IAltaMovimientoStock altaMovimientoStock, IGetMovimientoStockTipoArticulo getmovimientoStockTipoArticulo,
										IGetAllArticulos getAllArticulos, IGetAllTipoMovimientoStock gelAllTipoMovimientoStock, IGetValorPorNombre getValorPorNombre, IArticulosConMovimientosEntreFechas articulosConMovimientosEntreFechas, IObtenerResumenMovimientos resumenMovimientos)
        {
            _getAllArticulos = getAllArticulos;
            _getAllTiposMovimientoStock = gelAllTipoMovimientoStock;
            _altaMovimientoStock = altaMovimientoStock;
            _getMovimientoStockTipoArticulo = getmovimientoStockTipoArticulo;
            _getValorPorNombre = getValorPorNombre;
            _articulosConMovimientosEntreFechas = articulosConMovimientosEntreFechas;
            _obtenerResumenMovimientos = resumenMovimientos;
        }

        /// <summary>
        /// Obtiene todos los artículos y tipos de movimiento de stock.
        /// </summary>
        /// <returns>
        ///     Un objeto IActionResult que contiene una lista de artículos y tipos de movimiento de stock. 
        ///     Retorna 200 OK en caso de éxito, 204 No Content si no se encuentran artículos ni tipos de movimiento de stock, 
        ///     o 400 Bad Request si ocurre un error relacionado con movimientos no válidos.
        /// </returns>
        [HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> Get()
		{
			try
			{
				IEnumerable<ArticuloDto> articulos = _getAllArticulos.Ejecutar();
				IEnumerable<TipoMovimientoStockDto> tiposDeMovimientoStock = _getAllTiposMovimientoStock.Ejecutar();
				if (articulos == null && tiposDeMovimientoStock == null)
				{
					return NoContent();
				}

				return Ok(new { tiposDeMovimientoStock = tiposDeMovimientoStock, Articulos = articulos });
			}
			catch (MovimientoNoValidoException ex)
			{
				return BadRequest(ex.Message);
			}
		}

        /// <summary>
        /// Crea un nuevo movimiento de stock.
        /// </summary>
        /// <param name="movimientoDto"> El objeto MovimientoStockDto que contiene los detalles del movimiento a crear.</param>
        /// <returns>
        ///     Un objeto IActionResult que contiene el resultado de la operación. 
        ///     Retorna 201 Created en caso de éxito, 400 Bad Request si el movimiento no es válido, 
        ///     401 Unauthorized si no está autorizado, o 500 Internal Server Error si ocurre un error.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Post([FromBody] MovimientoStockDto movimientoDto)
        {
			try
			{
				if (movimientoDto == null)
				{
					return BadRequest(new { Message = "MovimientoStockDto no es válido." });
				}

				if (_getValorPorNombre.Ejecutar("TopeCantidadMovimiento") < movimientoDto.Cantidad)
				{
					throw new MovimientoNoValidoException("La cantidad del movimiento supera el tope permitido.");
				}

				_altaMovimientoStock.Ejecutar(movimientoDto);
				return CreatedAtAction(nameof(Post), new { id = movimientoDto.Id }, movimientoDto);
			}
			catch (MovimientoNoValidoException ex)
			{

				return BadRequest(new { Message = ex.Message });
			}
			catch (UnauthorizedAccessException ex)
			{
				return Unauthorized(new { Message = ex.Message });
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Ocurrió un error al procesar su solicitud.", Details = ex.Message });
			}
		}


        /// <summary>
        /// Obtiene una lista de movimientos de stock para un artículo y tipo de movimiento especificados, paginados.
        /// </summary>
        /// <param name="idArticulo">El ID del artículo.</param>
        /// <param name="tipoMovimientoId">El ID del tipo de movimiento.</param>
        /// <param name="pagina">El número de página para la paginación.</param>
        /// <returns>
        ///     Una lista de objetos MovimientoYTipoDto. Retorna 200 OK en caso de éxito, 
        ///     204 No Content si no hay movimientos, 400 Bad Request si los parámetros no son válidos, 
        ///     401 Unauthorized si no está autorizado o 500 Internal Server Error si se produce un error en el servidor.
        /// </returns>
        [HttpGet("{idArticulo}/{tipoMovimientoId}/pagina/{pagina}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<MovimientoYTipoDto>> GetMovimientosTipoArticulo(int idArticulo, int tipoMovimientoId, int pagina)
        {
            try
            {
                if (idArticulo < 1 || tipoMovimientoId < 1)
                {
                    return BadRequest("El id del articulo y del tipo de movimiento no puede ser menor a 1.");
                }

                int itemsPagina = (int)_getValorPorNombre.Ejecutar("CantidadItemsPorPagina");
                IEnumerable<MovimientoYTipoDto> movimientosTipoArticulo =
                    _getMovimientoStockTipoArticulo.Ejecutar(idArticulo, tipoMovimientoId, pagina, itemsPagina);

                if (movimientosTipoArticulo.Any())
                {
                    return Ok(movimientosTipoArticulo);
                }
                
                return NoContent();
                

            }
            catch (NoHayValoresParaLaPaginaException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Se produjo un error. "+ ex.Message });
            }
        }

        /// <summary>
        /// Obtiene una lista de artículos con movimientos de stock entre dos fechas especificadas, paginados.
        /// </summary>
        /// <param name="fecha1">La fecha de inicio del rango.</param>
        /// <param name="fecha2">La fecha de fin del rango.</param>
        /// <param name="pagina">El número de página para la paginación.</param>
        /// <returns>
        ///     Una lista de objetos ArticuloDto. Retorna 200 OK en caso de éxito, 
        ///     204 No Content si no hay artículos, 400 Bad Request si las fechas no son válidas, 
        ///     401 Unauthorized si no está autorizado o 500 Internal Server Error si se produce un error en el servidor.
        /// </returns>
        [HttpGet("articulosConMovimientosEnPeriodo/{fecha1}/{fecha2}/pagina/{pagina}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<IEnumerable<ArticuloDto>> GetArticulosConMovimientosEntreFechas(DateTime fecha1, DateTime fecha2, int pagina)
        {
            try
            {

                if (fecha1 == default || fecha2 == default)
                {
                    return BadRequest("Fechas inválidas.");
                }

                int itemsPagina = (int)_getValorPorNombre.Ejecutar("CantidadItemsPorPagina");
                IEnumerable<ArticuloDto> articulos = _articulosConMovimientosEntreFechas.Ejecutar(fecha1, fecha2, pagina, itemsPagina);

                if (articulos.Any())
                {
                    return Ok(articulos);
                }
                return NoContent();
            }
            catch (NoHayValoresParaLaPaginaException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Se produjo un error al obtener los movimientos de stock.", details = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene un resumen de todos los movimientos de stock.
        /// </summary>
        /// <returns>
        ///     Una lista de objetos ResumenMovimientoDto. Retorna 200 OK en caso de éxito, 
        ///     204 No Content si no hay movimientos, 400 Bad Request si ocurre un error
        ///     o 401 Unauthorized si no está autorizado.
        /// </returns>
        [HttpGet("resumen")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<IEnumerable<ResumenMovimientoDto>> GetResumenMovimientos()
        {
            try
            {
                var resumenMovimientos = _obtenerResumenMovimientos.Ejecutar();
                if (resumenMovimientos.Any())
                {
                    return Ok(resumenMovimientos);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
