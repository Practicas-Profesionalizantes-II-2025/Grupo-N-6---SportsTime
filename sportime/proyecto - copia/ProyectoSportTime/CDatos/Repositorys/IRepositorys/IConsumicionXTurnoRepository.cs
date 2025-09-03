using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos;

namespace CDatos.Repositorys.IRepositorys
{
    public interface IConsumicionXTurnoRepository
    {
        Task AgregarConsumicion(ConsumicionXturnoDTO consumicion);
        Task<List<ConsumicionXturnoDTO>> ObtenerConsumicionesPorTurno(int turnoId);
        Task QuitarConsumicion(int turnoID, int productoID); // 🔹 Cambio aquí
    }
}
