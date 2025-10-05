using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDatos.Repositorys.IRepositorys
{
    public interface IProveedoresRepository
    {
        Task<Proveedores> CrearProveedor(Proveedores proveedor);
        Task<Proveedores> ModificarProveedor(Proveedores proveedor);
        void EliminarProveedor(int proveedorid);
        Task<List<Proveedores>> ObtenerTodosLosProveedores();
        Task<Proveedores?> ObtenerProveedorPorId(int proveedorid);

    }
}
