using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos;


namespace CNegocio.Contracts
{
    public interface IClientes
    {
        Task AltaCliente(ClienteDTO nuevoCliente);
        Task ModificarCliente(int clienteID, ClienteDTO clienteModificado);
        Task BajaCliente(int clienteID);
        Task<List<ClienteDTO>> ObtenerTodosLosClientes();
        Task<ClienteDTO?> ObtenerClientePorId(int id);

       /*  Task<(bool isUnique, string message)> ValidarTelefonoUnico(int clienteID, int numeroTelefono);
        */
    }
}
