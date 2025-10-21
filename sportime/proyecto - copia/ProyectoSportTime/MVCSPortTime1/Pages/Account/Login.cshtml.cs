using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVCSPortTime1.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace MVCSPortTime1.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IAuthService _auth;
        public LoginModel(IAuthService auth) { _auth = auth; }

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
