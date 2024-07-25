using System.Data;
using LogicaAplicacion.DTOs.UsuarioDTOs;
using LogicaAplicacion.InterfacesCasosUsos.Usuarios;
using LogicaNegocio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsuarioController : ControllerBase
	{
		private ILogin _login;

		public UsuarioController(ILogin login)
		{
			_login = login;
		}


        /// <summary>
        /// Autentica a un usuario y genera un token JWT.
        /// </summary>
        /// <param name="usr">El objeto UsuarioLoginDto que contiene el email y la contraseña del usuario.</param>
        /// <returns>
        ///     Un objeto IActionResult que contiene el resultado de la operación. 
        ///     Retorna 200 OK con el token JWT y detalles del usuario en caso de éxito, 
        ///     400 Bad Request si no se encuentra un usuario con los datos proporcionados, 
        ///     401 Unauthorized si las credenciales son incorrectas, 
        ///     o 500 Internal Server Error si ocurre un error inesperado.
        /// </returns>

        [AllowAnonymous]
		[HttpPost("login")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login([FromBody] UsuarioLoginDto usr)
        {
            try
            {
                if (usr == null)
                {
                    return BadRequest("Lo campos de email y contraseña con obligatorios.");
                }

                var usuario = _login.Ejecutar(usr.Email, usr.Contrasenia);
                if (usuario == null)
                {
                    return BadRequest("No se encontró ningún usuario con los datos recibidos.");
                }

                string token = ManejadorJwt.ManejadorJwt.GenerarToken(usr.Email, usuario.Rol);
                return Ok(new { Token = token, Rol = usuario.Rol, Email = usuario.Email, EncargadoId = usuario.Id });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Error = "Credenciales incorrectas. " + ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "Ocurrió un error inesperado. " + ex.Message });
            }
        }

    }
}
