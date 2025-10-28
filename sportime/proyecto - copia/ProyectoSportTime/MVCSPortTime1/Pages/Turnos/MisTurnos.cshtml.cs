using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCSPortTime1.Services;
using System.Security.Claims;

namespace MVCSPortTime1.Pages.Turnos
{
    [Authorize(Policy = "ClienteOnly")]
    public class MisTurnosModel : PageModel
    {
        private readonly ITurnosAppService _turnos;
        private readonly CDatos.Data.DataContext _db;

        public MisTurnosModel(ITurnosAppService turnos, CDatos.Data.DataContext db)
        {
            _turnos = turnos; _db = db;
        }

        [BindProperty]
        public InputVM Input { get; set; } = new();
        [BindProperty]
        public int? EditId { get; set; }

        public List<global::Shared.Entidades.Canchas> Canchas { get; set; } = new();
        public List<global::Shared.Entidades.Deportes> Deportes { get; set; } = new();
        public List<global::Shared.Entidades.Productos> Productos { get; set; } = new();

        public Dictionary<int, string> NombreCancha { get; set; } = new();
        public List<SelectListItem> CanchaOptions { get; set; } = new();

        public async Task OnGet(){ await CargarListas(); }

        // AJAX: disponibilidad
        public async Task<IActionResult> OnGetCheck(int canchaId, DateTime inicio, DateTime fin)
        {
            var available = !await _db.Turnos.AsNoTracking().AnyAsync(t => t.Cancha_ID == canchaId && inicio < t.HoraFin && fin > t.HoraInicio);
            var msg = available ? "Horario disponible" : "La cancha ya está ocupada en ese horario";
            return new JsonResult(new { available, message = msg });
        }

        public async Task<IActionResult> OnPostGuardar()
        {
            await CargarListas();
            var (ok, msg) = await _turnos.Crear(new TurnoInput
            {
                Cancha_ID = Input.Cancha_ID,
                HoraInicio = Input.HoraInicio,
                HoraFin = Input.HoraFin,
                Cliente_ID = await GetClienteIdActual(),
                Consumos = Input.Consumos.Select(c => new ConsumoInput{ Producto_ID = c.Producto_ID ?? 0, Cantidad = c.Cantidad ?? 0}).ToList()
            }, usuarioId: GetUserId());
            TempData[ok ? "SuccessMessage" : "ErrorMessage"] = msg;
            return Page();
        }

        private int GetUserId(){ var c = User.FindFirstValue(ClaimTypes.NameIdentifier); return int.TryParse(c, out var id) ? id : 0; }
        private async Task<int?> GetClienteIdActual()
        {
            var uid = GetUserId();
            var cli = await _db.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.Usuario_ID == uid);
            return cli?.Cliente_ID;
        }

        private async Task CargarListas()
        {
            Canchas = await _db.Canchas.AsNoTracking().ToListAsync();
            Deportes = await _db.Deportes.AsNoTracking().ToListAsync();
            Productos = await _db.Productos.AsNoTracking().ToListAsync();
            NombreCancha = Canchas.ToDictionary(c => c.Cancha_ID, c => $"Cancha {c.Cancha_ID} - {Deportes.FirstOrDefault(d => d.Deporte_ID == c.Deporte_ID)?.Nombre}");
            CanchaOptions = Canchas.Select(c => new SelectListItem{ Value = c.Cancha_ID.ToString(), Text = NombreCancha[c.Cancha_ID]}).ToList();
        }

        public class ConsumoVM{ public int? Producto_ID { get; set; } public int? Cantidad { get; set; } }
        public class InputVM
        {
            public int? Cancha_ID { get; set; }
            public DateTime? HoraInicio { get; set; }
            public DateTime? HoraFin { get; set; }
            public List<ConsumoVM> Consumos { get; set; } = new();
        }
    }
}
