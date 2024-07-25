using LogicaAplicacion.DTOs.TipoMovimientoStockDTOs;
using LogicaAplicacion.InterfacesCasosUsos.TipoMovimientoStock;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoMovimientoStockController : ControllerBase
    {
        private readonly IAltaTipoMovimientoStock _AltaTipoMovimientoStock;
        private readonly IBajaTipoMovimientoStock _BajaTipoMovimientoStock;
        private readonly IEditarTipoMovimientoStock _EditarTipoMovimientoStock;
        private readonly IGetAllTipoMovimientoStock _GetAllTipoMovimientoStock;
        private readonly IGetByIdTipoMovimientoStock _GetByIdTipoMovimientoStock;
        public TipoMovimientoStockController(IAltaTipoMovimientoStock altaTipoMovimientoStock, IBajaTipoMovimientoStock bajaTipoMovimientoStock, IEditarTipoMovimientoStock editarTipoMovimientoStock, IGetAllTipoMovimientoStock getAllTipoMovimientoStock, IGetByIdTipoMovimientoStock getByIdTipoMovmientoStock)
        {
            _AltaTipoMovimientoStock = altaTipoMovimientoStock;
            _BajaTipoMovimientoStock = bajaTipoMovimientoStock;
            _EditarTipoMovimientoStock= editarTipoMovimientoStock;
            _GetAllTipoMovimientoStock= getAllTipoMovimientoStock;
            _GetByIdTipoMovimientoStock= getByIdTipoMovmientoStock;
        }

        /// <summary>
        /// Obtiene todos los tipos de movimientos de stock.
        /// </summary>
        /// <returns>
        ///     Una lista de objetos TipoMovimientoStockDto. Retorna 200 OK en caso de éxito, 
        ///     204 No Content si no hay movimientos, o 500 Internal Server Error si ocurre un error.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<TipoMovimientoStockDto>> Get()
        {
            try
            {
                IEnumerable<TipoMovimientoStockDto> tipoMovimientos = _GetAllTipoMovimientoStock.Ejecutar();
                if (tipoMovimientos.Any())
                {
                    return Ok(tipoMovimientos);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Obtiene un tipo de movimiento de stock por su ID.
        /// </summary>
        /// <param name="id">El ID del tipo de movimiento de stock.</param>
        /// <returns>
        ///     Un objeto TipoMovimientoStockDto. Retorna 200 OK en caso de éxito, 
        ///     204 No Content si no se encuentra el tipo de movimiento, o 500 Internal Server Error si ocurre un error.
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TipoMovimientoStockDto> Get(int id)
        {
            try
            {
                var tipoMovimientoDto = _GetByIdTipoMovimientoStock.Ejecutar(id);
                if (tipoMovimientoDto == null)
                {
                    return NoContent();
                }
                return Ok(tipoMovimientoDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Crea un nuevo tipo de movimiento de stock.
        /// </summary>
        /// <param name="tipoMovimientoDto">El objeto TipoMovimientoStockDto que contiene los detalles del tipo de movimiento a crear.</param>
        /// <returns>
        ///     Un objeto IActionResult que contiene el resultado de la operación. 
        ///     Retorna 201 Created en caso de éxito, 400 Bad Request si el tipo de movimiento no es válido
        ///     o 500 Internal Server Error si ocurre un error.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult AltaTipoMovimientoStock([FromBody] TipoMovimientoStockDto tipoMovimientoDto)
        {
            try
            {
                if (tipoMovimientoDto == null)
                {
                    return BadRequest("El Tipo de Movimiento no es válido.");
                }
                _AltaTipoMovimientoStock.Ejecutar(tipoMovimientoDto);
                return CreatedAtAction(nameof(Get), new { id = tipoMovimientoDto.Id }, tipoMovimientoDto);
            }
            catch (TipoMovimientoNoValidoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Actualiza un tipo de movimiento de stock existente.
        /// </summary>
        /// <param name="id">El ID del tipo de movimiento de stock a actualizar.</param>
        /// <param name="tipoMovimientoDto">El objeto TipoMovimientoStockDto que contiene los detalles del tipo de movimiento a actualizar.</param>
        /// <returns>
        ///     Un objeto IActionResult que contiene el resultado de la operación. 
        ///     Retorna 200 OK en caso de éxito, 400 Bad Request si el tipo de movimiento no es válido, 
        ///     o 500 Internal Server Error si ocurre un error.
        /// </returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Put(int id, [FromBody] TipoMovimientoStockDto tipoMovimientoDto)
        {
            try
            {
                if (tipoMovimientoDto == null)
                {
                    return BadRequest("El Tipo Movimiento que se recibio no es válido.");
                }
                _EditarTipoMovimientoStock.Ejecutar(tipoMovimientoDto, id);
                return Ok();
            }
            catch (TipoMovimientoNoValidoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Elimina un tipo de movimiento de stock por su ID.
        /// </summary>
        /// <param name="id">El ID del tipo de movimiento de stock a eliminar.</param>
        /// <returns>
        ///     Un objeto IActionResult que contiene el resultado de la operación. 
        ///     Retorna 200 OK en caso de éxito, 400 Bad Request si el ID no es válido, 
        ///     o 500 Internal Server Error si ocurre un error.
        /// </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Id no válido");
                }
                _BajaTipoMovimientoStock.Ejecutar(id);
                return Ok();
            }
            catch (TipoMovimientoNoValidoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
