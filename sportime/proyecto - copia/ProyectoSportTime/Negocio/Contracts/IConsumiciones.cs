using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos;

namespace CNegocio.Contracts
{
    public interface IConsumiciones
    {
        Task AltaConsumicion(ConsumicionDTO nuevaConsumicion);
        Task ModificarConsumicion(int consumicionID, ConsumicionDTO consumicionModificada);
        Task BajaConsumicion(int consumicionID);
        Task<List<ConsumicionDTO>> ObtenerTodasLasConsumiciones();
        Task<ConsumicionDTO?> ObtenerConsumicionPorId(int id);

    }
}
