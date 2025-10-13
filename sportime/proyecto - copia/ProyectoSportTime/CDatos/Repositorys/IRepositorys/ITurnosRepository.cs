using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Entidades;
using Shared.Dtos;

namespace CDatos.Repositorys.IRepositorys
{
    public interface ITurnosRepository
    {
        Task<Turnos> CrearTurno(Turnos turno);
        Task<Turnos> ModificarTurno(Turnos turnoModificado);
        void EliminarTurno(int turnoID);
        Task<List<Turnos>> ObtenerTodosLosTurnos();
        Task<List<Turnos>> ObtenerTurnosPorCancha(int canchaID);
        Task<List<Turnos>> ObtenerTurnosPorUsuario(int usuarioID);
        Task<Turnos?> ObtenerTurnoPorId(int id);
        Task<bool> TurnoDuplicado(int canchaId, DateTime horaInicio, DateTime horaFin, int? turnoIdExcluir = null);
        Task<bool> CanchaActiva(int canchaId);

    }
}
