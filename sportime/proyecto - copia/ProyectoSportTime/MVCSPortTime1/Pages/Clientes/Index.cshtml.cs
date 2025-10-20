using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MVCSPortTime1.Pages.Clientes
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public ClienteInput Input { get; set; } = new();

        public void OnGet() { }

        public IActionResult OnPostGuardar()
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Revisá los campos marcados.";
                return Page();
            }
            TempData["SuccessMessage"] = "Cliente guardado (demo).";
            ModelState.Clear();
            Input = new();
            return Page();
        }

        public class ClienteInput
        {
            [Required, StringLength(120)]
            public string Nombre { get; set; }

            [Required, Phone]
            [Display(Name = "Teléfono")]
            public string Telefono { get; set; }

            [EmailAddress]
            public string Email { get; set; }
        }
    }
}
