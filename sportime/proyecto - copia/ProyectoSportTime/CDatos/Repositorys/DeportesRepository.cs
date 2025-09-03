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

namespace CDatos.Repositorys
{
    public class DeportesRepository : IDeportesRepository
    {
        private readonly ProyectoDbContext _context;

        public DeportesRepository(ProyectoDbContext context)
        {
            _context = context;
        }
        // Crear un nuevo deporte
        public async Task CrearDeporte(DeporteDTO deporte)
        {
            ArgumentNullException.ThrowIfNull(deporte);

            var nuevoDeporte = new Deportes
            {
                Tipo = deporte.Tipo
            };

            _context.Deportes.Add(nuevoDeporte);
            await _context.SaveChangesAsync();
        }
        /*public static async Task CreateDeporte(DeporteDTO deporteDto)
        {
            ArgumentNullException.ThrowIfNull(deporteDto);

            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint("/api/deportes");
            var content = new StringContent(JsonConvert.SerializeObject(deporteDto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
        }
        */

        // Actualizar un deporte existente
        public async Task ModificarDeporte(int deporteID, DeporteDTO deporteModificado)
        {
            ArgumentNullException.ThrowIfNull(deporteModificado);

            var deporte = await _context.Deportes.FindAsync(deporteID);
            if (deporte == null)
                throw new KeyNotFoundException("Deporte no encontrado.");

            deporte.Tipo = deporteModificado.Tipo;
            await _context.SaveChangesAsync();
        }
        /* public static async Task UpdateDeporte(int deporteID, DeporteDTO deporteModificadoDto)
         {
             ArgumentNullException.ThrowIfNull(deporteModificadoDto);

             var client = ApiServer.ObtenerClientHttp();
             var url = ApiServer.ObtenerUrlEndPoint($"/api/deportes/{deporteID}");
             var content = new StringContent(JsonConvert.SerializeObject(deporteModificadoDto), Encoding.UTF8, "application/json");

             var response = await client.PutAsync(url, content);
             response.EnsureSuccessStatusCode();
         }
        */

        // Eliminar un deporte
        public async Task EliminarDeporte(int deporteID)
        {
            var deporte = await _context.Deportes.FindAsync(deporteID);
            if (deporte == null)
                throw new KeyNotFoundException("Deporte no encontrado.");

            _context.Deportes.Remove(deporte);
            await _context.SaveChangesAsync();
        }

        /* public static async Task DeleteDeporte(int deporteID)
         {
             var client = ApiServer.ObtenerClientHttp();
             var url = ApiServer.ObtenerUrlEndPoint($"/api/deportes/{deporteID}");

             var response = await client.DeleteAsync(url);
             response.EnsureSuccessStatusCode();
         }
        */

        // Obtener todos los deportes
        public async Task<List<DeporteDTO>> ObtenerTodosLosDeportes()
        {
            return await _context.Deportes
                .Select(d => new DeporteDTO
                {
                    Deporte_ID = d.Deporte_ID,
                    Tipo = d.Tipo
                })
                .ToListAsync();
        }
        /* public static async Task<List<DeporteDTO>> GetAllDeportes()
        {
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint("/api/deportes");

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<DeporteDTO>>(result);
        }
        */
        // Obtener un deporte por su ID
        public async Task<DeporteDTO?> ObtenerDeportePorId(int id)
        {
            var deporte = await _context.Deportes.FindAsync(id);
            if (deporte == null) return null;

            return new DeporteDTO
            {
                Deporte_ID = deporte.Deporte_ID,
                Tipo = deporte.Tipo
            };
        }
    }
    /* public static async Task<DeporteDTO?> GetDeporteById(int id)
    {
        var client = ApiServer.ObtenerClientHttp();
        var url = ApiServer.ObtenerUrlEndPoint($"/api/deportes/{id}");

        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<DeporteDTO>(result);
    }
    */
}


