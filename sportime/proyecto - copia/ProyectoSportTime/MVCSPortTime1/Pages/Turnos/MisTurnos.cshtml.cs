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

        public List<global::Shared.Entidades.Turnos> Items { get; set; } = new();
        public Dictionary<int, string> ProductosPorTurno { get; set; } = new();
        public List<global::Shared.Entidades.Canchas> Canchas { get; set; } = new();
        public List<global::Shared.Entidades.Deportes> Deportes { get; set; } = new();
        public List<global::Shared.Entidades.Productos> Productos { get; set; } = new();

        public Dictionary<int, string> NombreCancha { get; set; } = new();
        public List<SelectListItem> CanchaOptions { get; set; } = new();

        public async Task OnGet(){ await CargarListas(); await CargarMisTurnos(); }

        // AJAX: disponibilidad simple (mantener)
        public async Task<IActionResult> OnGetCheck(int canchaId, DateTime inicio, DateTime fin)
        {
            if (inicio < DateTime.Now)
                return new JsonResult(new { available = false, message = "No se puede reservar en el pasado" });
            var available = !await _db.Turnos.AsNoTracking().AnyAsync(t => t.Cancha_ID == canchaId && inicio < t.HoraFin && fin > t.HoraInicio);
            var msg = available ? "Horario disponible" : "La cancha ya está ocupada en ese horario";
            return new JsonResult(new { available, message = msg });
        }

        // AJAX: lista de horas disponibles para una fecha (bloques de 1 hora)
        public async Task<IActionResult> OnGetHours(int canchaId, DateTime fecha)
        {
            var dayStart = fecha.Date;
            var dayEnd = dayStart.AddDays(1);
            var reservas = await _db.Turnos.AsNoTracking()
                .Where(t => t.Cancha_ID == canchaId && t.HoraInicio < dayEnd && t.HoraFin > dayStart)
                .Select(t => new { t.HoraInicio, t.HoraFin })
                .ToListAsync();

            var booked = new HashSet<int>();
            foreach (var r in reservas)
            {
                var h = r.HoraInicio;
                while (h < r.HoraFin)
                {
                    booked.Add(h.Hour);
                    h = h.AddHours(1);
                }
            }
            // Rango 15:00 a 23:00 -> inicios posibles 15..22
            const int H_INI = 15; const int H_FIN = 23;
            var ahora = DateTime.Now;
            var horas = Enumerable.Range(H_INI, H_FIN - H_INI)
                .Select(h => new
                {
                    hour = h,
                    available = !booked.Contains(h) && dayStart.AddHours(h) > ahora
                })
                .ToList();
            return new JsonResult(horas);
        }

        public async Task<IActionResult> OnPostGuardar()
        {
            await CargarListas();
            var clienteId = await GetClienteIdActual();
            if (clienteId == null)
            {
                TempData["ErrorMessage"] = "No se encontró el cliente asociado a tu usuario.";
                await CargarMisTurnos();
                return Page();
            }
            var (ok, msg) = await _turnos.Crear(new TurnoInput
            {
                Cancha_ID = Input.Cancha_ID,
                HoraInicio = Input.HoraInicio,
                HoraFin = Input.HoraFin,
                Cliente_ID = clienteId,
                Consumos = Input.Consumos.Select(c => new ConsumoInput{ Producto_ID = c.Producto_ID ?? 0, Cantidad = c.Cantidad ?? 0}).ToList()
            }, usuarioId: GetUserId());
            TempData[ok ? "SuccessMessage" : "ErrorMessage"] = msg;
            if (ok) { ModelState.Clear(); Input = new(); }
            await CargarMisTurnos();
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

        private async Task CargarMisTurnos()
        {
            var uid = GetUserId();
            Items = await _db.Turnos.AsNoTracking().Where(t => t.Usuario_ID == uid).OrderBy(t => t.HoraInicio).ToListAsync();
            ProductosPorTurno = await (
                from tp in _db.TurnoProductos.AsNoTracking()
                join p in _db.Productos.AsNoTracking() on tp.Producto_ID equals p.Producto_ID
                group new { tp, p } by tp.Turno_ID into g
                select new { Turno_ID = g.Key, Texto = string.Join(", ", g.Select(x => $"{x.p.TipoProducto} x{x.tp.Cantidad}")) }
            ).ToDictionaryAsync(k => k.Turno_ID, v => v.Texto);
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
