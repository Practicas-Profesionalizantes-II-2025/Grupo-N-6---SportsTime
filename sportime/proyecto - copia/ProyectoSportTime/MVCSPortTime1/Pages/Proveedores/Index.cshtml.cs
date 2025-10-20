using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CNegocio.Contracts;

namespace MVCSPortTime1.Pages.Proveedores
{
    public class IndexModel : PageModel
    {
        private readonly IProveedores _proveedores;

        public IndexModel(IProveedores proveedores)
        {
            _proveedores = proveedores;
        }

        [BindProperty]
        public ProveedorInput Input { get; set; } = new();

        public System.Collections.Generic.List<global::Shared.Dtos.ProveedorDTO> Proveedores { get; set; } = new();

        public async Task OnGet()
        {
            Proveedores = await _proveedores.ObtenerTodosLosProveedores();
        }

        public async Task<IActionResult> OnPostGuardar()
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Revisá los campos marcados.";
                Proveedores = await _proveedores.ObtenerTodosLosProveedores();
                return Page();
            }

            try
            {
                var dto = new global::Shared.Dtos.ProveedorDTO
                {
                    Nombre = Input.Razon,
                    Telefono = Input.Telefono,
                    Email = Input.Email,
                    Direccion = Input.Direccion
                };
                await _proveedores.CrearProveedor(dto);
                TempData["SuccessMessage"] = "Proveedor guardado.";
                ModelState.Clear();
                Input = new();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            Proveedores = await _proveedores.ObtenerTodosLosProveedores();
            return Page();
        }

        public class ProveedorInput
        {
            [Required, StringLength(120)]
            [Display(Name = "Razón social")]
            public string Razon { get; set; }

            public string Direccion { get; set; }

            [Phone]
            [Display(Name = "Teléfono")]
            public string Telefono { get; set; }

            [EmailAddress]
            public string Email { get; set; }
        }
    }
}
