using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos;

namespace CNegocio.Contracts
{
    public interface ICanchas
    {
        Task<List<CanchaDTO>> ObtenerTodas();
        Task<CanchaDTO?> ObtenerPorId(int id);
        Task<List<CanchaDTO>> ObtenerPorDeporte(int deporteId);
        Task<List<CanchaDTO>> ObtenerActivas();
        Task<CanchaDTO> CrearCancha(CanchaDTO dto);
        Task<CanchaDTO> ModificarCancha(CanchaDTO dto);
        Task EliminarCancha(int id);
        Task<bool> CanchaExiste(int id);

    }
}
