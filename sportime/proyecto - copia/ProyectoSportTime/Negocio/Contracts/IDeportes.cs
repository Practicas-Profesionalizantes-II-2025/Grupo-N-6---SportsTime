using Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CNegocio.Contracts
{
    public interface IDeportes
    {
        Task<DeporteDTO> CrearDeporte(DeporteDTO deporte);
        Task<DeporteDTO> ModificarDeporte(DeporteDTO deporte);
        Task BajaDeporte(int deporteId);
        Task<List<DeporteDTO>> ObtenerTodosLosDeportes();
        Task<DeporteDTO?> ObtenerDeportePorId(int deporteId);
    }
}
