
using CDatos.Data;
using CDatos.Repositorys;
using CDatos.Repositorys.IRepositorys;
using CNegocio.Contracts;
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

// Registro de repositorio
builder.Services.AddScoped<IAdministradorRepository, AdministradorRepository>();
builder.Services.AddScoped<ICanchasRepository, CanchasRepository>();

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
