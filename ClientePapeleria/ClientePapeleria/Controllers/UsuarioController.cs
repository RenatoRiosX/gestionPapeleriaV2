using ClientePapeleria.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Exception = System.Exception;

namespace ClientePapeleria.Controllers
{
	public class UsuarioController : Controller
	{
		private HttpClient _cliente;
		private string _baseURL = "https://localhost:7012/api/Usuario/";
		private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		};

		public UsuarioController(HttpClient client)
		{
			_cliente = client;
			_cliente.BaseAddress = new Uri(_baseURL);
		}


		public ActionResult Login()
		{
			return View();
		}


		[HttpPost]
		public async Task<ActionResult> Login(DatosLogin datosLog)
		{
			try
			{
				var json = JsonConvert.SerializeObject(datosLog);
				var contenido = new StringContent(json, Encoding.UTF8, "application/json");
				var respuesta = await _cliente.PostAsync("login", contenido);

                if (respuesta.IsSuccessStatusCode)
                {
                    var datosRespuesta = await respuesta.Content.ReadAsStringAsync();
                    var datosLogin = JsonConvert.DeserializeObject<LoginUsuario>(datosRespuesta);

                    // Creación de variables de sesión
                    HttpContext.Session.SetString("logueadoRol", datosLogin.Rol);
                    HttpContext.Session.SetString("logueadoEmail", datosLogin.Email);
                    HttpContext.Session.SetInt32("logueadoId", datosLogin.EncargadoId);
                    HttpContext.Session.SetString("logueadoToken", datosLogin.Token);

                    return RedirectToAction("RegistrarMovimientoStock", "MovimientoStock");
                }
                else
                {
                    string errorContent = await respuesta.Content.ReadAsStringAsync();
                    if (respuesta.StatusCode == HttpStatusCode.BadRequest)
                    {
                        TempData["error"] = $"Solicitud inválida: {errorContent}";
                    }
                    else if (respuesta.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        TempData["error"] = $"No autorizado: {errorContent}";
                    }
                    else if (respuesta.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        TempData["error"] = $"Error en el servidor: {errorContent}";
                    }
                    else
                    {
                        TempData["error"] = "Se produjo un error inesperado.";
                    }
                    return View();
                }
            }
			catch (Exception ex)
			{
				TempData["error"] = ex.Message;
				return View();
			}
		}


		public ActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Login");
		}
	}


}
