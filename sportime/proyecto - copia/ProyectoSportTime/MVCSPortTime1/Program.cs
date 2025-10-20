using CDatos.Repositorys;
using CDatos.Repositorys.IRepositorys;
using CNegocio.Contracts;
using CNegocio.Implementations;
using Microsoft.EntityFrameworkCore;
using CDatos.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient(); 
//builder.Services.AddScoped<IClientes, ClientesLogic>();
//builder.Services.AddScoped<IClienteRepository, ClientesRepository>();
//builder.Services.AddScoped<ICanchas, CanchasLogic>();
//builder.Services.AddScoped<IDeportes, DeportesLogic>();
//builder.Services.AddScoped<ICanchasRepository, CanchasRepository>();
//builder.Services.AddScoped<IDeportesRepository, DeportesRepository>();
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


WebApplication app = builder.Build();   

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
