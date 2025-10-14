using CDatos.Data;
using CDatos.Repositorys.IRepositorys;
using CDatos.Repositorys;
using CNegocio.Contracts;
using CNegocio.Implementations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

// Configuración del DbContext con la cadena de conexión
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBContext")));

// Registro de servicios de lógica
builder.Services.AddScoped<IDeportes, DeportesLogic>();
builder.Services.AddScoped<ICanchas, CanchasLogic>();
builder.Services.AddScoped<IProveedores, ProveedoresLogic>();
builder.Services.AddScoped<ITurnos, TurnosLogic>();

// Registro de repositorio
builder.Services.AddScoped<IDeportesRepository, DeportesRepository>();
builder.Services.AddScoped<ICanchasRepository, CanchasRepository>();
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
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
