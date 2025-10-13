using CDatos.Data;
using CDatos.Repositorys.IRepositorys;
using Microsoft.EntityFrameworkCore;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDatos.Repositorys
{
    public class TurnosRepository : ITurnosRepository
    {
        private readonly DataContext _context;
        public TurnosRepository(DataContext context)
        {
            _context = context;
        }
        // Crear un nuevo turno
        public async Task<Turnos> CrearTurno(Turnos turno)
        {
            _context.Turnos.Add(turno);
            await _context.SaveChangesAsync();
            return turno;
        }
        // Modificar un turno existente
        public async Task<Turnos> ModificarTurno(Turnos turnoModificado)
        {
            ArgumentNullException.ThrowIfNull(turnoModificado);
            var turnoExistente = _context.Turnos.Find(turnoModificado.Turno_ID);
            if (turnoExistente == null)
            {
                throw new Exception("Turno no encontrado.");
            }
            turnoExistente.HoraInicio = turnoModificado.HoraInicio;
            turnoExistente.HoraFin = turnoModificado.HoraFin;
            turnoExistente.Estado = turnoModificado.Estado;
            turnoExistente.Usuario_ID = turnoModificado.Usuario_ID;
            turnoExistente.Cancha_ID = turnoModificado.Cancha_ID;
            await _context.SaveChangesAsync();
            return turnoExistente;
        }

        //Eliminar un turno
        public void EliminarTurno(int turnoID)
        {
            var turno = _context.Turnos.FirstOrDefault(x => x.Turno_ID == turnoID);
            if (turno != null)
            {
                _context.Turnos.Remove(turno);
                _context.SaveChanges();
            }
        }
        // Obtener todos los turnos
        public async Task<List<Turnos>> ObtenerTodosLosTurnos()
        {
            return await _context.Turnos.ToListAsync();
        }
        
        // Obtener turnos por cancha
        public async Task<List<Turnos>> ObtenerTurnosPorCancha(int canchaID)
        {
            return await _context.Turnos
                                 .Where(t => t.Cancha_ID == canchaID)
                                 .ToListAsync();
        }
        // Obtener turnos por usuario
        public async Task<List<Turnos>> ObtenerTurnosPorUsuario(int usuarioID)
        {
            return await _context.Turnos
                                 .Where(t => t.Usuario_ID == usuarioID)
                                 .ToListAsync();
        }
        // Obtener un turno por ID
        public async Task<Turnos?> ObtenerTurnoPorId(int TurnoID)
        {
            return await _context.Turnos.FindAsync(TurnoID);
        }
        // Verifica si hay un turno superpuesto (duplicado) para la misma cancha y rango horario
        public async Task<bool> TurnoDuplicado(int canchaId, DateTime horaInicio, DateTime horaFin, int? turnoIdExcluir = null)
        {
            return await _context.Turnos.AnyAsync(t =>
                t.Cancha_ID == canchaId &&
                (turnoIdExcluir == null || t.Turno_ID != turnoIdExcluir) &&
                (horaInicio < t.HoraFin && horaFin > t.HoraInicio)
            );
        }
        // Verifica si la cancha está activa (disponible para reservas)
        public async Task<bool> CanchaActiva(int canchaId)
        {
            var cancha = await _context.Canchas.FindAsync(canchaId);
            return cancha != null && cancha.Activa;
        }

    }
}
