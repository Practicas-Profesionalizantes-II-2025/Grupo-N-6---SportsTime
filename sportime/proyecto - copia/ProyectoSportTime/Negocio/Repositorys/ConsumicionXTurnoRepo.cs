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
    public class ConsumicionXTurnoRepo
    {
        public static async Task AgregarConsumicion(ConsumicionXturnoDTO consumicion)
        {
            ArgumentNullException.ThrowIfNull(consumicion);

            try
            {
                var client = ApiServer.ObtenerClientHttp();
                var url = ApiServer.ObtenerUrlEndPoint("/api/ConsumicionXTurno");
                var content = new StringContent(JsonConvert.SerializeObject(consumicion), Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error en la API: {response.StatusCode} - {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al agregar el producto: {ex.Message}"); // 🔹 Cambio en el mensaje
            }
        }

        public static async Task<List<ConsumicionXturnoDTO>> ObtenerConsumicionesPorTurno(int turnoId)
        {
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint($"/api/ConsumicionXTurno/{turnoId}");

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ConsumicionXturnoDTO>>(result);
        }

        public static async Task QuitarConsumicion(int turnoID, int productoID) // 🔹 Cambio aquí
        {
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint($"/api/ConsumicionXTurno/{turnoID}/{productoID}"); // 🔹 Cambio aquí

            var response = await client.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
        }
    }

}
