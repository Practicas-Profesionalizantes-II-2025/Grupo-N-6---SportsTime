using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNegocio.Contracts
{
    public interface IConsumicionXTurno
    {
        Task AgregarConsumicion(ConsumicionXturnoDTO consumicion);
        Task<List<ConsumicionXturnoDTO>> ObtenerConsumicionesPorTurno(int turnoId);
        Task QuitarConsumicion(int turnoID, int productoID);

    }
}
