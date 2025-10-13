using CDatos.Repositorys.IRepositorys;
using CNegocio.Contracts;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CNegocio.Implementations
{
    public class TurnosLogic : ITurnos
    {
        private readonly ITurnosRepository _repo;

        public TurnosLogic(ITurnosRepository repo)
        {
            _repo = repo;
        }
        // Alta de un turno
        public async Task<TurnoDTO> CrearTurno(TurnoDTO turnoDTO)
        {
            ValidarTurnoDTO(turnoDTO, esNuevo: true);

            // Validar que la cancha esté activa
            if (!await _repo.CanchaActiva(turnoDTO.Cancha_ID))
                throw new InvalidOperationException("La cancha seleccionada no está activa.");

            // Validar que no haya solapamiento de horarios
            if (await _repo.TurnoDuplicado(turnoDTO.Cancha_ID, turnoDTO.HoraInicio, turnoDTO.HoraFin))
                throw new InvalidOperationException("Ya existe un turno en ese horario para la cancha seleccionada.");

            var turno = new Turnos
            {
                HoraInicio = turnoDTO.HoraInicio,
                HoraFin = turnoDTO.HoraFin,
                Estado = turnoDTO.Estado,
                Usuario_ID = turnoDTO.Usuario_ID,
                Cancha_ID = turnoDTO.Cancha_ID
            };

            await _repo.CrearTurno(turno);

            // Obtener el turno creado para devolver el DTO completo (con ID)
            var turnoCreado = await _repo.ObtenerTurnoPorId(turno.Turno_ID);

            return turnoCreado != null
                ? new TurnoDTO
                {
                    Turno_ID = turnoCreado.Turno_ID,
                    HoraInicio = turnoCreado.HoraInicio,
                    HoraFin = turnoCreado.HoraFin,
                    Estado = turnoCreado.Estado,
                    Usuario_ID = turnoCreado.Usuario_ID,
                    Cancha_ID = turnoCreado.Cancha_ID
                }
                : throw new InvalidOperationException("No se pudo obtener el turno recién creado.");
        }
        // Modificación de un turno existente
        public async Task<TurnoDTO> ModificarTurno(TurnoDTO turnoDTO)
        {
            if (turnoDTO.Turno_ID <= 0)
                throw new ArgumentException("El ID del turno no es válido.");

            var existente = await _repo.ObtenerTurnoPorId(turnoDTO.Turno_ID);
            if (existente == null)
                throw new InvalidOperationException("No se encontró el turno a actualizar.");

            ValidarTurnoDTO(turnoDTO, esNuevo: false);

            // Validar que la cancha esté activa
            if (!await _repo.CanchaActiva(turnoDTO.Cancha_ID))
                throw new InvalidOperationException("La cancha seleccionada no está activa.");

            // Validar solapamiento excluyendo este turno
            if (await _repo.TurnoDuplicado(turnoDTO.Cancha_ID, turnoDTO.HoraInicio, turnoDTO.HoraFin, turnoDTO.Turno_ID))
                throw new InvalidOperationException("Ya existe un turno en ese horario para la cancha seleccionada.");

            var turno = new Turnos
            {
                Turno_ID = turnoDTO.Turno_ID,
                HoraInicio = turnoDTO.HoraInicio,
                HoraFin = turnoDTO.HoraFin,
                Estado = turnoDTO.Estado,
                Usuario_ID = turnoDTO.Usuario_ID,
                Cancha_ID = turnoDTO.Cancha_ID
            };

            await _repo.ModificarTurno(turno);

            var turnoActualizado = await _repo.ObtenerTurnoPorId(turnoDTO.Turno_ID);

            return turnoActualizado != null
                ? new TurnoDTO
                {
                    Turno_ID = turnoActualizado.Turno_ID,
                    HoraInicio = turnoActualizado.HoraInicio,
                    HoraFin = turnoActualizado.HoraFin,
                    Estado = turnoActualizado.Estado,
                    Usuario_ID = turnoActualizado.Usuario_ID,
                    Cancha_ID = turnoActualizado.Cancha_ID
                }
                : throw new InvalidOperationException("No se pudo obtener el turno actualizado.");
        }
        // Baja de un turno
        public async Task EliminarTurno(int turnoID)
        {
            if (turnoID <= 0)
                throw new ArgumentException("El ID del turno debe ser mayor a cero.");
            var existente = await _repo.ObtenerTurnoPorId(turnoID);
            if (existente == null)
                throw new InvalidOperationException("No se encontró el turno a eliminar.");

             _repo.EliminarTurno(turnoID);
        }
        // Obtener todos los turnos
        public async Task<List<TurnoDTO>> ObtenerTodosLosTurnos()
        {
            var turnos = await _repo.ObtenerTodosLosTurnos();
            return turnos.Select(t => new TurnoDTO
            {
                Turno_ID = t.Turno_ID,
                HoraInicio = t.HoraInicio,
                HoraFin = t.HoraFin,
                Estado = t.Estado,
                Usuario_ID = t.Usuario_ID,
                Cancha_ID = t.Cancha_ID
            }).ToList();
        }
        // Obtener turnos por cancha
        public async Task<List<TurnoDTO>> ObtenerTurnosPorCancha(int canchaID)
        {
            var turnos = await _repo.ObtenerTurnosPorCancha(canchaID);
            return turnos.Select(t => new TurnoDTO
            {
                Turno_ID = t.Turno_ID,
                HoraInicio = t.HoraInicio,
                HoraFin = t.HoraFin,
                Estado = t.Estado,
                Usuario_ID = t.Usuario_ID,
                Cancha_ID = t.Cancha_ID
            }).ToList();
        }
        // Obtener turnos por usuario
        public async Task<List<TurnoDTO>> ObtenerTurnosPorUsuario(int usuarioID)
        {
            var turnos = await _repo.ObtenerTurnosPorUsuario(usuarioID);
            return turnos.Select(t => new TurnoDTO
            {
                Turno_ID = t.Turno_ID,
                HoraInicio = t.HoraInicio,
                HoraFin = t.HoraFin,
                Estado = t.Estado,
                Usuario_ID = t.Usuario_ID,
                Cancha_ID = t.Cancha_ID
            }).ToList();
        }
        // Obtener turno por ID
        public async Task<TurnoDTO?> ObtenerTurnoPorId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del turno debe ser mayor que cero.");

            var turno = await _repo.ObtenerTurnoPorId(id);
            if (turno == null)
                throw new ArgumentException($"No se encontró un turno con el ID {id}");

            return new TurnoDTO
            {
                Turno_ID = turno.Turno_ID,
                HoraInicio = turno.HoraInicio,
                HoraFin = turno.HoraFin,
                Estado = turno.Estado,
                Usuario_ID = turno.Usuario_ID,
                Cancha_ID = turno.Cancha_ID
            };
        }
        // Validación de solapamiento de horarios, para API o validaciones externas
        public async Task<bool> TurnoDuplicado(int canchaId, DateTime horaInicio, DateTime horaFin, int? turnoIdExcluir = null)
        {
            return await _repo.TurnoDuplicado(canchaId, horaInicio, horaFin, turnoIdExcluir);
        }

        // Validación de estado de cancha, para API o validaciones externas
        public async Task<bool> CanchaActiva(int canchaId)
        {
            return await _repo.CanchaActiva(canchaId);
        }

        // Validaciones internas de TurnoDTO
        private void ValidarTurnoDTO(TurnoDTO turnoDTO, bool esNuevo)
        {
            if (turnoDTO == null)
                throw new ArgumentNullException(nameof(turnoDTO), "El turno no puede ser nulo.");

            if (turnoDTO.Usuario_ID <= 0)
                throw new ArgumentException("El usuario es obligatorio.");

            if (turnoDTO.Cancha_ID <= 0)
                throw new ArgumentException("La cancha es obligatoria.");

            if (turnoDTO.HoraInicio == DateTime.MinValue)
                throw new ArgumentException("La hora de inicio es obligatoria.");

            if (turnoDTO.HoraFin == DateTime.MinValue)
                throw new ArgumentException("La hora de fin es obligatoria.");

            if (turnoDTO.HoraFin <= turnoDTO.HoraInicio)
                throw new ArgumentException("La hora de fin debe ser mayor a la hora de inicio.");

            if (string.IsNullOrWhiteSpace(turnoDTO.Estado))
                throw new ArgumentException("El estado del turno es obligatorio.");
        }
    }
}

