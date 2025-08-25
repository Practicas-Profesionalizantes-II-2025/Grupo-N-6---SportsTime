using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNegocio.Contracts
{
    public interface IDeportes
    {
        Task AltaDeporte(DeporteDTO nuevoDeporte);
        Task ModificarDeporte(int deporteID, DeporteDTO deporteModificado);
        Task BajaDeporte(int deporteID);
        Task<List<DeporteDTO>> ObtenerTodosLosDeportes();
        Task<DeporteDTO?> ObtenerDeportePorId(int id);

    }
}
