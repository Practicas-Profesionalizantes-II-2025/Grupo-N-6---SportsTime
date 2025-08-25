using Microsoft.EntityFrameworkCore;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio.Repositorys;
using Shared.Dtos;
using CDatos.Repositorys.IRepositorys;
using CNegocio.Contracts;


namespace CNegocio.Implementations
{
    public class ClientesLogic : IClientes
    {
        private readonly IClienteRepository _repo;

        public ClientesLogic(IClienteRepository repo)
        {
            _repo = repo;
        }

        public async Task AltaCliente(ClienteDTO nuevoCliente)
        {
            ArgumentNullException.ThrowIfNull(nuevoCliente);

            if (string.IsNullOrWhiteSpace(nuevoCliente.Nombre))
                throw new ArgumentException("El nombre no puede estar vacío.");

            if (nuevoCliente.NumeroTelefono <= 0)
                throw new ArgumentException("El número de teléfono debe ser mayor a cero.");

            await _repo.CrearCliente(nuevoCliente);
        }

        /* public async Task<(bool success, string message)> AltaCliente(ClienteDTO nuevoCliente)
         {

             ArgumentNullException.ThrowIfNull(nuevoCliente);
             ValidarCliente(nuevoCliente);

             // Validar si el número de teléfono ya está registrado
             var (isUnique, message) = await ValidarTelefonoUnico(0, nuevoCliente.NumeroTelefono); // 0 significa que no es una modificación
             if (!isUnique)
             {
                 return (false, message);
             }

             try
             {
                 await ClientesRepository.CreateCliente(nuevoCliente);
             }
             catch (Exception ex)
             {
                 return (false, "Ocurrió un error al guardar el cliente.");
             }

             return (true, "Cliente guardado correctamente.");
         }

         */

        public async Task ModificarCliente(int clienteID, ClienteDTO clienteModificado)
        {
            ArgumentNullException.ThrowIfNull(clienteModificado);

            if (clienteID <= 0)
                throw new ArgumentException("Cliente_ID debe ser mayor a cero.");

            await _repo.ModificarCliente(clienteID, clienteModificado);
        }

        /* public async Task ModificarCliente(int clienteID, ClienteDTO clienteModificado)
         {
             ArgumentNullException.ThrowIfNull(clienteModificado);
             if (clienteID <= 0) throw new ArgumentException("El ID debe ser mayor a cero.");
             ValidarCliente(clienteModificado);

             // Verificar si el número de teléfono ya existe para otro cliente
             var (isUnique, message) = await ValidarTelefonoUnico(clienteID, clienteModificado.NumeroTelefono);
             if (!isUnique)
             {
                 throw new ArgumentException(message);
             }

             await _repo.ModificarCliente(clienteID, clienteModificado);
         }
        */

        public async Task BajaCliente(int clienteID)
        {
            if (clienteID <= 0)
                throw new ArgumentException("Cliente_ID debe ser mayor a cero.");

            await _repo.EliminarCliente(clienteID);
        }

        public async Task<List<ClienteDTO>> ObtenerTodosLosClientes()
        {
            return await _repo.ObtenerTodosLosClientes();
        }

        public async Task<ClienteDTO?> ObtenerClientePorId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Cliente_ID debe ser mayor a cero.");

            return await _repo.ObtenerClientePorId(id);
        }

         /*  private void ValidarCliente(ClienteDTO cliente)
        {
            if (string.IsNullOrEmpty(cliente.Nombre))
            {
                throw new ArgumentException("El nombre no puede estar vacío.");
            }
            if (cliente.NumeroTelefono <= 0)
            {
                throw new ArgumentException("El número de teléfono debe ser válido.");
            }
        }

        public async Task<(bool success, string message)> ValidarTelefonoUnico(int clienteID, int numeroTelefono)
        {
            // Obtener todos los clientes
            var clientes = await ObtenerTodosLosClientes();

            // Verificar si ya existe otro cliente con el mismo número de teléfono
            bool existeDuplicadoTelefono = clientes.Any(c =>
                c.Cliente_ID != clienteID &&  // Ignorar el cliente actual
                c.NumeroTelefono == numeroTelefono
            );

            if (existeDuplicadoTelefono)
            {
                return (false, "Ya existe un cliente con el mismo número de teléfono.");
            }

            return (true, "Número de teléfono único.");
        }
         */
    }
}

