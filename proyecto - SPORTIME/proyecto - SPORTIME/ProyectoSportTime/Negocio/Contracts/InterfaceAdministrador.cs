using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Contracts
{
    public interface InterfaceAdministrador
    {
        void AltaAdministrador(string nombre, string email);
        void ModificarAdministrador(int adminID, string nuevoNombre, string nuevoEmail);
        void BajaAdministrador(int adminID);
    }
}
