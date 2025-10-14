using Shared.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDatos.Repositorys.IRepositorys
{
    public interface IDeportesRepository
    {
        Task<Deportes> CrearDeporte(Deportes deporte);
        Task<Deportes> ModificarDeporte(Deportes deporte);
        void EliminarDeporte(int deporteId);
        Task<List<Deportes>> ObtenerTodosLosDeportes();
        Task<Deportes?> ObtenerDeportePorId(int deporteId);
    }
}
