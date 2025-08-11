using Negocio.ClienteHttp;
using Newtonsoft.Json;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Repositorys
{
    public class ClientesRepository
    {
        // Crear un nuevo cliente
        public static async Task CreateCliente(ClienteDTO clienteDto)
        {
            ArgumentNullException.ThrowIfNull(clienteDto);

            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint("/api/clientes");
            var content = new StringContent(JsonConvert.SerializeObject(clienteDto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

        }


        public static async Task<bool> ExisteClienteConNombreYTelefono(string nombre, int numeroTelefono, int clienteID = 0)
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


        // Actualizar un cliente existente
        public static async Task UpdateCliente(int clienteID, ClienteDTO clienteModificadoDto)
        {
            ArgumentNullException.ThrowIfNull(clienteModificadoDto);


            // Si pasa las validaciones, procedemos con la actualización
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint($"/api/clientes/{clienteID}");
            var content = new StringContent(JsonConvert.SerializeObject(clienteModificadoDto), Encoding.UTF8, "application/json");

            var response = await client.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
        }

        // Eliminar un cliente
        public static async Task DeleteCliente(int clienteID)
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

        // Obtener todos los clientes
        public static async Task<List<ClienteDTO>> GetAllClientes()
        {
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint("/api/clientes");

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ClienteDTO>>(result);
        }

        // Obtener un cliente por su ID
        public static async Task<ClienteDTO?> GetClienteById(int id)
        {
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint($"/api/clientes/{id}");

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ClienteDTO>(result);
        }
    }

}
