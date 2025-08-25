using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos;

namespace CDatos.Repositorys.IRepositorys
{
    public interface IProveedoresRepository
    {
        Task CrearProveedor(ProveedorDTO proveedor);
        Task ModificarProveedor(int proveedorID, ProveedorDTO proveedorModificado);
        Task EliminarProveedor(int proveedorID);
        Task<List<ProveedorDTO>> ObtenerTodosLosProveedores();
        Task<ProveedorDTO?> ObtenerProveedorPorId(int id);

    }
}
