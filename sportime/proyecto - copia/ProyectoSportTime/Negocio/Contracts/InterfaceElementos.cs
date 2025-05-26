using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Contracts
{
    public interface InterfaceElementos
    {
        void AltaElemento(string nombre, int cantidad, int canchaID);
        void ModificarElemento(int elementoID, string nuevoNombre, int nuevaCantidad);
        void BajaElemento(int elementoID);
    }
}
