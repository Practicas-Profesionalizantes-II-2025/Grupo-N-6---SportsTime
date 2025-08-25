
using CDatos.Data;
using CDatos.Repositorys;
using CDatos.Repositorys.IRepositorys;
using CNegocio.Contracts;
using CNegocio.Implementations;
using Microsoft.EntityFrameworkCore;
using Negocio.Contracts;
using Negocio.Implementations;
using Negocio.Repositorys;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

// Configuración del DbContext con la cadena de conexión
builder.Services.AddDbContext<ProyectoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBContext")));

// Registro de servicios de lógica
builder.Services.AddScoped<IAdministrador, AdministradorLogic>();
builder.Services.AddScoped<ICanchas, CanchasLogic>();
builder.Services.AddScoped<IClientes, ClientesLogic>();
builder.Services.AddScoped<IConsumiciones, ConsumicionesLogic>();
builder.Services.AddScoped<IConsumicionXTurno, ConsumicionXturnoLogic>();
builder.Services.AddScoped<IDeportes, DeportesLogic>();
builder.Services.AddScoped<IProductos, ProductosLogic>();
builder.Services.AddScoped<IProveedores, ProveedoresLogic>();
builder.Services.AddScoped<ITurnos, TurnosLogic>();


// Registro de repositorio
builder.Services.AddScoped<IAdministradorRepository, AdministradorRepository>();
builder.Services.AddScoped<ICanchasRepository, CanchasRepository>();
builder.Services.AddScoped<IClienteRepository, ClientesRepository>();
builder.Services.AddScoped<IConsumicionesRepository, ConsumicionesRepository>();
builder.Services.AddScoped<IConsumicionXTurnoRepository, ConsumicionXTurnoRepository>();
builder.Services.AddScoped<IDeportesRepository, DeportesRepository>();
builder.Services.AddScoped<IProductoRepository, ProductosRepository>();
builder.Services.AddScoped<IProveedoresRepository, ProveedoresRepository>();
builder.Services.AddScoped<ITurnosRepository, TurnosRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
