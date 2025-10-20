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
        Task<List<DeporteDTO>> ObtenerTodos();
        Task<DeporteDTO?> ObtenerPorId(int id);
        Task<DeporteDTO> Crear(DeporteDTO dto);
        Task<DeporteDTO> Modificar(DeporteDTO dto);
        Task Eliminar(int id);

    }
}
