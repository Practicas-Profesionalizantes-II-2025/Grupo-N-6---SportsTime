using Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CNegocio.Contracts
{
    public interface ICanchas
    {
        Task<CanchaDTO> CrearCancha(CanchaDTO nuevaCancha);
        Task<CanchaDTO> ModificarCancha(CanchaDTO canchaModificada);
        Task BajaCancha(int canchaID);
        Task<List<CanchaDTO>> ObtenerTodasLasCanchas();
        Task<CanchaDTO?> ObtenerCanchaPorId(int id);
        Task<List<CanchaDTO>> ObtenerCanchasPorDeporteId(int deporteId);
    }
}
