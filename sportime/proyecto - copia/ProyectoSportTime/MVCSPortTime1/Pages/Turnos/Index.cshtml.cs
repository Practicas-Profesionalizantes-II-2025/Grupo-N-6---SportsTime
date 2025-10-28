using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCSPortTime1.Services;
using TurnoEntity = Shared.Entidades.Turnos;
using CanchaEntity = Shared.Entidades.Canchas;
using ClienteEntity = Shared.Entidades.Clientes;
using ProductoEntity = Shared.Entidades.Productos;
using DeporteEntity = Shared.Entidades.Deportes;

namespace MVCSPortTime1.Pages.Turnos
{
    public class IndexModel : PageModel
    {
        private readonly ITurnosAppService _turnos;
        private readonly CDatos.Data.DataContext _db;
        public IndexModel(ITurnosAppService turnos, CDatos.Data.DataContext db)
        { _turnos = turnos; _db = db; }

        [BindProperty]
        public TurnoInputModel Input { get; set; } = new();
        [BindProperty]
        public int? EditId { get; set; }

        public List<TurnoEntity> Items { get; set; } = new();
        public List<CanchaEntity> Canchas { get; set; } = new();
        public List<ClienteEntity> Clientes { get; set; } = new();
        public List<ProductoEntity> Productos { get; set; } = new();
        public List<DeporteEntity> Deportes { get; set; } = new();

        public Dictionary<int, string> NombreCancha { get; set; } = new();
        public List<SelectListItem> CanchaOptions { get; set; } = new();
        public Dictionary<int, string> ProductosPorTurno { get; set; } = new();

        public async Task OnGet() { Items = await _turnos.Listar(); await CargarListas(); }

        public async Task<IActionResult> OnPostGuardar()
        {
            await CargarListas();
            var (ok, msg) = await _turnos.Crear(new TurnoInput
            {
                Cancha_ID = Input.Cancha_ID,
                HoraInicio = Input.HoraInicio,
                HoraFin = Input.HoraFin,
                Cliente_ID = Input.Cliente_ID,
                Producto_ID = Input.Producto_ID,
                Cantidad = Input.Cantidad,
                Consumos = Input.Consumos.Select(c => new ConsumoInput{ Producto_ID = c.Producto_ID ?? 0, Cantidad = c.Cantidad ?? 0 }).ToList()
            }, usuarioId: ObtenerUsuarioId());
            TempData[ok ? "SuccessMessage" : "ErrorMessage"] = msg;
            if (ok) { ModelState.Clear(); Input = new(); Items = await _turnos.Listar(); await CargarProductosPorTurno(); }
            return Page();
        }

        public async Task<IActionResult> OnPostEditar(int id)
        {
            await CargarListas();
            var t = await _turnos.Obtener(id);
            if (t == null) { TempData["ErrorMessage"] = "Turno no encontrado"; return Page(); }
            EditId = t.Turno_ID;
            Input = new TurnoInputModel { Cancha_ID = t.Cancha_ID, HoraInicio = t.HoraInicio, HoraFin = t.HoraFin, Cliente_ID = t.Cliente_ID };
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
                HoraFin = Input.HoraFin,
                Cliente_ID = Input.Cliente_ID,
                Producto_ID = Input.Producto_ID,
                Cantidad = Input.Cantidad,
                Consumos = Input.Consumos.Select(c => new ConsumoInput{ Producto_ID = c.Producto_ID ?? 0, Cantidad = c.Cantidad ?? 0 }).ToList()
            });
            TempData[ok ? "SuccessMessage" : "ErrorMessage"] = msg;
            if (ok) { EditId = null; Input = new(); Items = await _turnos.Listar(); await CargarProductosPorTurno(); }
            return Page();
        }

        public async Task<IActionResult> OnPostEliminar(int id)
        {
            await CargarListas();
            var (ok, msg) = await _turnos.Eliminar(id);
            TempData[ok ? "SuccessMessage" : "ErrorMessage"] = msg;
            if (ok) { Items = await _turnos.Listar(); await CargarProductosPorTurno(); }
            return Page();
        }

        private int ObtenerUsuarioId()
        { var claim = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier); return int.TryParse(claim?.Value, out var id) ? id : 0; }

        private async Task CargarListas()
        {
            Items = await _turnos.Listar();
            Canchas = await _db.Canchas.AsNoTracking().ToListAsync();
            Clientes = await _db.Clientes.AsNoTracking().ToListAsync();
            Productos = await _db.Productos.AsNoTracking().ToListAsync();
            Deportes = await _db.Deportes.AsNoTracking().ToListAsync();

            NombreCancha = Canchas.Select(c => new { c.Cancha_ID, Texto = $"Cancha {c.Cancha_ID} - {Deportes.FirstOrDefault(d => d.Deporte_ID == c.Deporte_ID)?.Nombre ?? "Sin deporte"}" })
                                   .ToDictionary(k => k.Cancha_ID, v => v.Texto);
            CanchaOptions = Canchas.Select(c => new SelectListItem { Value = c.Cancha_ID.ToString(), Text = NombreCancha[c.Cancha_ID] }).ToList();
            await CargarProductosPorTurno();
        }

        private async Task CargarProductosPorTurno()
        {
            ProductosPorTurno = await (
                from tp in _db.TurnoProductos.AsNoTracking()
                join p in _db.Productos.AsNoTracking() on tp.Producto_ID equals p.Producto_ID
                group new { tp, p } by tp.Turno_ID into g
                select new { Turno_ID = g.Key, Texto = string.Join(", ", g.Select(x => $"{x.p.TipoProducto} x{x.tp.Cantidad}")) }
            ).ToDictionaryAsync(k => k.Turno_ID, v => v.Texto);
        }

        public class ConsumoVM
        {
            public int? Producto_ID { get; set; }
            public int? Cantidad { get; set; }
        }

        public class TurnoInputModel
        {
            public int? Cancha_ID { get; set; }
            public DateTime? HoraInicio { get; set; }
            public DateTime? HoraFin { get; set; }
            public int? Cliente_ID { get; set; }
            // Compatibilidad simple
            public int? Producto_ID { get; set; }
            public int? Cantidad { get; set; }
            // Múltiples consumos
            public List<ConsumoVM> Consumos { get; set; } = new();
        }
    }
}
