using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CNegocio.Contracts;

namespace MVCSPortTime1.Pages.Productos
{
    public class IndexModel : PageModel
    {
        private readonly IProductos _productos;
        private readonly IProveedores _proveedoresLogic;

        public IndexModel(IProductos productos, IProveedores proveedoresLogic)
        {
            _productos = productos;
            _proveedoresLogic = proveedoresLogic;
        }

        [BindProperty]
        public ProductoInput Input { get; set; } = new();

        public List<global::Shared.Dtos.ProductoDTO> Productos { get; set; } = new();
        public List<global::Shared.Dtos.ProveedorDTO> Proveedores { get; set; } = new();
        public List<SelectListItem> ProveedorOptions { get; set; } = new();

        public async Task OnGet()
        {
            await CargarListas();
        }

        public async Task<IActionResult> OnPostGuardar()
        {
            await CargarListas();

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Revisá los campos marcados.";
                return Page();
            }

            try
            {
                var dto = new global::Shared.Dtos.ProductoDTO
                {
                    TipoProducto = Input.Tipo,
                    Proveedor_ID = Input.ProveedorId!.Value,
                    Precio = Input.Precio ?? 0
                };
                await _productos.AltaProducto(dto);
                TempData["SuccessMessage"] = "Producto guardado.";
                ModelState.Clear();
                Input = new();
                await CargarListas();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return Page();
        }

        private async Task CargarListas()
        {
            Productos = await _productos.ObtenerTodosLosProductos();
            Proveedores = await _proveedoresLogic.ObtenerTodosLosProveedores();
            ProveedorOptions = Proveedores
                .Select(p => new SelectListItem { Value = p.Proveedor_ID.ToString(), Text = p.Nombre })
                .ToList();
        }

        public class ProductoInput
        {
            [Required]
            public string Tipo { get; set; }

            [Display(Name = "Descripción")]
            public string Descripcion { get; set; }

            [Required(ErrorMessage = "Seleccione un proveedor")]
            public int? ProveedorId { get; set; }

            [Range(0, double.MaxValue)]
            public decimal? Precio { get; set; }
        }
    }
}
