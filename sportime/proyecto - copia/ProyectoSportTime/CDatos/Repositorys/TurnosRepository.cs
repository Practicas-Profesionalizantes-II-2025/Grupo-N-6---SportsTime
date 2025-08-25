using System;
using Shared.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Entidades;
using CDatos.Data;
using CDatos.Repositorys.IRepositorys;
using Microsoft.EntityFrameworkCore;


namespace CDatos.Repositorys
{
    public class TurnosRepository : ITurnosRepository
    {
        private readonly ProyectoDbContext _context;

        public TurnosRepository(ProyectoDbContext context)
        {
            _context = context;
        }
        // Crear un nuevo turno
        public async Task CrearTurno(TurnoDTO turno)
        {
            ArgumentNullException.ThrowIfNull(turno);

            var nuevoTurno = new Turnos
            {
                Admin_ID = turno.Admin_ID,
                Cancha_ID = turno.Cancha_ID,
                Cliente_ID = turno.Cliente_ID,
                HoraInicio = turno.HoraInicio,
                HoraFin = turno.HoraFin,
                ConsumicionProductos = turno.ConsumicionProductos?.Select(cp => new ConsumicionProducto
                {
                    Consumicion_ID = cp.Consumicion_ID,
                    Producto_ID = cp.Producto_ID,
                    Cantidad = cp.Cantidad
                }).ToList() // Mapeo de consumiciones 
            };
            _context.Turnos.Add(nuevoTurno);
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
        public async Task<List<TurnoDTO>> ObtenerTurnosPorCancha(int canchaID, DateTime horaInicio, DateTime horaFin)
        {
            return await _context.Turnos
                .Where(t => t.Cancha_ID == canchaID && t.HoraInicio >= horaInicio && t.HoraFin <= horaFin)
                .Include(t => t.ConsumicionProductos) // Incluir las consumiciones asociadas
                .Select(t => new TurnoDTO
                {
                    Turno_ID = t.Turno_ID,
                    Admin_ID = t.Admin_ID,
                    Cancha_ID = t.Cancha_ID,
                    Cliente_ID = t.Cliente_ID,
                    HoraInicio = t.HoraInicio,
                    HoraFin = t.HoraFin,
                    ConsumicionProductos = t.ConsumicionProductos.Select(cp => new ConsumicionProductoDTO
                    {
                        Consumicion_ID = cp.Consumicion_ID,
                        Producto_ID = cp.Producto_ID,
                        Cantidad = cp.Cantidad // Incluir la cantidad
                    }).ToList()
                })
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
        public async Task ModificarTurno(int turnoID, TurnoDTO turnoModificado)
        {
            ArgumentNullException.ThrowIfNull(turnoModificado);
            var turno = await _context.Turnos.FindAsync(turnoID);
            if (turno == null)
                throw new ArgumentException("Turno no encontrado");
            turno.Admin_ID = turnoModificado.Admin_ID;
            turno.Cancha_ID = turnoModificado.Cancha_ID;
            turno.Cliente_ID = turnoModificado.Cliente_ID;
            turno.HoraInicio = turnoModificado.HoraInicio;
            turno.HoraFin = turnoModificado.HoraFin;
            // Actualizar las consumiciones asociadas
            if (turnoModificado.ConsumicionProductos != null)
            {
                // Eliminar las consumiciones existentes
                var consumicionesExistentes = _context.consumicionProductos.Where(cp => cp.Consumicion_ID == turnoID).ToList();
                _context.consumicionProductos.RemoveRange(consumicionesExistentes);
                // Agregar las nuevas consumiciones
                foreach (var consumicionProductoDTO in turnoModificado.ConsumicionProductos)
                {
                    var consumicionProducto = new ConsumicionProducto
                    {
                        Consumicion_ID = consumicionProductoDTO.Consumicion_ID,
                        Producto_ID = consumicionProductoDTO.Producto_ID,
                        Cantidad = consumicionProductoDTO.Cantidad // Establecer la cantidad
                    };
                    _context.consumicionProductos.Add(consumicionProducto);
                }
            }
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
            var turno = await _context.Turnos.FindAsync(turnoID);
            if (turno == null)
                throw new KeyNotFoundException("Turno no encontrado.");
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
        public async Task<List<TurnoDTO>> ObtenerTodosLosTurnos()
        {
            return await _context.Turnos
                .Include(t => t.ConsumicionProductos) // Incluir las consumiciones asociadas
                .Select(t => new TurnoDTO
                {
                    Turno_ID = t.Turno_ID,
                    Admin_ID = t.Admin_ID,
                    Cancha_ID = t.Cancha_ID,
                    Cliente_ID = t.Cliente_ID,
                    HoraInicio = t.HoraInicio,
                    HoraFin = t.HoraFin,
                    ConsumicionProductos = t.ConsumicionProductos.Select(cp => new ConsumicionProductoDTO
                    {
                        Consumicion_ID = cp.Consumicion_ID,
                        Producto_ID = cp.Producto_ID,
                        Cantidad = cp.Cantidad // Incluir la cantidad
                    }).ToList()
                })
                .ToListAsync();
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
        public async Task<TurnoDTO?> ObtenerTurnoPorId(int id)
        {
            var turno = await _context.Turnos
                .Include(t => t.ConsumicionProductos) // Incluir las consumiciones asociadas
                .FirstOrDefaultAsync(t => t.Turno_ID == id);
            if (turno == null)
                return null;
            return new TurnoDTO
            {
                Turno_ID = turno.Turno_ID,
                Admin_ID = turno.Admin_ID,
                Cancha_ID = turno.Cancha_ID,
                Cliente_ID = turno.Cliente_ID,
                HoraInicio = turno.HoraInicio,
                HoraFin = turno.HoraFin,
                ConsumicionProductos = turno.ConsumicionProductos.Select(cp => new ConsumicionProductoDTO
                {
                    Consumicion_ID = cp.Consumicion_ID,
                    Producto_ID = cp.Producto_ID,
                    Cantidad = cp.Cantidad // Incluir la cantidad
                }).ToList()
            };
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
