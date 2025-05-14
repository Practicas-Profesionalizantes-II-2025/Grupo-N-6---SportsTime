using Microsoft.EntityFrameworkCore;
using Negocio.Repositorys;
using Negocio.Contracts;
using API.Data;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos;

namespace Negocio.Implementations
{
    public class TurnosLogic
    {
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

            //var turnosExistentes = await TurnosRepository.GetTurnosPorCancha(turno.Cancha_ID,turno.HoraInicio, turno.HoraFin);


            //if (turnosExistentes.Any())
            //    return (false, "La cancha ya tiene un turno en ese horario.");

            await TurnosRepository.CreateTurno(turno);
            return (true, "Turno creado correctamente.");
        }

        public async Task ModificarTurno(int turnoID, TurnoDTO turnoModificar)
        {
            ArgumentNullException.ThrowIfNull(turnoModificar);

            if (turnoID <= 0)
                throw new ArgumentException("Id debe ser mayor a cero");

            if (turnoModificar.HoraInicio >= turnoModificar.HoraFin)
                throw new ArgumentException("Hora de inicio debe ser menor que la hora de fin");

            await TurnosRepository.UpdateTurno(turnoID, turnoModificar);  // Llamamos al servicio para modificar el turno
        }

        public async Task BorrarTurno(int turnoID)
        {
            if (turnoID <= 0)
                throw new ArgumentException("Id debe ser mayor a cero");

            await TurnosRepository.DeleteTurno(turnoID);  // Llamamos al servicio para eliminar el turno
        }

        public async Task<List<TurnoDTO>> ObtenerTodosLosTurnos()
        {
            return await TurnosRepository.GetAllTurnos();  // Llamamos al servicio para obtener los turnos
        }

        public async Task<TurnoDTO?> ObtenerTurnoPorId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id debe ser mayor a cero");

            return await TurnosRepository.GetTurnoById(id);  // Llamamos al servicio para obtener un turno por ID
        }
    }

}

