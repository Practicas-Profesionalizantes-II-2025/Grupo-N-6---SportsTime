using CDatos.Data;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CNegocio.Contracts;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurnosController : ControllerBase
    {
        private readonly ITurnos _turnosLogic;

        public TurnosController(ITurnos turnosLogic)
        {
            _turnosLogic = turnosLogic;
        }
        /*private readonly ProyectoDbContext _context;

        // Constructor donde se inyecta el DbContext
        public TurnosController(ProyectoDbContext context)
        {
            _context = context;
        }
        */

        // GET: api/turnos (Obtener todos los turnos)
        [HttpGet]
        public async Task<IActionResult> ObtenerTodosLosTurnos()
        {
            try
            {
                var turnos = await _turnosLogic.ObtenerTodosLosTurnos();
                return Ok(turnos);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al obtener los turnos.");
            }
        }
        /*public async Task<ActionResult<List<TurnoDTO>>> Get()
        {
            var turnos = await _context.Turnos
                .Include(t => t.Administrador) // Incluye el administrador relacionado
                .Include(t => t.Canchas) // Incluye la cancha relacionada
                .Include(t => t.Cliente) // Incluye el cliente relacionado
                .Select(t => new TurnoDTO
                {
                    Turno_ID = t.Turno_ID,
                    Admin_ID = t.Admin_ID,
                    Cancha_ID = t.Cancha_ID,
                    HoraInicio = t.HoraInicio,
                    HoraFin = t.HoraFin,
                    Cliente_ID = t.Cliente_ID // Agregado para reflejar la relación con Cliente
                })
                .ToListAsync();

            return Ok(turnos);
        }
        */
        // GET: api/turnos/{id} (Obtener un turno específico por ID)
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerTurnoPorId(int id)
        {
            try
            {
                var turno = await _turnosLogic.ObtenerTurnoPorId(id);
                if (turno == null)
                    return NotFound("Turno no encontrado.");

                return Ok(turno);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al obtener el turno.");
            }
        }
        /* public async Task<ActionResult<TurnoDTO>> Get(int id)
         {
             var turno = await _context.Turnos
                 .Include(t => t.Administrador)
                 .Include(t => t.Canchas)
                 .Include(t => t.Cliente)
                 .Where(t => t.Turno_ID == id)
                 .Select(t => new TurnoDTO
                 {
                     Turno_ID = t.Turno_ID,
                     Admin_ID = t.Admin_ID,
                     Cancha_ID = t.Cancha_ID,
                     HoraInicio = t.HoraInicio,
                     HoraFin = t.HoraFin,
                     Cliente_ID = t.Cliente_ID // Agregado para reflejar la relación con Cliente
                 })
                 .FirstOrDefaultAsync();

             if (turno == null)
             {
                 return NotFound(); // Devuelve 404 si no se encuentra el turno
             }

             return Ok(turno);
         }
         */
        // POST: api/turnos (Alta de un nuevo turno)
        [HttpPost]
        public async Task<IActionResult> CrearTurno([FromBody] TurnoDTO nuevoTurno)
        {
            if (nuevoTurno == null)
                return BadRequest("Datos de turno inválidos.");

            try
            {
                var resultado = await _turnosLogic.CrearTurno(nuevoTurno);
                if (!resultado.EsExitoso)
                    return BadRequest(resultado.MensajeError);

                return Ok("Turno creado correctamente.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al crear el turno.");
            }
        }
        /* public async Task<ActionResult<TurnoDTO>> Post([FromBody] TurnoDTO nuevoTurno)
         {
             var turno = new Turnos
             {
                 Admin_ID = nuevoTurno.Admin_ID,
                 Cancha_ID = nuevoTurno.Cancha_ID,
                 HoraInicio = nuevoTurno.HoraInicio,
                 HoraFin = nuevoTurno.HoraFin,
                 Cliente_ID = nuevoTurno.Cliente_ID
             };

             // Guardar el turno
             _context.Turnos.Add(turno);
             await _context.SaveChangesAsync();

             // Guardar los productos asociados con cantidad
             if (nuevoTurno.ConsumicionProductos != null)
             {
                 foreach (var consumicionProductoDTO in nuevoTurno.ConsumicionProductos)
                 {
                     var consumicionProducto = new ConsumicionProducto
                     {
                         Consumicion_ID = consumicionProductoDTO.Consumicion_ID,
                         Producto_ID = consumicionProductoDTO.Producto_ID,
                         Cantidad = consumicionProductoDTO.Cantidad // Establecer la cantidad
                     };

                     _context.consumicionProductos.Add(consumicionProducto);
                 }
                 await _context.SaveChangesAsync();
             }

             nuevoTurno.Turno_ID = turno.Turno_ID;
             return CreatedAtAction(nameof(Get), new { id = nuevoTurno.Turno_ID }, nuevoTurno);
         }
        */

        // PUT: api/turnos/{id} (Modificar un turno existente)
        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarTurno(int id, [FromBody] TurnoDTO turnoModificado)
        {
            if (turnoModificado == null)
                return BadRequest("Datos de turno inválidos.");

            try
            {
                await _turnosLogic.ModificarTurno(id, turnoModificado);
                return Ok("Turno actualizado correctamente.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Turno no encontrado.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al actualizar el turno.");
            }
        }
        /* public async Task<ActionResult> Put(int id, [FromBody] TurnoDTO turnoModificado)
         {
             var turno = await _context.Turnos
                 .Include(t => t.ConsumicionProductos)  // Aseguramos que los productos se incluyan
                 .FirstOrDefaultAsync(t => t.Turno_ID == id);

             if (turno == null)
             {
                 return NotFound(); // Devuelve 404 si no se encuentra el turno
             }

             // Actualizamos los datos del turno
             turno.Admin_ID = turnoModificado.Admin_ID;
             turno.Cancha_ID = turnoModificado.Cancha_ID;
             turno.HoraInicio = turnoModificado.HoraInicio;
             turno.HoraFin = turnoModificado.HoraFin;
             turno.Cliente_ID = turnoModificado.Cliente_ID;

             // Eliminar productos antiguos
             _context.consumicionProductos.RemoveRange(turno.ConsumicionProductos);

             // Agregar nuevos productos con cantidad
             if (turnoModificado.ConsumicionProductos != null)
             {
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
             return NoContent(); // Devuelve 204 No Content
         }
        */

        // DELETE: api/turnos/{id} (Eliminar un turno)
        [HttpDelete("{id}")]
        public async Task<IActionResult> BorrarTurno(int id)
        {
            try
            {
                await _turnosLogic.BorrarTurno(id);
                return Ok("Turno eliminado correctamente.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Turno no encontrado.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al eliminar el turno.");
            }
        }
        /*public async Task<ActionResult> Delete(int id)
        {
            var turno = await _context.Turnos.FindAsync(id);
            if (turno == null)
            {
                return NotFound(); // Devuelve 404 si no se encuentra el turno
            }

            _context.Turnos.Remove(turno);
            await _context.SaveChangesAsync();

            return NoContent(); // Devuelve 204 No Content
        }
        */
        // GET: api/turnos/cancha/{canchaId}
        [HttpGet("cancha/{canchaId}")]
        public async Task<IActionResult> ObtenerTurnosPorCancha(int canchaId, [FromQuery] DateTime horaInicio, [FromQuery] DateTime horaFin)
        {
            try
            {
                var turnos = await _turnosLogic.ObtenerTurnosPorCancha(canchaId, horaInicio, horaFin);
                return Ok(turnos);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al obtener los turnos por cancha.");
            }
        }
    }

}
