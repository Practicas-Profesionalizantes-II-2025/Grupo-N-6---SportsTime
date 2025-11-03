using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVCSPortTime1.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace MVCSPortTime1.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IAuthService _auth;
        private readonly CDatos.Data.DataContext _db;
        public LoginModel(IAuthService auth, CDatos.Data.DataContext db) { _auth = auth; _db = db; }

        [BindProperty]
        public string Usuario { get; set; } = string.Empty;
        [BindProperty]
        public string Password { get; set; } = string.Empty;
        [BindProperty]
        public bool Recordar { get; set; }
        public string? Error { get; set; }

        public IActionResult OnGet()
        {
            if (User?.Identity?.IsAuthenticated == true)
                return RedirectToPage("/Index");
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (string.IsNullOrWhiteSpace(Usuario) || string.IsNullOrWhiteSpace(Password))
            {
                Error = "Credenciales inválidas";
                return Page();
            }

            var user = await _auth.ValidateAsync(Usuario.Trim(), Password.Trim());
            if (user == null)
            {
                Error = "Usuario o contraseña incorrectos";
                return Page();
            }

            // Asegurar vinculación Cliente <-> Usuario al iniciar sesión (por email o por nombre)
            var cliente = await _db.Clientes.FirstOrDefaultAsync(c => c.Email.ToLower() == (user.Email ?? string.Empty).ToLower())
                          ?? await _db.Clientes.FirstOrDefaultAsync(c => c.Nombre == (user.Nombre ?? string.Empty));
            if (cliente == null)
            {
                _db.Clientes.Add(new global::Shared.Entidades.Clientes
                {
                    Nombre = $"{user.Nombre} {user.Apellido}".Trim(),
                    Email = user.Email ?? string.Empty,
                    NumeroTelefono = user.NumeroTelefono ?? string.Empty,
                    Usuario_ID = user.Usuario_ID
                });
                await _db.SaveChangesAsync();
            }
            else if (cliente.Usuario_ID != user.Usuario_ID || cliente.Email != user.Email || cliente.NumeroTelefono != user.NumeroTelefono || cliente.Nombre != $"{user.Nombre} {user.Apellido}".Trim())
            {
                cliente.Usuario_ID = user.Usuario_ID;
                cliente.Email = user.Email ?? cliente.Email;
                cliente.NumeroTelefono = user.NumeroTelefono ?? cliente.NumeroTelefono;
                cliente.Nombre = $"{user.Nombre} {user.Apellido}".Trim();
                await _db.SaveChangesAsync();
            }

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                BuildPrincipal(user, Recordar), new AuthenticationProperties { IsPersistent = Recordar });

            return RedirectToPage("/Index");
        }

        public static ClaimsPrincipal BuildPrincipal(global::Shared.Entidades.Usuarios user, bool persist)
        {
            var claims = new List<global::System.Security.Claims.Claim>
            {
                new global::System.Security.Claims.Claim(ClaimTypes.NameIdentifier, user.Usuario_ID.ToString()),
                new global::System.Security.Claims.Claim(ClaimTypes.Name, user.Nombre ?? user.Email ?? "Usuario"),
                new global::System.Security.Claims.Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new global::System.Security.Claims.Claim(ClaimTypes.Role, user.Rol ?? "Usuario")
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(identity);
        }
    }
}
