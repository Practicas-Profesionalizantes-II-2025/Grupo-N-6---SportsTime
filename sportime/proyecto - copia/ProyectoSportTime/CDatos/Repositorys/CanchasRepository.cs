using CDatos.Data;
using Shared.Dtos;
using Shared.Entidades;
using Microsoft.EntityFrameworkCore;
using CDatos.Repositorys.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Repositorys
{
    public class CanchasRepository : ICanchasRepository
    {
        private readonly ProyectoDbContext _context;

        public CanchasRepository(ProyectoDbContext context)
        {
            _context = context;
        }
        // Crear una nueva cancha
        public async Task CreateCancha(CanchaDTO cancha)
        {
            ArgumentNullException.ThrowIfNull(cancha);

            var nuevaCancha = new Canchas
            {
                Deporte_ID = cancha.Deporte_ID
            };

            _context.Canchas.Add(nuevaCancha);
            await _context.SaveChangesAsync();

            /* var client = ApiServer.ObtenerClientHttp();
             var url = ApiServer.ObtenerUrlEndPoint("/api/canchas/");
             var content = new StringContent(JsonConvert.SerializeObject(new { Deporte_ID = cancha.Deporte_ID }), Encoding.UTF8, "application/json");

             var response = await client.PostAsync(url, content);
             response.EnsureSuccessStatusCode();
            */
        }


        // Actualizar una cancha existente
        public async Task UpdateCancha(int canchaID, CanchaDTO canchaModificada)
        {
            ArgumentNullException.ThrowIfNull(canchaModificada);

            var cancha = await _context.Canchas.FindAsync(canchaID);
            if (cancha == null)
                throw new KeyNotFoundException("Cancha no encontrada.");

            cancha.Deporte_ID = canchaModificada.Deporte_ID;
            await _context.SaveChangesAsync();

            /*  var client = ApiServer.ObtenerClientHttp();
              var url = ApiServer.ObtenerUrlEndPoint($"/api/canchas/{canchaID}");
              var content = new StringContent(JsonConvert.SerializeObject(new { Deporte_ID = canchaModificada.Deporte_ID }), Encoding.UTF8, "application/json");

              var response = await client.PutAsync(url, content);
              response.EnsureSuccessStatusCode();
            */
        }


        // Eliminar una cancha
        public async Task DeleteCancha(int canchaID)
        {
            var cancha = await _context.Canchas.FindAsync(canchaID);
            if (cancha == null)
                throw new KeyNotFoundException("Cancha no encontrada.");
            _context.Canchas.Remove(cancha);
            await _context.SaveChangesAsync();

            /* var client = ApiServer.ObtenerClientHttp();
             var url = ApiServer.ObtenerUrlEndPoint($"/api/canchas/{canchaID}");

             var response = await client.DeleteAsync(url);
             response.EnsureSuccessStatusCode();
            */
        }

        // Obtener todas las canchas
        public async Task<List<CanchaDTO>> GetAllCanchas()
        {
            return await _context.Canchas
                .Include(c => c.Deporte)
                .Select(c => new CanchaDTO
                {
                    Cancha_ID = c.Cancha_ID,
                    Deporte_ID = c.Deporte_ID,
                    Tipo = c.Deporte.Tipo,
                    deporte = c.Deporte
                })
                .ToListAsync();
            /* var client = ApiServer.ObtenerClientHttp();
             var url = ApiServer.ObtenerUrlEndPoint("/api/canchas");

             var response = await client.GetAsync(url);
             response.EnsureSuccessStatusCode();

             var result = await response.Content.ReadAsStringAsync();
             return JsonConvert.DeserializeObject<List<CanchaDTO>>(result);
               */
        }

        // Obtener una cancha por ID
        public async Task<CanchaDTO?> GetCanchaById(int id)
        {
            var cancha = await _context.Canchas
                .Include(c => c.Deporte)
                .FirstOrDefaultAsync(c => c.Cancha_ID == id);

            if (cancha == null) return null;

            return new CanchaDTO
            {
                Cancha_ID = cancha.Cancha_ID,
                Deporte_ID = cancha.Deporte_ID,
                Tipo = cancha.Deporte.Tipo,
                deporte = cancha.Deporte
            };

            /* var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint($"/api/canchas/{id}");

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CanchaDTO>(result);
            */
        }
    }
}
