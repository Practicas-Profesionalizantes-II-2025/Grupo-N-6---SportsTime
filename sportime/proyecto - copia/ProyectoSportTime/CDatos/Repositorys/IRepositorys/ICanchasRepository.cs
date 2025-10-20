using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDatos.Repositorys.IRepositorys
{
    public interface ICanchasRepository
    {
        Task<List<Canchas>> ObtenerTodasLasCanchas();
        Task<Canchas?> ObtenerCanchaPorId(int id);
        Task<List<Canchas>> ObtenerCanchasPorDeporte(int deporteId);
        Task<List<Canchas>> ObtenerCanchasActivas();
        Task<Canchas> CrearCancha(Canchas cancha);
        Task<Canchas> ModificarCancha(Canchas cancha);
        Task EliminarCancha(int canchaId);
        Task<bool> ExisteCanchaPorId(int canchaId);
        Task<bool> ExisteCanchaConMismoDeporte(int deporteId); // util para reglas si se decide
    }
}
