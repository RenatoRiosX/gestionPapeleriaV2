using ClientePapeleria.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace ClientePapeleria.Controllers
{
    public class MovimientoStockController : Controller
    {
        private HttpClient _cliente;
        private string baseURL;

        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
	        PropertyNameCaseInsensitive = true
        };

		public MovimientoStockController(HttpClient cliente)
        {
            _cliente = cliente;
            baseURL = "https://localhost:7012/api/MovimientoStock";
        }


        public ActionResult RegistrarMovimientoStock()
        {
	        AltaMovimientoStock datosMostrar = new AltaMovimientoStock();
	        try
	        {
                string token = HttpContext.Session.GetString("logueadoToken");

                if (HttpContext.Session.GetString("logueadoRol") != "Encargado" || string.IsNullOrEmpty(token))
                {
                    TempData["error"] = "No tiene permisos para registrar movimientos de stock.";
                    return RedirectToAction("Login", "Usuario");
                }

                _cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Bearer",
                    token
                );

                Uri uri = new Uri(baseURL);
		        HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);
		        Task<HttpResponseMessage> respuesta = _cliente.SendAsync(solicitud);

		        respuesta.Wait();
		        if (respuesta.Result.IsSuccessStatusCode)
		        {
			        var datosRespuesta = respuesta.Result.Content.ReadAsStringAsync().Result;
			        ArticulosYTiposDeMovimiento articulosYTipos =
				        JsonConvert.DeserializeObject<ArticulosYTiposDeMovimiento>(datosRespuesta);
			        datosMostrar.TiposDeMovimientos = articulosYTipos.TiposDeMovimientoStock;
					datosMostrar.Articulos = articulosYTipos.Articulos;
			        
		        }

		        return View(datosMostrar);
	        }
	        catch (BadHttpRequestException e)
	        {
		        TempData["error"] = e.Message;
		        return View(datosMostrar);
	        }
			catch (Exception e)
	        {
		        TempData["error"] = e.Message;
		        return View(datosMostrar);
	        }
	        
        }

        [HttpPost]
        public async Task<ActionResult> RegistrarMovimientoStock(AltaMovimientoStock altaMovimientoStock)
        {
			try
			{
                string token = HttpContext.Session.GetString("logueadoToken");

                if (HttpContext.Session.GetString("logueadoRol") != "Encargado" || string.IsNullOrEmpty(token))
				{
					TempData["error"] = "No tiene permisos para registrar movimientos de stock.";
					return RedirectToAction("Login", "Usuario");
				}
                
                _cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Bearer",
                    token
                );

                Uri uri = new Uri(baseURL);
				HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Post, uri);

				MovimientoStock movimientoStock = new MovimientoStock
				{
					Cantidad = altaMovimientoStock.Cantidad,
					ArticuloId = altaMovimientoStock.IdArticulo,
					TipoMovimientoId = altaMovimientoStock.IdTipoMovimiento,
					FechaMovimiento = DateTime.Now,
					EncargadoEmail = HttpContext.Session.GetString("logueadoEmail"),
					EncargadoId = HttpContext.Session.GetInt32("logueadoId").Value
				};

				var json = JsonConvert.SerializeObject(movimientoStock);
				var contenido = new StringContent(json, Encoding.UTF8, "application/json");
				var respuesta = await _cliente.PostAsync(uri, contenido);

				if (respuesta.IsSuccessStatusCode)
				{
					TempData["mensaje"] = "Movimiento de stock registrado con éxito.";
				}
				else
				{
					// Leer el contenido de la respuesta para obtener detalles del error
					var errorContent = await respuesta.Content.ReadAsStringAsync();
					var errorDetails = JsonConvert.DeserializeObject<ErrorDetails>(errorContent);

					switch (respuesta.StatusCode)
					{
						case HttpStatusCode.BadRequest:
							TempData["error"] = "Solicitud incorrecta: " + errorDetails.Message;
							break;
						case HttpStatusCode.Unauthorized:
							TempData["error"] = "No autorizado: " + errorDetails.Message;
							break;
						case HttpStatusCode.InternalServerError:
							TempData["error"] = "Error del servidor: " + errorDetails.Message;
							break;
						default:
							TempData["error"] = "Error al registrar el movimiento de stock: " + errorDetails.Message;
							break;
					}
				}

				return RedirectToAction("RegistrarMovimientoStock");
			}
			catch (Exception ex)
			{
				TempData["error"] = "Error al enviar el movimiento de stock: " + ex.Message;
				return RedirectToAction("RegistrarMovimientoStock");
			}
		}

        public IActionResult MovimientoDeArticulo()
        {
            if (HttpContext.Session.GetString("logueadoRol") != "Encargado")
            {
                TempData["error"] = "No tiene permisos para registrar movimientos de stock.";
                return RedirectToAction("Login", "Usuario");
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<MovimientosArticuloTipo>>>  MovimientoDeArticulo(int idArticulo, int idTipoMovimiento,int pagina)
        {
            try
            {
                string token = HttpContext.Session.GetString("logueadoToken");

                if (HttpContext.Session.GetString("logueadoRol") != "Encargado" || string.IsNullOrEmpty(token))
                {
                    TempData["error"] = "No tiene permisos para registrar movimientos de stock.";
                    return RedirectToAction("Login", "Usuario");
                }

                _cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Bearer",
                    token
                );

                Uri uri = new Uri($"{baseURL}/{idArticulo}/{idTipoMovimiento}/pagina/{pagina}");
                HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);
                var respuesta = await _cliente.SendAsync(solicitud);

				var movimientos = new List<MovimientosArticuloTipo>();
                if (respuesta.IsSuccessStatusCode)
                {
                    if (respuesta.StatusCode == HttpStatusCode.NoContent)
                    {
                        TempData["Mensaje"] = "No se encontraron movimientos para los filtros seleccionados.";
                    }
                    else
                    {
                        var datosRespuesta = await respuesta.Content.ReadAsStringAsync();
                        movimientos = JsonConvert.DeserializeObject<List<MovimientosArticuloTipo>>(datosRespuesta);
                    }
                }
                else
                {
                    if (respuesta.StatusCode == HttpStatusCode.BadRequest)
                    {
                        var errorContent = await respuesta.Content.ReadAsStringAsync();
                        TempData["Error"] = $"Solicitud inválida: {errorContent}";
                    }
                    else if (respuesta.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        TempData["Error"] = "No está autorizado para realizar esta acción.";
                    }
                    else if (respuesta.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        var errorContent = await respuesta.Content.ReadAsStringAsync();
                        TempData["Error"] = $"Error en el servidor: {errorContent}";
                    }
                    else
                    {
                        TempData["Error"] = "Se produjo un error con la solicitud. Compruebe los parámetros.";
                    }
                }

                return View("MovimientoDeArticulo", movimientos);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al obtener los movimientos de stock: " + ex.Message;
                return View("MovimientoDeArticulo", new List<MovimientosArticuloTipo>());
            }
        }

        public ActionResult MovimientoDeArticulosEnPeriodo()
        {
            if (HttpContext.Session.GetString("logueadoRol") != "Encargado")
            {
                TempData["error"] = "No tiene permisos para registrar movimientos de stock.";
                return RedirectToAction("Login", "Usuario");
            }

            var viewModel = new MovimientoDeArticulos
            {
                Pagina = 1,
                Articulos = new List<Articulo>()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<ArticulosEntreFechas>>> MovimientoDeArticulosEnPeriodo(MovimientoDeArticulos movimientoDeArticulos)
		{

            try
            {
                string token = HttpContext.Session.GetString("logueadoToken");

                if (HttpContext.Session.GetString("logueadoRol") != "Encargado" || string.IsNullOrEmpty(token))
                {
                    TempData["error"] = "No tiene permisos para registrar movimientos de stock.";
                    return RedirectToAction("Login", "Usuario");
                }

                _cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Bearer",
                    token
                );

                if (!movimientoDeArticulos.FechaInicio.HasValue || !movimientoDeArticulos.FechaFin.HasValue)
                {
                    movimientoDeArticulos.Articulos = new List<Articulo>();
                    TempData["Error"] = "Por favor, selecciona ambas fechas.";
                    return View(movimientoDeArticulos);
                }

                // Formatear las fechas para que sean compatibles con la URL
                string fecha1String = Uri.EscapeDataString(movimientoDeArticulos.FechaInicio.Value.ToString("yyyy-MM-ddTHH:mm:ss"));
                string fecha2String = Uri.EscapeDataString(movimientoDeArticulos.FechaFin.Value.ToString("yyyy-MM-ddTHH:mm:ss"));

                Uri uri = new Uri($"{baseURL}/articulosConMovimientosEnPeriodo/{fecha1String}/{fecha2String}/pagina/{movimientoDeArticulos.Pagina}");

                HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);
                var respuesta = await _cliente.SendAsync(solicitud);

                if (respuesta.IsSuccessStatusCode)
                {

                    if (respuesta.StatusCode == HttpStatusCode.NoContent)
                    {
                        TempData["Mensaje"] = "No hay movimiento de artículos para el periodo seleccionado.";
                        movimientoDeArticulos.Articulos = new List<Articulo>();
                    }
                    else
                    {
                        var datosRespuesta = await respuesta.Content.ReadAsStringAsync();
                        var articulos = JsonConvert.DeserializeObject<IEnumerable<Articulo>>(datosRespuesta);
                        movimientoDeArticulos.Articulos = articulos;
                    }
                }
                else
                {
                    movimientoDeArticulos.Articulos = new List<Articulo>();

                    if (respuesta.StatusCode == HttpStatusCode.BadRequest)
                    {
                        var errorContent = await respuesta.Content.ReadAsStringAsync();
                        TempData["Error"] = $"Solicitud inválida: {errorContent}";
                    }
                    else if (respuesta.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        TempData["Error"] = "No está autorizado para realizar esta acción.";
                    }
                    else if (respuesta.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        var errorContent = await respuesta.Content.ReadAsStringAsync();
                        TempData["Error"] = $"Error en el servidor: {errorContent}";
                    }
                    else
                    {
                        TempData["Error"] = "Se produjo un error con la solicitud. Compruebe las fechas y el número de página.";
                    }
                }
            }
            catch (Exception ex)
            {
                movimientoDeArticulos.Articulos = new List<Articulo>();
                TempData["Error"] = "Error al obtener los artículos movidos en el periodo: " + ex.Message;
            }

            return View(movimientoDeArticulos);
        }

		public ActionResult<IEnumerable<ResumenMovimientos>> ResumenCantMovidas()
        {
	        try
	        {
                string token = HttpContext.Session.GetString("logueadoToken");

                if (HttpContext.Session.GetString("logueadoRol") != "Encargado" || string.IsNullOrEmpty(token))
                {
                    TempData["error"] = "No tiene permisos para registrar movimientos de stock.";
                    return RedirectToAction("Login", "Usuario");
                }

                _cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Bearer",
                    token
                );

                Uri uri = new Uri(baseURL+ "/resumen");
		        HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);

		        Task<HttpResponseMessage> respuesta = _cliente.SendAsync(solicitud);
		        respuesta.Wait();
		        IEnumerable<ResumenMovimientos> resumenMovimientos;

				if (respuesta.Result.IsSuccessStatusCode)
		        {
			        var datosRespuesta = respuesta.Result.Content.ReadAsStringAsync().Result;
			         resumenMovimientos = JsonConvert.DeserializeObject<IEnumerable<ResumenMovimientos>>(datosRespuesta);
				}
		        else
				{
					resumenMovimientos = new List<ResumenMovimientos>();
					TempData["Error"] = "Se produjo en error al consultar el servidor.";
		        }

				return View(resumenMovimientos);
			}
	        catch (Exception e)
	        {
		        
		        TempData["Error"] = e.Message;
				return View();
	        }
        }
    }
}
