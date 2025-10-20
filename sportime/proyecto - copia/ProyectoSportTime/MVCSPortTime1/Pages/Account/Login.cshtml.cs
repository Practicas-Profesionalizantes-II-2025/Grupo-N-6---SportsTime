using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MVCSPortTime1.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Usuario { get; set; } = string.Empty;
        [BindProperty]
        public string Password { get; set; } = string.Empty;
        [BindProperty]
        public bool Recordar { get; set; }
        public string? Error { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(Usuario) || string.IsNullOrWhiteSpace(Password))
            {
                Error = "Credenciales inválidas";
                return Page();
            }

            return RedirectToPage("/Index");
        }
    }
}
