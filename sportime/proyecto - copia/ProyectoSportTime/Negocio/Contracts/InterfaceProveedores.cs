using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Contracts
{
    public interface InterfaceProveedores
    {
        void AltaProveedor(string nombre, string email, string telefono);
        void ModificarProveedor(int proveedorID, string nuevoNombre, string nuevoEmail, string nuevoTelefono);
        void BajaProveedor(int proveedorID);
    }
}
