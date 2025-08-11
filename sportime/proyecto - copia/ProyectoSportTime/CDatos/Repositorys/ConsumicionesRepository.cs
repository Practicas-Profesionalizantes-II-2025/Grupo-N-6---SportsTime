using Negocio.ClienteHttp;
using Newtonsoft.Json;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Repositorys
{
    public class ConsumicionesRepository
    {
        // Crear una nueva consumición
        public static async Task CreateConsumicion(ConsumicionDTO consumicionDto)
        {
            ArgumentNullException.ThrowIfNull(consumicionDto);

            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint("/api/consumiciones");
            var content = new StringContent(JsonConvert.SerializeObject(consumicionDto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
        }

        // Actualizar una consumición existente
        public static async Task UpdateConsumicion(int consumicionID, ConsumicionDTO consumicionModificadaDto)
        {
            ArgumentNullException.ThrowIfNull(consumicionModificadaDto);

            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint($"/api/consumiciones/{consumicionID}");
            var content = new StringContent(JsonConvert.SerializeObject(consumicionModificadaDto), Encoding.UTF8, "application/json");

            var response = await client.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
        }

        // Eliminar una consumición
        public static async Task DeleteConsumicion(int consumicionID)
        {
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint($"/api/consumiciones/{consumicionID}");

            var response = await client.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
        }

        // Obtener todas las consumiciones
        public static async Task<List<ConsumicionDTO>> GetAllConsumiciones()
        {
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint("/api/consumiciones");

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ConsumicionDTO>>(result);
        }

        // Obtener una consumición por su ID
        public static async Task<ConsumicionDTO?> GetConsumicionById(int id)
        {
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint($"/api/consumiciones/{id}");

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ConsumicionDTO>(result);
        }
    }

}
