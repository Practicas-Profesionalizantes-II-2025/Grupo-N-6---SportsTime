using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDatos.Repositorys.IRepositorys
{
    public interface IConsumicionesRepository
    {
        Task CrearConsumicion(ConsumicionDTO consumicion);
        Task ModificarConsumicion(int consumicionID, ConsumicionDTO consumicionModificada);
        Task EliminarConsumicion(int consumicionID);
        Task<List<ConsumicionDTO>> ObtenerTodasLasConsumiciones();
        Task<ConsumicionDTO?> ObtenerConsumicionPorId(int id);
    }
}
