using System;
using Shared.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Entidades;
using Negocio.ClienteHttp;
using Negocio.Helper;
using Newtonsoft.Json;

namespace Negocio.Repositorys
{
    public class TurnosRepository
    {
        public static async Task CreateTurno(TurnoDTO turno)
        {
            ArgumentNullException.ThrowIfNull(turno);

            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint("/api/turnos");
            var content = new StringContent(JsonConvert.SerializeObject(turno), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
        }

        public static async Task<List<TurnoDTO>> GetTurnosPorCancha(int canchaID, DateTime horaInicio, DateTime horaFin)
        {
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint($"/api/turnos/cancha/{canchaID}?inicio={horaInicio:o}&fin={horaFin:o}");

            var response = await client.GetAsync(url);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
               
            }
          

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<TurnoDTO>>(result) ?? new List<TurnoDTO>();
        }

        public static async Task UpdateTurno(int turnoID, TurnoDTO turnoModificar)
        {
            ArgumentNullException.ThrowIfNull(turnoModificar);

            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint($"/api/turnos/{turnoID}");
            var content = new StringContent(JsonConvert.SerializeObject(turnoModificar), Encoding.UTF8, "application/json");

            var response = await client.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
        }

        public static async Task DeleteTurno(int turnoID)
        {
            var client = ApiServer.ObtenerClientHttp();
            //var url = ApiServer.ObtenerUrlEndPoint($"/api/turnos/{turnoID}");

            //var response = await client.DeleteAsync(url);
            //response.EnsureSuccessStatusCode();

            var url = ApiServer.ObtenerUrlEndPoint($"/api/turnos/{turnoID}");
            Console.WriteLine($"URL para eliminar: {url}");
            var response = await client.DeleteAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var errorDetails = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error al eliminar el turno. Código de estado: {response.StatusCode}\nDetalles: {errorDetails}");
            }
            else
            {
                response.EnsureSuccessStatusCode();
            }
        }

        public static async Task<List<TurnoDTO>> GetAllTurnos()
        {
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint("/api/turnos");

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<TurnoDTO>>(result);
        }

        public static async Task<TurnoDTO?> GetTurnoById(int id)
        {
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint($"/api/turnos/{id}");

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TurnoDTO>(result);
        }
    }

}
