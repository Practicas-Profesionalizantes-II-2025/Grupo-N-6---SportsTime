using CDatos.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Entidades;

namespace MVCSPortTime1.Services
{
    public interface ITurnosAppService
    {
        Task<(bool ok, string msg)> Crear(TurnoInput input, int usuarioId);
        Task<(bool ok, string msg)> Actualizar(int id, TurnoInput input);
        Task<(bool ok, string msg)> Eliminar(int id);
        Task<List<Turnos>> Listar();
        Task<Turnos?> Obtener(int id);
    }

    public class ConsumoInput
    {
        public int Producto_ID { get; set; }
        public int Cantidad { get; set; }
    }

    public class TurnoInput
    {
        public DateTime? HoraInicio { get; set; }
        public DateTime? HoraFin { get; set; }
        public int? Cancha_ID { get; set; }
        public int? Cliente_ID { get; set; }
        // Compatibilidad: un solo producto opcional
        public int? Producto_ID { get; set; }
        public int? Cantidad { get; set; }
        // Nuevo: múltiples consumos
        public List<ConsumoInput> Consumos { get; set; } = new();
    }

    public class TurnosAppService : ITurnosAppService
    {
        private readonly DataContext _db;
        public TurnosAppService(DataContext db) { _db = db; }

        public async Task<(bool ok, string msg)> Crear(TurnoInput input, int usuarioId)
        {
            // Si no hay Cliente_ID, buscar el cliente vinculado al usuario
            if (input.Cliente_ID == null)
            {
                var cli = await _db.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.Usuario_ID == usuarioId);
                input.Cliente_ID = cli?.Cliente_ID;
            }

            var e = await Validar(input, null);
            if (e != null) return (false, e);

            var turno = new Turnos
            {
                HoraInicio = input.HoraInicio!.Value,
                HoraFin = input.HoraFin!.Value,
                Estado = "Confirmado",
                Usuario_ID = usuarioId,
                Cancha_ID = input.Cancha_ID!.Value,
                Cliente_ID = input.Cliente_ID
            };
            _db.Turnos.Add(turno);
            await _db.SaveChangesAsync();

            foreach (var c in ExpandConsumos(input))
            {
                _db.TurnoProductos.Add(new TurnoProducto
                {
                    Turno_ID = turno.Turno_ID,
                    Producto_ID = c.Producto_ID,
                    Cantidad = c.Cantidad
                });
            }
            if (_db.ChangeTracker.HasChanges())
                await _db.SaveChangesAsync();

            return (true, "Turno creado");
        }

        public async Task<(bool ok, string msg)> Actualizar(int id, TurnoInput input)
        {
            var e = await Validar(input, id);
            if (e != null) return (false, e);

            var turno = await _db.Turnos.FindAsync(id);
            if (turno == null) return (false, "Turno no encontrado");
            turno.HoraInicio = input.HoraInicio!.Value;
            turno.HoraFin = input.HoraFin!.Value;
            turno.Cancha_ID = input.Cancha_ID!.Value;
            turno.Cliente_ID = input.Cliente_ID;
            await _db.SaveChangesAsync();

            // Agregar nuevos consumos (no edita existentes)
            foreach (var c in ExpandConsumos(input))
            {
                _db.TurnoProductos.Add(new TurnoProducto
                {
                    Turno_ID = turno.Turno_ID,
                    Producto_ID = c.Producto_ID,
                    Cantidad = c.Cantidad
                });
            }
            if (_db.ChangeTracker.HasChanges())
                await _db.SaveChangesAsync();

            return (true, "Turno actualizado");
        }

        public async Task<(bool ok, string msg)> Eliminar(int id)
        {
            var turno = await _db.Turnos.FindAsync(id);
            if (turno == null) return (false, "Turno no encontrado");

            var consumos = await _db.TurnoProductos.Where(tp => tp.Turno_ID == id).ToListAsync();
            if (consumos.Count > 0) _db.TurnoProductos.RemoveRange(consumos);

            _db.Turnos.Remove(turno);
            await _db.SaveChangesAsync();
            return (true, "Turno eliminado");
        }

        public Task<List<Turnos>> Listar() => _db.Turnos.AsNoTracking().OrderBy(t => t.HoraInicio).ToListAsync();
        public Task<Turnos?> Obtener(int id) => _db.Turnos.FindAsync(id).AsTask();

        private async Task<string?> Validar(TurnoInput input, int? turnoId)
        {
            if (input.Cancha_ID == null || input.Cancha_ID <= 0)
                return "Seleccione una cancha";
            // Para clientes, Cliente_ID puede venir null; tras la resolución previa debería venir seteado
            if (input.Cliente_ID == null || input.Cliente_ID <= 0)
                return "No se pudo identificar el cliente asociado";
            if (input.HoraInicio == null || input.HoraFin == null)
                return "Debe ingresar inicio y fin";
            if (input.HoraFin <= input.HoraInicio)
                return "La hora fin debe ser mayor a la hora inicio";

            var hi = input.HoraInicio.Value;
            var hf = input.HoraFin.Value;
            bool overlap = await _db.Turnos
                .AnyAsync(t => t.Cancha_ID == input.Cancha_ID.Value
                            && (turnoId == null || t.Turno_ID != turnoId.Value)
                            && hi < t.HoraFin && hf > t.HoraInicio);
            if (overlap) return "La cancha ya está ocupada en ese horario";

            // Validar consumos (múltiples o único)
            var consumos = ExpandConsumos(input).ToList();
            foreach (var c in consumos)
            {
                if (c.Producto_ID <= 0) return "Seleccione un producto válido";
                if (c.Cantidad <= 0) return "Ingrese una cantidad válida";
                var exists = await _db.Productos.AnyAsync(p => p.Producto_ID == c.Producto_ID);
                if (!exists) return "El producto seleccionado no existe";
            }

            return null;
        }

        private IEnumerable<ConsumoInput> ExpandConsumos(TurnoInput input)
        {
            var list = new List<ConsumoInput>();
            if (input.Consumos != null) list.AddRange(input.Consumos.Where(c => c != null));
            if (input.Producto_ID.HasValue && input.Cantidad.HasValue)
            {
                list.Add(new ConsumoInput { Producto_ID = input.Producto_ID.Value, Cantidad = input.Cantidad.Value });
            }
            // Consolidar por producto si se repite (opcional)
            var grouped = list
                .Where(c => c.Producto_ID > 0 && c.Cantidad > 0)
                .GroupBy(c => c.Producto_ID)
                .Select(g => new ConsumoInput { Producto_ID = g.Key, Cantidad = g.Sum(x => x.Cantidad) });
            return grouped;
        }
    }
}
