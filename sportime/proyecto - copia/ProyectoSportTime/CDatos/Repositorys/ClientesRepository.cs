using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CDatos.Data;
using Shared.Entidades;
using Microsoft.EntityFrameworkCore;
using CDatos.Repositorys.IRepositorys;

namespace Negocio.Repositorys
{
    public class ClientesRepository : IClienteRepository
    {
        private readonly ProyectoDbContext _context;

        public ClientesRepository(ProyectoDbContext context)
        {
            _context = context;
        }

        // Crear un nuevo cliente
        public async Task CrearCliente(ClienteDTO cliente)
        {
            ArgumentNullException.ThrowIfNull(cliente);

            var nuevoCliente = new Clientes
            {
                Nombre = cliente.Nombre,
                NumeroTelefono = cliente.NumeroTelefono
            };

            _context.Clientes.Add(nuevoCliente);
            await _context.SaveChangesAsync();

            /*  var client = ApiServer.ObtenerClientHttp();
              var url = ApiServer.ObtenerUrlEndPoint("/api/clientes");
              var content = new StringContent(JsonConvert.SerializeObject(clienteDto), Encoding.UTF8, "application/json");

              var response = await client.PostAsync(url, content);
              response.EnsureSuccessStatusCode();
            */

        }


        /*   public static async Task<bool> ExisteClienteConNombreYTelefono(string nombre, int numeroTelefono, int clienteID = 0)
           {
               var client = ApiServer.ObtenerClientHttp();
               var url = ApiServer.ObtenerUrlEndPoint("/api/clientes");

               var response = await client.GetAsync(url);
               response.EnsureSuccessStatusCode();

               var result = await response.Content.ReadAsStringAsync();
               var clientes = JsonConvert.DeserializeObject<List<ClienteDTO>>(result);

               // Verificamos si existe un cliente con el mismo nombre y número de teléfono
               // y aseguramos que no sea el mismo cliente que está siendo modificado
               return clientes.Any(c => c.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase)
                                         && c.NumeroTelefono == numeroTelefono
                                         && c.Cliente_ID != clienteID);
           }

           public static async Task<bool> ExisteClienteConNumeroTelefono(int numeroTelefono, int clienteID = 0)
           {
               var client = ApiServer.ObtenerClientHttp();
               var url = ApiServer.ObtenerUrlEndPoint("/api/clientes");

               var response = await client.GetAsync(url);
               response.EnsureSuccessStatusCode();

               var result = await response.Content.ReadAsStringAsync();
               var clientes = JsonConvert.DeserializeObject<List<ClienteDTO>>(result);

               // Verificamos si existe un cliente con el mismo número de teléfono
               // y aseguramos que no sea el mismo cliente que está siendo modificado
               return clientes.Any(c => c.NumeroTelefono == numeroTelefono && c.Cliente_ID != clienteID);
           }
        */

        // Actualizar un cliente existente
        public async Task ModificarCliente(int clienteID, ClienteDTO clienteModificado)
        {
            ArgumentNullException.ThrowIfNull(clienteModificado);

            var cliente = await _context.Clientes.FindAsync(clienteID);
            if (cliente == null)
                throw new KeyNotFoundException("Cliente no encontrado.");

            cliente.Nombre = clienteModificado.Nombre;
            cliente.NumeroTelefono = clienteModificado.NumeroTelefono;
            await _context.SaveChangesAsync();
        }
        /*  public static async Task UpdateCliente(int clienteID, ClienteDTO clienteModificadoDto)
          {
              ArgumentNullException.ThrowIfNull(clienteModificadoDto);


              // Si pasa las validaciones, procedemos con la actualización
              var client = ApiServer.ObtenerClientHttp();
              var url = ApiServer.ObtenerUrlEndPoint($"/api/clientes/{clienteID}");
              var content = new StringContent(JsonConvert.SerializeObject(clienteModificadoDto), Encoding.UTF8, "application/json");

              var response = await client.PutAsync(url, content);
              response.EnsureSuccessStatusCode();
          }
        */
        // Eliminar un cliente
        public async Task EliminarCliente(int clienteID)
        {
            var cliente = await _context.Clientes.FindAsync(clienteID);
            if (cliente == null)
                throw new KeyNotFoundException("Cliente no encontrado.");

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }
        /* public static async Task DeleteCliente(int clienteID)
         {
             var client = ApiServer.ObtenerClientHttp();
             var url = ApiServer.ObtenerUrlEndPoint($"/api/clientes/{clienteID}");

             var response = await client.DeleteAsync(url);
             try
             {
                 response.EnsureSuccessStatusCode();
             }catch
             {
                 throw new Exception("EL cliente no se puede eliminar, tiene turnos pedidos");
             }

             response.EnsureSuccessStatusCode();
         }
        */

        // Obtener todos los clientes
        public async Task<List<ClienteDTO>> ObtenerTodosLosClientes()
        {
            return await _context.Clientes
                .Select(c => new ClienteDTO
                {
                    Cliente_ID = c.Cliente_ID,
                    Nombre = c.Nombre,
                    NumeroTelefono = c.NumeroTelefono
                })
                .ToListAsync();
        }
        /* public static async Task<List<ClienteDTO>> GetAllClientes()
         {
             var client = ApiServer.ObtenerClientHttp();
             var url = ApiServer.ObtenerUrlEndPoint("/api/clientes");

             var response = await client.GetAsync(url);
             response.EnsureSuccessStatusCode();

             var result = await response.Content.ReadAsStringAsync();
             return JsonConvert.DeserializeObject<List<ClienteDTO>>(result);
         }
        */

        // Obtener un cliente por su ID
        public async Task<ClienteDTO?> ObtenerClientePorId(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return null;

            return new ClienteDTO
            {
                Cliente_ID = cliente.Cliente_ID,
                Nombre = cliente.Nombre,
                NumeroTelefono = cliente.NumeroTelefono
            };
        }
        /*  public static async Task<ClienteDTO?> GetClienteById(int id)
          {
              var client = ApiServer.ObtenerClientHttp();
              var url = ApiServer.ObtenerUrlEndPoint($"/api/clientes/{id}");

              var response = await client.GetAsync(url);
              response.EnsureSuccessStatusCode();

              var result = await response.Content.ReadAsStringAsync();
              return JsonConvert.DeserializeObject<ClienteDTO>(result);
          }
        */
    }

}
