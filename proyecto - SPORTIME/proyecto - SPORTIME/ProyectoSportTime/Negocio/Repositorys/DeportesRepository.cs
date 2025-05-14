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
    public class DeportesRepository
    {
        // Crear un nuevo deporte
        public static async Task CreateDeporte(DeporteDTO deporteDto)
        {
            ArgumentNullException.ThrowIfNull(deporteDto);

            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint("/api/deportes");
            var content = new StringContent(JsonConvert.SerializeObject(deporteDto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
        }

        // Actualizar un deporte existente
        public static async Task UpdateDeporte(int deporteID, DeporteDTO deporteModificadoDto)
        {
            ArgumentNullException.ThrowIfNull(deporteModificadoDto);

            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint($"/api/deportes/{deporteID}");
            var content = new StringContent(JsonConvert.SerializeObject(deporteModificadoDto), Encoding.UTF8, "application/json");

            var response = await client.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
        }

        // Eliminar un deporte
        public static async Task DeleteDeporte(int deporteID)
        {
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint($"/api/deportes/{deporteID}");

            var response = await client.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
        }

        // Obtener todos los deportes
        public static async Task<List<DeporteDTO>> GetAllDeportes()
        {
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint("/api/deportes");

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<DeporteDTO>>(result);
        }

        // Obtener un deporte por su ID
        public static async Task<DeporteDTO?> GetDeporteById(int id)
        {
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint($"/api/deportes/{id}");

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DeporteDTO>(result);
        }
    }

}
