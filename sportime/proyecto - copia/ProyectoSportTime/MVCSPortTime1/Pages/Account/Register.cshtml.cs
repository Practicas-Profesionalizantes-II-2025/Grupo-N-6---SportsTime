using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVCSPortTime1.Services;
using Shared.Entidades;
using System.Text.RegularExpressions;

namespace MVCSPortTime1.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly CDatos.Data.DataContext _db;
        private readonly IAuthService _auth;
        public RegisterModel(CDatos.Data.DataContext db, IAuthService auth)
        {
            _db = db; _auth = auth;
        }

        [BindProperty]
        public RegisterInput Input { get; set; } = new();
        public string? Error { get; set; }

        public IActionResult OnGet()
        {
            if (User?.Identity?.IsAuthenticated == true) return RedirectToPage("/Index");
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var email = Input.Email.Trim().ToLowerInvariant();
            if (!Regex.IsMatch(email, @"^[A-Za-z0-9._%+-]+@gmail\.com$"))
            {
                ModelState.AddModelError("Input.Email", "Debe ser un Gmail válido (usuario@gmail.com)");
                return Page();
            }

            var existe = _db.Usuarios.FirstOrDefault(u => u.Email.ToLower() == email);
            if (existe != null)
            {
                Error = "Ya existe un usuario con ese email";
                return Page();
            }

            var hash = _auth.HashPassword(Input.Password);

            var user = new global::Shared.Entidades.Usuarios
            {
                Nombre = Input.Nombre.Trim(),
                Apellido = Input.Apellido?.Trim() ?? string.Empty,
                Email = email,
                NumeroTelefono = Input.Telefono?.Trim() ?? string.Empty,
                PasswordHash = hash,
                Rol = "Usuario"
            };
            _db.Add(user);
            await _db.SaveChangesAsync();

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                LoginModel.BuildPrincipal(user, persist: true), new AuthenticationProperties { IsPersistent = true });

            return RedirectToPage("/Index");
        }

        public class RegisterInput
        {
            [Required, StringLength(120)]
            public string Nombre { get; set; } = string.Empty;

            [Required, StringLength(120)]
            public string Apellido { get; set; } = string.Empty;

            [Required, EmailAddress]
            public string Email { get; set; } = string.Empty;

            [Phone]
            public string? Telefono { get; set; }

            [Required, MinLength(6)]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;

            [Required, Compare(nameof(Password), ErrorMessage = "Las contraseñas no coinciden")]
            [DataType(DataType.Password)]
            public string ConfirmPassword { get; set; } = string.Empty;
        }
    }
}
