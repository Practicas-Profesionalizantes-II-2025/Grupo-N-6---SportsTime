using Shared.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDatos.Repositorys.IRepositorys
{
    public interface ICanchasRepository
    {
        Task<Canchas> CrearCancha(Canchas cancha);
        Task<Canchas> ModificarCancha(Canchas cancha);
        void EliminarCancha(int canchaId);
        Task<List<Canchas>> ObtenerTodasLasCanchas();
        Task<Canchas?> ObtenerCanchaPorId(int canchaId);
        Task<List<Canchas>> ObtenerCanchasPorDeporteId(int deporteId);
    }
}
