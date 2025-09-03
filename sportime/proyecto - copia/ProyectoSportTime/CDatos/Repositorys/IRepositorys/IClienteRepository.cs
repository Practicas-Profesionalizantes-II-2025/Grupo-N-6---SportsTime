using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos;

namespace CDatos.Repositorys.IRepositorys
{
    public interface IClienteRepository
    {
        Task CrearCliente(ClienteDTO cliente);
        Task ModificarCliente(int clienteID, ClienteDTO clienteModificado);
        Task EliminarCliente(int clienteID);
        Task<List<ClienteDTO>> ObtenerTodosLosClientes();
        Task<ClienteDTO?> ObtenerClientePorId(int id);

    }
}
