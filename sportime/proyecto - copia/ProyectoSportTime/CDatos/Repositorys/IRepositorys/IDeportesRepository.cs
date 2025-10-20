using Shared.Dtos;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDatos.Repositorys.IRepositorys
{
    public interface IDeportesRepository
    {
        Task<List<Deportes>> ObtenerTodosLosDeportes();
        Task<Deportes?> ObtenerDeportePorId(int id);
        Task<Deportes> CrearDeporte(Deportes deporte);
        Task<Deportes> ModificarDeporte(Deportes deporte);
        Task EliminarDeporte(int id);
        Task<bool> ExisteDeporte(int id);

    }
}
