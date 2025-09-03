using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Shared.Dtos;
using CNegocio.Contracts;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeportesController : ControllerBase
    {
        private readonly IDeportes _deportesLogic;

        public DeportesController(IDeportes deportesLogic)
        {
            _deportesLogic = deportesLogic;
        }

        /* private readonly ProyectoDbContext _context;

        // Constructor donde se inyecta el DbContext
        public DeportesController(ProyectoDbContext context)
        {
            _context = context;
        }
        */

        // GET: api/deportes (Obtener todos los deportes)
        [HttpGet]
        public async Task<IActionResult> ObtenerTodosLosDeportes()
        {
            try
            {
                var deportes = await _deportesLogic.ObtenerTodosLosDeportes();
                return Ok(deportes);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al obtener los deportes.");
            }
        }
        /* public async Task<ActionResult<List<Deportes>>> Get()
         {
             var deportes = await _context.Deportes.ToListAsync();
             return Ok(deportes);
         }
        */
        // GET: api/deportes/{id} (Obtener un deporte específico por ID)
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerDeportePorId(int id)
        {
            try
            {
                var deporte = await _deportesLogic.ObtenerDeportePorId(id);
                if (deporte == null)
                    return NotFound("Deporte no encontrado.");

                return Ok(deporte);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al obtener el deporte.");
            }
        }
        /* public async Task<ActionResult<Deportes>> Get(int id)
         {
             var deporte = await _context.Deportes.FindAsync(id);
             if (deporte == null)
             {
                 return NotFound(); // Devuelve 404 si no se encuentra el deporte
             }
             return Ok(deporte);
         }
        */

        // POST: api/deportes (Alta de un nuevo deporte)
        [HttpPost]
        public async Task<IActionResult> AltaDeporte([FromBody] DeporteDTO nuevoDeporte)
        {
            if (nuevoDeporte == null)
                return BadRequest("Datos de deporte inválidos.");

            try
            {
                await _deportesLogic.AltaDeporte(nuevoDeporte);
                return Ok("Deporte creado correctamente.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al crear el deporte.");
            }
        }
        /*public async Task<ActionResult<Deportes>> Post([FromBody] Deportes nuevoDeporte)
        {
            _context.Deportes.Add(nuevoDeporte);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = nuevoDeporte.Deporte_ID }, nuevoDeporte);
        }
        */

        // PUT: api/deportes/{id} (Modificar un deporte existente)
        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarDeporte(int id, [FromBody] DeporteDTO deporteModificado)
        {
            if (deporteModificado == null)
                return BadRequest("Datos de deporte inválidos.");

            try
            {
                await _deportesLogic.ModificarDeporte(id, deporteModificado);
                return Ok("Deporte actualizado correctamente.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Deporte no encontrado.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al actualizar el deporte.");
            }
        }
        /* public async Task<ActionResult> Put(int id, [FromBody] Deportes deporteModificado)
         {
             var deporte = await _context.Deportes.FindAsync(id);
             if (deporte == null)
             {
                 return NotFound(); // Devuelve 404 si no se encuentra el deporte
             }

             // Actualizamos los datos del deporte
             deporte.Tipo = deporteModificado.Tipo;

             await _context.SaveChangesAsync();

             return NoContent(); // Devuelve 204 No Content
         }
        */

        // DELETE: api/deportes/{id} (Eliminar un deporte)
        [HttpDelete("{id}")]
        public async Task<IActionResult> BajaDeporte(int id)
        {
            try
            {
                await _deportesLogic.BajaDeporte(id);
                return Ok("Deporte eliminado correctamente.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Deporte no encontrado.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al eliminar el deporte.");
            }
        }
        /* public async Task<ActionResult> Delete(int id)
        {
            var deporte = await _context.Deportes.FindAsync(id);
            if (deporte == null)
            {
                return NotFound(); // Devuelve 404 si no se encuentra el deporte
            }

            _context.Deportes.Remove(deporte);
            await _context.SaveChangesAsync();

            return NoContent(); // Devuelve 204 No Content
        }
        */
    }

}
