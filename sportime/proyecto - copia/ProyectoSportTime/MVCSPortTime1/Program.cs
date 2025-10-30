using CDatos.Repositorys;
using CDatos.Repositorys.IRepositorys;
using CNegocio.Contracts;
using CNegocio.Implementations;
using Microsoft.EntityFrameworkCore;
using CDatos.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using MVCSPortTime1.Services;
using Shared.Entidades;
using Prometheus;
using MVCSPortTime1.Monitoring;

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

    // Autorización por roles (políticas)
    options.Conventions.AuthorizeFolder("/Clientes", policy: "AdminOnly");
    options.Conventions.AuthorizeFolder("/Productos", policy: "AdminOnly");
    options.Conventions.AuthorizeFolder("/Proveedores", policy: "AdminOnly");
    options.Conventions.AuthorizeFolder("/Canchas", policy: "AdminOnly");
    // Página de cliente
    options.Conventions.AuthorizePage("/Turnos/MisTurnos", policy: "ClienteOnly");
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
        o.AccessDeniedPath = "/Account/AccessDenied";
    });

// Autorización
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", p => p.RequireRole("Admin"));
    // Aceptar "Cliente" y antiguos "Usuario" como clientes válidos
    options.AddPolicy("ClienteOnly", p => p.RequireRole("Cliente", "Usuario"));
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

var app = builder.Build();   

// Aplicar migraciones y seed de admin
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<DataContext>();
        db.Database.Migrate();

        // Seed Admin (si no existe)
        var auth = scope.ServiceProvider.GetRequiredService<IAuthService>();
        const string adminEmail = "admin@sporttime.local";
        var admin = db.Usuarios.FirstOrDefault(u => u.Email == adminEmail);
        if (admin == null)
        {
            db.Usuarios.Add(new Usuarios
            {
                Nombre = "Admin",
                Apellido = "",
                Email = adminEmail,
                NumeroTelefono = "",
                PasswordHash = auth.HashPassword("Admin123!"),
                Rol = "Admin"
            });
            db.SaveChanges();
            Console.WriteLine("Admin creado: admin@sporttime.local / Admin123!");
        }
    }
    catch (Exception ex)
    {
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

// Prometheus metrics
app.UseHttpMetrics();
app.MapMetrics(); // expone /metrics

// Contador simple de errores 500
app.Use(async (ctx, next) =>
{
    try
    {
        await next();
        // Si el status fue establecido a 500 por un middleware previo
        if (ctx.Response.StatusCode >= 500) AppMetrics.Error("500");
    }
    catch
    {
        AppMetrics.Error("500");
        throw;
    }
});

app.UseAuthentication();
app.UseAuthorization();

// Razor Pages endpoints
app.MapRazorPages();

// MVC default route (kept for compatibility)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
