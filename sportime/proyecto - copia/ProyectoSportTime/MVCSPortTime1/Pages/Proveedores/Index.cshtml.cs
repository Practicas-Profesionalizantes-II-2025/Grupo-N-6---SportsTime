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

        [BindProperty]
        public int? EditId { get; set; }

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

        public async Task<IActionResult> OnPostEditar(int id)
        {
            var dto = await _proveedores.ObtenerProveedorPorId(id);
            if (dto == null)
            {
                TempData["ErrorMessage"] = "Proveedor no encontrado";
            }
            else
            {
                EditId = dto.Proveedor_ID;
                Input = new ProveedorInput
                {
                    Razon = dto.Nombre,
                    Direccion = dto.Direccion,
                    Telefono = dto.Telefono,
                    Email = dto.Email
                };
                ViewData["OpenProveedorModal"] = true;
            }
            Proveedores = await _proveedores.ObtenerTodosLosProveedores();
            return Page();
        }

        public async Task<IActionResult> OnPostActualizar()
        {
            if (EditId == null)
            {
                TempData["ErrorMessage"] = "Seleccione un proveedor";
                Proveedores = await _proveedores.ObtenerTodosLosProveedores();
                return Page();
            }
            try
            {
                await _proveedores.ModificarProveedor(new global::Shared.Dtos.ProveedorDTO
                {
                    Proveedor_ID = EditId.Value,
                    Nombre = Input.Razon,
                    Direccion = Input.Direccion,
                    Telefono = Input.Telefono,
                    Email = Input.Email
                });
                TempData["SuccessMessage"] = "Proveedor actualizado";
                EditId = null; Input = new();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            Proveedores = await _proveedores.ObtenerTodosLosProveedores();
            return Page();
        }

        public async Task<IActionResult> OnPostEliminar(int id)
        {
            try
            {
                await _proveedores.BajaProveedor(id);
                TempData["SuccessMessage"] = "Proveedor eliminado";
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
