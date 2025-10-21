using CDatos.Repositorys;
using CDatos.Repositorys.IRepositorys;
using CNegocio.Contracts;
using CNegocio.Implementations;
using Microsoft.EntityFrameworkCore;
using CDatos.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using MVCSPortTime1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(options =>
{
    // Requiere autenticación en todas las páginas
    options.Conventions.AuthorizeFolder("/");
    // Permitir anónimo para Login y Registro
    options.Conventions.AllowAnonymousToPage("/Account/Login");
    options.Conventions.AllowAnonymousToPage("/Account/Register");
});
builder.Services.AddHttpClient();

// EF Core
builder.Services.AddDbContext<DataContext>(options =>
{
    var cs = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(cs, sql => sql.EnableRetryOnFailure());
});

// Auth (cookies)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o =>
    {
        o.LoginPath = "/Account/Login";
        o.LogoutPath = "/Account/Logout";
        o.AccessDeniedPath = "/Account/Login";
    });

builder.Services.AddScoped<IAuthService, AuthService>();

// Repositorios y Lógica de Negocio existentes
builder.Services.AddScoped<IProveedoresRepository, ProveedoresRepository>();
builder.Services.AddScoped<IProveedores, ProveedoresLogic>();

builder.Services.AddScoped<IProductoRepository, ProductosRepository>();
builder.Services.AddScoped<IProductos, ProductosLogic>();

// Canchas + Deportes
builder.Services.AddScoped<ICanchasRepository, CanchasRepository>();
builder.Services.AddScoped<ICanchas, CanchasLogic>();

builder.Services.AddScoped<IDeportesRepository, DeportesRepository>();
builder.Services.AddScoped<IDeportes, DeportesLogic>();

// Clientes
builder.Services.AddScoped<IClientesAppService, ClientesAppService>();

// Turnos
builder.Services.AddScoped<ITurnosAppService, TurnosAppService>();

WebApplication app = builder.Build();   

// Aplicar migraciones automáticamente en arranque (solo desarrollo/probar)
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<DataContext>();
        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        // Log mínimo; en producción usar ILogger
        Console.WriteLine($"Error applying migrations: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Razor Pages endpoints
app.MapRazorPages();

// MVC default route (kept for compatibility)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
