using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CNegocio.Contracts;
using Shared.Dtos;

namespace MVCSPortTime1.Pages.Canchas
{
    public class IndexModel : PageModel
    {
        private readonly ICanchas _canchas;
        private readonly IDeportes _deportes;
        public IndexModel(ICanchas canchas, IDeportes deportes)
        {
            _canchas = canchas;
            _deportes = deportes;
        }

        [BindProperty]
        public CanchaInput Input { get; set; } = new();

        [BindProperty]
        public int? EditId { get; set; }

        public List<CanchaDTO> Items { get; set; } = new();
        public List<DeporteDTO> Deportes { get; set; } = new();

        public async Task OnGet()
        {
            Items = await _canchas.ObtenerTodasLasCanchas();
            Deportes = await _deportes.ObtenerTodosLosDeportes();
        }

        public async Task<IActionResult> OnPostGuardar()
        {
            await CargarListas();
            if (!ModelState.IsValid) return Page();
            try
            {
                await _canchas.CrearCancha(new CanchaDTO
                {
                    Deporte_ID = Input.DeporteId!.Value,
                    Activa = Input.Activa
                });
                TempData["SuccessMessage"] = "Cancha guardada.";
                ModelState.Clear(); Input = new();
                Items = await _canchas.ObtenerTodasLasCanchas();
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
            var c = await _canchas.ObtenerCanchaPorId(id);
            if (c == null)
            {
                TempData["ErrorMessage"] = "Cancha no encontrada";
                return Page();
            }
            EditId = c.Cancha_ID;
            Input = new CanchaInput { DeporteId = c.Deporte_ID, Activa = c.Activa };
            return Page();
        }

        public async Task<IActionResult> OnPostActualizar()
        {
            await CargarListas();
            if (EditId == null)
            {
                TempData["ErrorMessage"] = "Seleccione una cancha";
                return Page();
            }
            try
            {
                await _canchas.ModificarCancha(new CanchaDTO
                {
                    Cancha_ID = EditId.Value,
                    Deporte_ID = Input.DeporteId!.Value,
                    Activa = Input.Activa
                });
                TempData["SuccessMessage"] = "Cancha actualizada.";
                EditId = null; Input = new();
                Items = await _canchas.ObtenerTodasLasCanchas();
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
                await _canchas.BajaCancha(id);
                TempData["SuccessMessage"] = "Cancha eliminada.";
                Items = await _canchas.ObtenerTodasLasCanchas();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return Page();
        }

        private async Task CargarListas()
        {
            Deportes = await _deportes.ObtenerTodosLosDeportes();
            Items = await _canchas.ObtenerTodasLasCanchas();
        }

        public class CanchaInput
        {
            [Required]
            public int? DeporteId { get; set; }
            public bool Activa { get; set; } = true;
        }
    }
}
