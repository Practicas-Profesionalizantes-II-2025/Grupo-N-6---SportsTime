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

        public async Task<IActionResult> OnPostEditar(int id)
        {
            await CargarListas();
            var dto = await _productos.ObtenerProductoPorId(id);
            if (dto == null)
            {
                TempData["ErrorMessage"] = "Producto no encontrado";
                return Page();
            }
            Input = new ProductoInput
            {
                ProductoId = dto.Producto_ID,
                Tipo = dto.TipoProducto,
                Descripcion = string.Empty,
                ProveedorId = dto.Proveedor_ID,
                Precio = dto.Precio
            };
            ViewData["OpenProductoModal"] = true;
            return Page();
        }

        public async Task<IActionResult> OnPostActualizar()
        {
            await CargarListas();
            if (Input.ProductoId == null)
            {
                TempData["ErrorMessage"] = "Seleccione un producto";
                return Page();
            }
            try
            {
                await _productos.ModificarProducto(Input.ProductoId.Value, new global::Shared.Dtos.ProductoDTO
                {
                    Producto_ID = Input.ProductoId.Value,
                    TipoProducto = Input.Tipo,
                    Proveedor_ID = Input.ProveedorId!.Value,
                    Precio = Input.Precio ?? 0
                });
                TempData["SuccessMessage"] = "Producto actualizado";
                Input = new();
                await CargarListas();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostEliminar(int id)
        {
            await CargarListas();
            try
            {
                await _productos.BajaProducto(id);
                TempData["SuccessMessage"] = "Producto eliminado";
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
            public int? ProductoId { get; set; }
            [Required]
            public string Tipo { get; set; }
            public string Descripcion { get; set; }
            [Required(ErrorMessage = "Seleccione un proveedor")]
            public int? ProveedorId { get; set; }
            [Range(0, double.MaxValue)]
            public decimal? Precio { get; set; }
        }
    }
}
