using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVCSPortTime1.Services;
using ClientesEntity = Shared.Entidades.Clientes;

namespace MVCSPortTime1.Pages.Clientes
{
    public class IndexModel : PageModel
    {
        private readonly IClientesAppService _clientes;
        public IndexModel(IClientesAppService clientes)
        {
            _clientes = clientes;
        }

        [BindProperty]
        public ClienteInputModel Input { get; set; } = new();

        [BindProperty]
        public int? EditId { get; set; }

        public List<ClientesEntity> Items { get; set; } = new();

        public async Task OnGet()
        {
            Items = await _clientes.Listar();
        }

        public async Task<IActionResult> OnPostGuardar()
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Revisá los campos.";
                Items = await _clientes.Listar();
                return Page();
            }

            var (ok, msg) = await _clientes.Crear(new ClienteInput
            {
                Nombre = Input.Nombre,
                Email = Input.Email,
                Telefono = Input.Telefono
            });

            TempData[ok ? "SuccessMessage" : "ErrorMessage"] = msg;
            ModelState.Clear();
            Input = new();
            Items = await _clientes.Listar();
            return Page();
        }

        public async Task<IActionResult> OnPostEditar(int id)
        {
            var cli = await _clientes.Obtener(id);
            if (cli == null)
            {
                TempData["ErrorMessage"] = "Cliente no encontrado";
            }
            else
            {
                EditId = cli.Cliente_ID;
                Input = new ClienteInputModel
                {
                    Nombre = cli.Nombre,
                    Telefono = cli.NumeroTelefono,
                    Email = cli.Email
                };
            }
            Items = await _clientes.Listar();
            return Page();
        }

        public async Task<IActionResult> OnPostActualizar()
        {
            if (EditId == null)
            {
                TempData["ErrorMessage"] = "Seleccione un cliente.";
                Items = await _clientes.Listar();
                return Page();
            }
            var (ok, msg) = await _clientes.Actualizar(EditId.Value, new ClienteInput
            {
                Nombre = Input.Nombre,
                Telefono = Input.Telefono,
                Email = Input.Email
            });
            TempData[ok ? "SuccessMessage" : "ErrorMessage"] = msg;
            EditId = null; Input = new();
            Items = await _clientes.Listar();
            return Page();
        }

        public async Task<IActionResult> OnPostEliminar(int id)
        {
            var (ok, msg) = await _clientes.Eliminar(id);
            TempData[ok ? "SuccessMessage" : "ErrorMessage"] = msg;
            Items = await _clientes.Listar();
            return Page();
        }

        public class ClienteInputModel
        {
            [Required, StringLength(120)]
            public string Nombre { get; set; } = string.Empty;

            [Phone]
            [Display(Name = "Teléfono")]
            public string Telefono { get; set; } = string.Empty;

            [EmailAddress]
            public string? Email { get; set; }
        }
    }
}
