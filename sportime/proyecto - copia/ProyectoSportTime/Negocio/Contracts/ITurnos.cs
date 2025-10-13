using Shared.Dtos;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNegocio.Contracts
{
    public interface ITurnos
    {
        Task<TurnoDTO> CrearTurno(TurnoDTO turnoDTO);
        Task<TurnoDTO> ModificarTurno(TurnoDTO turnoDTO);
        Task EliminarTurno(int turnoID);
        Task<List<TurnoDTO>> ObtenerTodosLosTurnos();
        Task<List<TurnoDTO>> ObtenerTurnosPorCancha(int canchaID);
        Task<List<TurnoDTO>> ObtenerTurnosPorUsuario(int usuarioID);
        Task<TurnoDTO?> ObtenerTurnoPorId(int id);
        Task<bool> TurnoDuplicado(int canchaId, DateTime horaInicio, DateTime horaFin, int? turnoIdExcluir = null);
        Task<bool> CanchaActiva(int canchaId);
    }
}
