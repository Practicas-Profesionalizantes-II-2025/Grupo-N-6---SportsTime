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
    public class CanchasRepository
    {
        // Crear una nueva cancha
        public static async Task CreateCancha(CanchaDTO cancha)
        {
            ArgumentNullException.ThrowIfNull(cancha);

            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint("/api/canchas/");
            var content = new StringContent(JsonConvert.SerializeObject(new { Deporte_ID = cancha.Deporte_ID }), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
        }


        // Actualizar una cancha existente
        public static async Task UpdateCancha(int canchaID, CanchaDTO canchaModificada)
        {
            ArgumentNullException.ThrowIfNull(canchaModificada);

            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint($"/api/canchas/{canchaID}");
            var content = new StringContent(JsonConvert.SerializeObject(new { Deporte_ID = canchaModificada.Deporte_ID }), Encoding.UTF8, "application/json");

            var response = await client.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
        }


        // Eliminar una cancha
        public static async Task DeleteCancha(int canchaID)
        {
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint($"/api/canchas/{canchaID}");

            var response = await client.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
        }

        // Obtener todas las canchas
        public static async Task<List<CanchaDTO>> GetAllCanchas()
        {
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint("/api/canchas");

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<CanchaDTO>>(result);
        }

        // Obtener una cancha por ID
        public static async Task<CanchaDTO?> GetCanchaById(int id)
        {
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint($"/api/canchas/{id}");

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CanchaDTO>(result);
        }
    }
}
