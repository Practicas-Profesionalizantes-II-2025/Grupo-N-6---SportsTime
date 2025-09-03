using CDatos.Repositorys.IRepositorys;
using Microsoft.EntityFrameworkCore;
using CNegocio.Contracts;
using Shared.Dtos;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<(bool EsExitoso, string MensajeError)> CrearTurno(TurnoDTO turno)
        {
            if (turno == null)
                return (false, "El turno no puede ser nulo.");

            if (turno.Cancha_ID <= 0)
                return (false, "Seleccione una cancha válida.");

            if (turno.Cliente_ID <= 0)
                return (false, "Seleccione un cliente válido.");

            if (turno.HoraInicio >= turno.HoraFin)
                return (false, "La hora de inicio debe ser menor que la hora de fin.");

            // Validación de solapamiento de turnos (si implementas GetTurnosPorCancha)
             var turnosExistentes = await _repo.ObtenerTurnosPorCancha(turno.Cancha_ID, turno.HoraInicio, turno.HoraFin);
             if (turnosExistentes.Any())
                return (false, "La cancha ya tiene un turno en ese horario.");

            await _repo.CrearTurno(turno);
            return (true, "Turno creado correctamente.");
        }
        /* public async Task<(bool EsExitoso, string MensajeError)> CrearTurno(TurnoDTO turno)
         {
             if (turno == null)
                 return (false, "El turno no puede ser nulo.");

             if (turno.Cancha_ID <= 0)
                 return (false, "Seleccione una cancha válida.");

             if (turno.Cliente_ID <= 0)
                 return (false, "Seleccione un cliente válido.");

             if (turno.HoraInicio >= turno.HoraFin)
                 return (false, "La hora de inicio debe ser menor que la hora de fin.");

             //var turnosExistentes = await TurnosRepository.GetTurnosPorCancha(turno.Cancha_ID,turno.HoraInicio, turno.HoraFin);


             //if (turnosExistentes.Any())
             //    return (false, "La cancha ya tiene un turno en ese horario.");

             await TurnosRepository.CreateTurno(turno);
             return (true, "Turno creado correctamente.");
         }
         */

        // Modificar un turno existente
        public async Task ModificarTurno(int turnoID, TurnoDTO turnoModificar)
        {
            ArgumentNullException.ThrowIfNull(turnoModificar);

            if (turnoID <= 0)
                throw new ArgumentException("Id debe ser mayor a cero");

            if (turnoModificar.HoraInicio >= turnoModificar.HoraFin)
                throw new ArgumentException("Hora de inicio debe ser menor que la hora de fin");

            await _repo.ModificarTurno(turnoID, turnoModificar);
        }
        /* public async Task ModificarTurno(int turnoID, TurnoDTO turnoModificar)
         {
             ArgumentNullException.ThrowIfNull(turnoModificar);

             if (turnoID <= 0)
                 throw new ArgumentException("Id debe ser mayor a cero");

             if (turnoModificar.HoraInicio >= turnoModificar.HoraFin)
                 throw new ArgumentException("Hora de inicio debe ser menor que la hora de fin");

             await TurnosRepository.UpdateTurno(turnoID, turnoModificar);  // Llamamos al servicio para modificar el turno
         }
        */

        // Baja de un turno
        public async Task BorrarTurno(int turnoID)
        {
            if (turnoID <= 0)
                throw new ArgumentException("Id debe ser mayor a cero");
            await _repo.EliminarTurno(turnoID);  
        }
        /*public async Task BorrarTurno(int turnoID)
        {
            if (turnoID <= 0)
                throw new ArgumentException("Id debe ser mayor a cero");

            await TurnosRepository.DeleteTurno(turnoID);  // Llamamos al servicio para eliminar el turno
        }
        */

        //Obtener todos los turnos
        public async Task<List<TurnoDTO>> ObtenerTodosLosTurnos()
        {
            return await _repo.ObtenerTodosLosTurnos();
        }
        /* public async Task<List<TurnoDTO>> ObtenerTodosLosTurnos()
        {
            return await TurnosRepository.GetAllTurnos();  // Llamamos al servicio para obtener los turnos
        }
        */

        // obtener turnos por id
        public async Task<TurnoDTO?> ObtenerTurnoPorId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id debe ser mayor a cero");

            return await _repo.ObtenerTurnoPorId(id); 
        }

        public async Task<List<TurnoDTO>> ObtenerTurnosPorCancha(int canchaID, DateTime horaInicio, DateTime horaFin)
        {
            if (canchaID <= 0)
                throw new ArgumentException("Cancha_ID debe ser mayor a cero.");
            if (horaInicio >= horaFin)
                throw new ArgumentException("La hora de inicio debe ser menor que la hora de fin.");

            return await _repo.ObtenerTurnosPorCancha(canchaID, horaInicio, horaFin);
        }
    }

}

