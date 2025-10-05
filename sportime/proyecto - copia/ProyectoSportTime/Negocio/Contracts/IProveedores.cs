using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos;

namespace CNegocio.Contracts
{
    public interface IProveedores
    {
        Task<ProveedorDTO> CrearProveedor(ProveedorDTO proveedorDTO);
        Task<ProveedorDTO> ModificarProveedor(ProveedorDTO proveedorDTO);
        Task BajaProveedor(int ProveedorID);
        Task<List<ProveedorDTO>> ObtenerTodosLosProveedores();
        Task<ProveedorDTO?> ObtenerProveedorPorId(int ProveedorID);

    }
}
