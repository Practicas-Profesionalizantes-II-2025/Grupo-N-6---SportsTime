using CDatos.Data;
using CDatos.Repositorys.IRepositorys;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace CDatos.Repositorys
{
    public class TurnosRepository : ITurnosRepository
    {
        private readonly DataContext _context;

        public TurnosRepository(DataContext context)
        {
            _context = context;
        }
        // Crear un nuevo turno
        public async Task CrearTurno(Turnos turno)
        {          
            _context.Turnos.Add(turno);
            await _context.SaveChangesAsync();
        }

        /* public static async Task CrearTurno(TurnoDTO turno)
         {
             ArgumentNullException.ThrowIfNull(turno);

             var client = ApiServer.ObtenerClientHttp();
             var url = ApiServer.ObtenerUrlEndPoint("/api/turnos");
             var content = new StringContent(JsonConvert.SerializeObject(turno), Encoding.UTF8, "application/json");

             var response = await client.PostAsync(url, content);
             response.EnsureSuccessStatusCode();
         }
        */

        //Obtener turnos por cancha y rango horario
        public async Task<List<Turnos>> ObtenerTurnosPorCancha(int canchaID)
        {
            return await _context.Turnos
                .Where(p => p.Cancha_ID == canchaID)
               .ToListAsync();

        }

        /* public static async Task<List<TurnoDTO>> GetTurnosPorCancha(int canchaID, DateTime horaInicio, DateTime horaFin)
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
        */
        //Actualizar un turno existente
        public async Task ModificarTurno(Turnos turnoModificado)
        {
           _context.Turnos.Update(turnoModificado);
              await _context.SaveChangesAsync();
        }
        /* public static async Task UpdateTurno(int turnoID, TurnoDTO turnoModificar)
         {
             ArgumentNullException.ThrowIfNull(turnoModificar);

             var client = ApiServer.ObtenerClientHttp();
             var url = ApiServer.ObtenerUrlEndPoint($"/api/turnos/{turnoID}");
             var content = new StringContent(JsonConvert.SerializeObject(turnoModificar), Encoding.UTF8, "application/json");

             var response = await client.PutAsync(url, content);
             response.EnsureSuccessStatusCode();
         }
        */

        //Eliminar un turno
        public async Task EliminarTurno(int turnoID)
        {
            var turno = await ObtenerTurnoPorId(turnoID);
            if (turno == null)
            {
                throw new Exception("Turno no encontrado.");
            }

            _context.Turnos.Remove(turno);

            await _context.SaveChangesAsync();
        }
        /*  public static async Task DeleteTurno(int turnoID)
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
        */

        //Obtener todos los turnos
        public async Task<List<Turnos>> ObtenerTodosLosTurnos()
        {
            return await _context.Turnos.ToListAsync();

        }
        /* public static async Task<List<TurnoDTO>> GetAllTurnos()
        {
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint("/api/turnos");

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<TurnoDTO>>(result);
        }
        */

        //Obtener un turno por ID
        public async Task<Turnos?> ObtenerTurnoPorId(int id)
        {
            return await _context.Turnos.FindAsync(id);

        }
        /* public static async Task<TurnoDTO?> GetTurnoById(int id)
         {
             var client = ApiServer.ObtenerClientHttp();
             var url = ApiServer.ObtenerUrlEndPoint($"/api/turnos/{id}");

             var response = await client.GetAsync(url);
             response.EnsureSuccessStatusCode();

             var result = await response.Content.ReadAsStringAsync();
             return JsonConvert.DeserializeObject<TurnoDTO>(result);
         }
        */
    }

}
