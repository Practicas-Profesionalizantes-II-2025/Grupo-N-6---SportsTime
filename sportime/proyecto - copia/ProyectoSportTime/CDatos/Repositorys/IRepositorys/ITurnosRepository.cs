using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos;

namespace CDatos.Repositorys.IRepositorys
{
    public interface ITurnosRepository
    {
        Task CrearTurno(TurnoDTO turno);
        Task ModificarTurno(int turnoID, TurnoDTO turnoModificar);
        Task EliminarTurno(int turnoID);
        Task<List<TurnoDTO>> ObtenerTodosLosTurnos();
        Task<TurnoDTO?> ObtenerTurnoPorId(int id);
        Task<List<TurnoDTO>> ObtenerTurnosPorCancha(int canchaID, DateTime horaInicio, DateTime horaFin);
    }
}
