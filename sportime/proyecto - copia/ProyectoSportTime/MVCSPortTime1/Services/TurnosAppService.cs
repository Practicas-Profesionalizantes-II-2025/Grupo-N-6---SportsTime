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

    public class TurnoInput
    {
        public DateTime? HoraInicio { get; set; }
        public DateTime? HoraFin { get; set; }
        public int? Cancha_ID { get; set; }
    }

    public class TurnosAppService : ITurnosAppService
    {
        private readonly DataContext _db;
        public TurnosAppService(DataContext db) { _db = db; }

        public async Task<(bool ok, string msg)> Crear(TurnoInput input, int usuarioId)
        {
            var e = await Validar(input, null);
            if (e != null) return (false, e);

            var turno = new Turnos
            {
                HoraInicio = input.HoraInicio!.Value,
                HoraFin = input.HoraFin!.Value,
                Estado = "Confirmado",
                Usuario_ID = usuarioId,
                Cancha_ID = input.Cancha_ID!.Value
            };
            _db.Turnos.Add(turno);
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
            await _db.SaveChangesAsync();
            return (true, "Turno actualizado");
        }

        public async Task<(bool ok, string msg)> Eliminar(int id)
        {
            var turno = await _db.Turnos.FindAsync(id);
            if (turno == null) return (false, "Turno no encontrado");
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
            if (input.HoraInicio == null || input.HoraFin == null)
                return "Debe ingresar inicio y fin";
            if (input.HoraFin <= input.HoraInicio)
                return "La hora fin debe ser mayor a la hora inicio";

            // no solapar con otros turnos de la misma cancha
            var hi = input.HoraInicio.Value;
            var hf = input.HoraFin.Value;
            bool overlap = await _db.Turnos
                .AnyAsync(t => t.Cancha_ID == input.Cancha_ID.Value
                            && (turnoId == null || t.Turno_ID != turnoId.Value)
                            && hi < t.HoraFin && hf > t.HoraInicio);
            if (overlap) return "La cancha ya está ocupada en ese horario";

            return null;
        }
    }
}
