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

namespace Negocio.Repositorys
{
    public class ConsumicionesRepository : IConsumicionesRepository
    {
        private readonly ProyectoDbContext _context;

        public ConsumicionesRepository(ProyectoDbContext context)
        {
            _context = context;
        }
        // Crear una nueva consumición
        public async Task CrearConsumicion(ConsumicionDTO consumicion)
        {
            ArgumentNullException.ThrowIfNull(consumicion);

            var nuevaConsumicion = new Consumiciones
            {
                // Asigna aquí las propiedades según tu DTO y entidad
                // Ejemplo:
                // Nombre = consumicion.Nombre,
                // Precio = consumicion.Precio,
                // etc.
            };

            _context.Consumiciones.Add(nuevaConsumicion);
            await _context.SaveChangesAsync();
        }
        /* public static async Task CreateConsumicion(ConsumicionDTO consumicionDto)
         {
             ArgumentNullException.ThrowIfNull(consumicionDto);

             var client = ApiServer.ObtenerClientHttp();
             var url = ApiServer.ObtenerUrlEndPoint("/api/consumiciones");
             var content = new StringContent(JsonConvert.SerializeObject(consumicionDto), Encoding.UTF8, "application/json");

             var response = await client.PostAsync(url, content);
             response.EnsureSuccessStatusCode();
         }
        */

        // Actualizar una consumición existente
        public async Task ModificarConsumicion(int consumicionID, ConsumicionDTO consumicionModificada)
        {
            ArgumentNullException.ThrowIfNull(consumicionModificada);

            var consumicion = await _context.Consumiciones.FindAsync(consumicionID);
            if (consumicion == null)
                throw new KeyNotFoundException("Consumición no encontrada.");

            // Actualiza las propiedades
            // consumicion.Nombre = consumicionModificada.Nombre;
            // consumicion.Precio = consumicionModificada.Precio;
            // etc.

            await _context.SaveChangesAsync();
        }

        /* public static async Task UpdateConsumicion(int consumicionID, ConsumicionDTO consumicionModificadaDto)
         {
             ArgumentNullException.ThrowIfNull(consumicionModificadaDto);

             var client = ApiServer.ObtenerClientHttp();
             var url = ApiServer.ObtenerUrlEndPoint($"/api/consumiciones/{consumicionID}");
             var content = new StringContent(JsonConvert.SerializeObject(consumicionModificadaDto), Encoding.UTF8, "application/json");

             var response = await client.PutAsync(url, content);
             response.EnsureSuccessStatusCode();
         }
        */
        // Eliminar una consumición
        public async Task EliminarConsumicion(int consumicionID)
        {
            var consumicion = await _context.Consumiciones.FindAsync(consumicionID);
            if (consumicion == null)
                throw new KeyNotFoundException("Consumición no encontrada.");

            _context.Consumiciones.Remove(consumicion);
            await _context.SaveChangesAsync();
        }
        /* public static async Task DeleteConsumicion(int consumicionID)
         {
             var client = ApiServer.ObtenerClientHttp();
             var url = ApiServer.ObtenerUrlEndPoint($"/api/consumiciones/{consumicionID}");

             var response = await client.DeleteAsync(url);
             response.EnsureSuccessStatusCode();
         }
        */

        // Obtener todas las consumiciones
        public async Task<List<ConsumicionDTO>> ObtenerTodasLasConsumiciones()
        {
            return await _context.Consumiciones
                .Select(c => new ConsumicionDTO
                {
                    // Consumicion_ID = c.Consumicion_ID,
                    // Nombre = c.Nombre,
                    // Precio = c.Precio,
                    // etc.
                })
                .ToListAsync();
        }
        /* public static async Task<List<ConsumicionDTO>> GetAllConsumiciones()
         {
             var client = ApiServer.ObtenerClientHttp();
             var url = ApiServer.ObtenerUrlEndPoint("/api/consumiciones");

             var response = await client.GetAsync(url);
             response.EnsureSuccessStatusCode();

             var result = await response.Content.ReadAsStringAsync();
             return JsonConvert.DeserializeObject<List<ConsumicionDTO>>(result);
         }
         */
        // Obtener una consumición por su ID
        public async Task<ConsumicionDTO?> ObtenerConsumicionPorId(int id)
        {
            var consumicion = await _context.Consumiciones.FindAsync(id);
            if (consumicion == null) return null;

            return new ConsumicionDTO
            {
                // Consumicion_ID = consumicion.Consumicion_ID,
                // Nombre = consumicion.Nombre,
                // Precio = consumicion.Precio,
                // etc.
            };
        }
        /* public static async Task<ConsumicionDTO?> GetConsumicionById(int id)
        {
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint($"/api/consumiciones/{id}");

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ConsumicionDTO>(result);
        }
        */
    }

}
