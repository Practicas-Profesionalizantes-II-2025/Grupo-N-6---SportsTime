using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNegocio.Contracts
{
    public interface ITurnos
    {
        Task<(bool EsExitoso, string MensajeError)> CrearTurno(TurnoDTO turno);
        Task ModificarTurno(int turnoID, TurnoDTO turnoModificar);
        Task BorrarTurno(int turnoID);
        Task<List<TurnoDTO>> ObtenerTodosLosTurnos();
        Task<TurnoDTO?> ObtenerTurnoPorId(int id);
        Task<List<TurnoDTO>> ObtenerTurnosPorCancha(int canchaID, DateTime horaInicio, DateTime horaFin);

    }
}
