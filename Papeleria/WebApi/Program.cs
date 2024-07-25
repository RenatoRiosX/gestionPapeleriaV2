using AccesoDatos.RepositorioEF;
using LogicaAplicacion.ImplementacionCasosUsos.Articulos;
using LogicaAplicacion.ImplementacionCasosUsos.Clientes;
using LogicaAplicacion.ImplementacionCasosUsos.MovimientosStock;
using LogicaAplicacion.ImplementacionCasosUsos.Pedidos;
using LogicaAplicacion.ImplementacionCasosUsos.TipoMovimientosStock;
using LogicaAplicacion.ImplementacionCasosUsos.Usuarios;
using LogicaAplicacion.InterfacesCasosUsos.Articulos;
using LogicaAplicacion.InterfacesCasosUsos.Clientes;
using LogicaAplicacion.InterfacesCasosUsos.MovimientoStock;
using LogicaAplicacion.InterfacesCasosUsos.Pedidos;
using LogicaAplicacion.InterfacesCasosUsos.TipoMovimientoStock;
using LogicaAplicacion.InterfacesCasosUsos.Usuarios;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using LogicaAplicacion.ImplementacionCasosUsos.Configuraciones;
using LogicaAplicacion.InterfacesCasosUsos.Configuraciones;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
var ruta = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebApi.xml");
builder.Services.AddSwaggerGen(opciones =>
        {
	        opciones.IncludeXmlComments(ruta);
			opciones.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
				Title = "API de Stock Papeleria",
				Version = "v1",
				Description = "Documentación del obligatorio 2 de programación 3. Sistema de gestión de stock de una papelería.",
				Contact = new OpenApiContact{ Email = "renatoriosx@gmail.com"}
            });
        }
    );

//Se inyecta el repositorio en el controlador
builder.Services.AddScoped<IRepositorioArticulos, RepositorioArticulosEF>();
builder.Services.AddScoped<IRepositorioClientes, RepositorioClientesEF>();
builder.Services.AddScoped<IRepositorioPedidos, RepositorioPedidosEF>();
builder.Services.AddScoped<IRepositorioTipoMovimientoStock,RepositorioTipoMovimientoEF>();
builder.Services.AddScoped<IRepositorioMovimientoStock, RepositorioMovimientoStockEF>();
builder.Services.AddScoped<IRepositorioUsuarios, RepositorioUsuariosEF>();
builder.Services.AddScoped<IRepositorioConfiguracion, RepositorioConfiguracionEF>();

//casos de uso MovimientoStock
builder.Services.AddScoped<IAltaMovimientoStock, AltaMovimientoStock>();
builder.Services.AddScoped<IGetAllArticulos, GetAllArticulos>();
builder.Services.AddScoped<IArticulosConMovimientosEntreFechas, ArticulosConMovimientosEntreFechas>();
builder.Services.AddScoped<IObtenerResumenMovimientos, ObtenerResumenMovimientos>();
builder.Services.AddScoped<IGetMovimientoStockTipoArticulo, GetMovimientoStockTipoArticulo>();


//casos de uso TipoMovimientoStock
builder.Services.AddScoped<IAltaTipoMovimientoStock,AltaTipoMovimientoStock>();
builder.Services.AddScoped<IBajaTipoMovimientoStock, BajaTipoMovimientoStock>();
builder.Services.AddScoped<IEditarTipoMovimientoStock, EditarTipoMovimientoStock>();
builder.Services.AddScoped<IGetAllTipoMovimientoStock, GetAllTipoMovimientoStock>();
builder.Services.AddScoped<IGetByIdTipoMovimientoStock, GetByIdTipoMovimientoStock>();

// casos configuracion
builder.Services.AddScoped<IGetValorPorNombre, GetValorPorNombre>();


//casos de uso Usuario
builder.Services.AddScoped<ILogin, Login>();

//Casos de uso 
builder.Services.AddScoped<IBuscarClientePorRazonSocial, BuscarClientePorRazonSocial>();
builder.Services.AddScoped<IMostrarArticulos, MostrarArticulos>();
builder.Services.AddScoped<IObtenerPedidosAnulados, ObtenerPedidosAnulados>();

//Servicios necesarios para autenticacion
var claveDificil = "ClaveMuySecrecta1_ClaveMuySecrecta1_ClaveMuySecrecta1_ClaveMuySecrecta1_ClaveMuySecrecta1";
var claveDificilEncriptada = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveDificil));

//Registro de servicios JWT 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(opt =>
{
	opt.TokenValidationParameters = new TokenValidationParameters
	{
		//Definir la verificaciones  a realizar
		ValidateIssuer = false,
		ValidateAudience = false,
		ValidateLifetime = false,
		ValidateIssuerSigningKey = true,
		IssuerSigningKey= claveDificilEncriptada
	};
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();// agregamos este para el uso de la token , van en orden(es importante)

app.UseAuthorization();

app.MapControllers();

app.Run();
