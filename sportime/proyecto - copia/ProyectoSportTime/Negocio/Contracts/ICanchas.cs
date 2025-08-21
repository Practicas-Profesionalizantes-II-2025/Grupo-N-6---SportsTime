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
        Task AltaCancha(CanchaDTO nuevaCancha);
        Task ModificarCancha(int canchaID, CanchaDTO canchaModificada);
        Task BajaCancha(int canchaID);
        Task<List<CanchaDTO>> ObtenerTodasLasCanchas();
        Task<CanchaDTO?> ObtenerCanchaPorId(int id);

    }
}
