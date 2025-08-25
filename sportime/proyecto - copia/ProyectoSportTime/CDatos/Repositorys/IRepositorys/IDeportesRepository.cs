using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos;

namespace CDatos.Repositorys.IRepositorys
{
    public interface IDeportesRepository
    {
        Task CrearDeporte(DeporteDTO deporte);
        Task ModificarDeporte(int deporteID, DeporteDTO deporteModificado);
        Task EliminarDeporte(int deporteID);
        Task<List<DeporteDTO>> ObtenerTodosLosDeportes();
        Task<DeporteDTO?> ObtenerDeportePorId(int id);

    }
}
