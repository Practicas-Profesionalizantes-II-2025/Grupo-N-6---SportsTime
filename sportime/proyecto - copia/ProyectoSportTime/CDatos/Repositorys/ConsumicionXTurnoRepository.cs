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
    public class ConsumicionXTurnoRepository : IConsumicionXTurnoRepository
    {
        private readonly ProyectoDbContext _context;

        public ConsumicionXTurnoRepository(ProyectoDbContext context)
        {
            _context = context;
        }
        public async Task AgregarConsumicion(ConsumicionXturnoDTO consumicion)
        {
            ArgumentNullException.ThrowIfNull(consumicion);
            var entidad = new ConsumicionXturno
            {
                Turno_ID = consumicion.Turno_ID,
                Producto_ID = consumicion.Producto_ID,
                Cantidad = consumicion.Cantidad
            };

            _context.consumicionXturnos.Add(entidad);
            await _context.SaveChangesAsync();
        }
           /* try
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
           */

        public async Task<List<ConsumicionXturnoDTO>> ObtenerConsumicionesPorTurno(int turnoId)
        {
            return await _context.consumicionXturnos
               .Where(x => x.Turno_ID == turnoId)
               .Select(x => new ConsumicionXturnoDTO
               {
                   ConsumicionXturno_ID = x.ConsumicionXturno_ID,
                   Turno_ID = x.Turno_ID,
                   Producto_ID = x.Producto_ID,
                   Cantidad = x.Cantidad
               })
               .ToListAsync();
            /* var client = ApiServer.ObtenerClientHttp();
             var url = ApiServer.ObtenerUrlEndPoint($"/api/ConsumicionXTurno/{turnoId}");

             var response = await client.GetAsync(url);
             response.EnsureSuccessStatusCode();

             var result = await response.Content.ReadAsStringAsync();
             return JsonConvert.DeserializeObject<List<ConsumicionXturnoDTO>>(result);
            */
        }

        public async Task QuitarConsumicion(int turnoID, int productoID) // 🔹 Cambio aquí
        {
            var entidad = await _context.consumicionXturnos
               .FirstOrDefaultAsync(x => x.Turno_ID == turnoID && x.Producto_ID == productoID);

            if (entidad == null)
                throw new KeyNotFoundException("Consumición no encontrada para ese turno y producto.");

            _context.consumicionXturnos.Remove(entidad);
            await _context.SaveChangesAsync();
            /*var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint($"/api/ConsumicionXTurno/{turnoID}/{productoID}"); // 🔹 Cambio aquí

            var response = await client.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
            */
        }
    }

}
