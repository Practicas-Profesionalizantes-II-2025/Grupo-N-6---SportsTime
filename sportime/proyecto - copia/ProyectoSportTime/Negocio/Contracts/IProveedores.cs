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
        Task AltaProveedor(ProveedorDTO nuevoProveedor);
        Task ModificarProveedor(int proveedorID, ProveedorDTO proveedorModificado);
        Task BajaProveedor(int proveedorID);
        Task<List<ProveedorDTO>> ObtenerTodosLosProveedores();
        Task<ProveedorDTO?> ObtenerProveedorPorId(int id);

    }
}
