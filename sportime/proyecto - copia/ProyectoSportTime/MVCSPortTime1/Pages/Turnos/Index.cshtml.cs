using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVCSPortTime1.Services;
using TurnoEntity = Shared.Entidades.Turnos;
using CanchaEntity = Shared.Entidades.Canchas;

namespace MVCSPortTime1.Pages.Turnos
{
    public class IndexModel : PageModel
    {
        private readonly ITurnosAppService _turnos;
        private readonly CDatos.Data.DataContext _db;
        public IndexModel(ITurnosAppService turnos, CDatos.Data.DataContext db)
        {
            _turnos = turnos; _db = db;
        }

        [BindProperty]
        public TurnoInputModel Input { get; set; } = new();
        [BindProperty]
        public int? EditId { get; set; }

        public List<TurnoEntity> Items { get; set; } = new();
        public List<CanchaEntity> Canchas { get; set; } = new();

        public async Task OnGet()
        {
            Items = await _turnos.Listar();
            Canchas = await _db.Canchas.AsNoTracking().ToListAsync();
        }

        public async Task<IActionResult> OnPostGuardar()
        {
            await CargarListas();
            var (ok, msg) = await _turnos.Crear(new TurnoInput
            {
                Cancha_ID = Input.Cancha_ID,
                HoraInicio = Input.HoraInicio,
                HoraFin = Input.HoraFin
            }, usuarioId: ObtenerUsuarioId());
            TempData[ok ? "SuccessMessage" : "ErrorMessage"] = msg;
            if (ok) { ModelState.Clear(); Input = new(); Items = await _turnos.Listar(); }
            return Page();
        }

        public async Task<IActionResult> OnPostEditar(int id)
        {
            await CargarListas();
            var t = await _turnos.Obtener(id);
            if (t == null) { TempData["ErrorMessage"] = "Turno no encontrado"; return Page(); }
            EditId = t.Turno_ID;
            Input = new TurnoInputModel { Cancha_ID = t.Cancha_ID, HoraInicio = t.HoraInicio, HoraFin = t.HoraFin };
            return Page();
        }

        public async Task<IActionResult> OnPostActualizar()
        {
            await CargarListas();
            if (EditId == null) { TempData["ErrorMessage"] = "Seleccione un turno"; return Page(); }
            var (ok, msg) = await _turnos.Actualizar(EditId.Value, new TurnoInput
            {
                Cancha_ID = Input.Cancha_ID,
                HoraInicio = Input.HoraInicio,
                HoraFin = Input.HoraFin
            });
            TempData[ok ? "SuccessMessage" : "ErrorMessage"] = msg;
            if (ok) { EditId = null; Input = new(); Items = await _turnos.Listar(); }
            return Page();
        }

        public async Task<IActionResult> OnPostEliminar(int id)
        {
            await CargarListas();
            var (ok, msg) = await _turnos.Eliminar(id);
            TempData[ok ? "SuccessMessage" : "ErrorMessage"] = msg;
            if (ok) Items = await _turnos.Listar();
            return Page();
        }

        private int ObtenerUsuarioId()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier);
            return int.TryParse(claim?.Value, out var id) ? id : 0;
        }

        private async Task CargarListas()
        {
            Items = await _turnos.Listar();
            Canchas = await _db.Canchas.AsNoTracking().ToListAsync();
        }

        public class TurnoInputModel
        {
            public int? Cancha_ID { get; set; }
            public DateTime? HoraInicio { get; set; }
            public DateTime? HoraFin { get; set; }
        }
    }
}
