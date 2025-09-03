using Microsoft.AspNetCore.Mvc;
using Shared.Entidades;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CDatos.Data;
using Shared.Dtos;
using CNegocio.Contracts;
using Negocio.Implementations;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CanchasController : ControllerBase
    {
        private readonly ICanchas _canchasLogic;

        // Constructor donde se inyecta la logica de negocio
        public CanchasController(ICanchas canchasLogic)
        {
            _canchasLogic = canchasLogic;
        }

        // GET: api/canchas (Obtener todas las canchas)
        [HttpGet]
        public async Task<IActionResult> ObtenerTodasLasCanchas()
        {
            try
            {
                var canchas = await _canchasLogic.ObtenerTodasLasCanchas();
                return Ok(canchas);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al obtener las canchas.");
            }
        }
        /*public async Task<ActionResult<List<CanchaDTO>>> Get()
        {
            var canchas = await _context.Canchas
                .Include(c => c.Deporte) // Incluye el deporte asociado
                .Select(c => new CanchaDTO
                {
                    Cancha_ID = c.Cancha_ID,
                    Deporte_ID = c.Deporte_ID,
                    deporte = c.Deporte,
                    Tipo = c.Deporte.Tipo // Asigna el tipo del deporte al DTO
                })
                .ToListAsync();

            return Ok(canchas);
        }
        */

        // GET: api/canchas/{id} (Obtener una cancha específica por ID)
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerCanchaPorId(int id)
        {
            try
            {
                var cancha = await _canchasLogic.ObtenerCanchaPorId(id);
                if (cancha == null)
                    return NotFound("Cancha no encontrada.");

                return Ok(cancha);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al obtener la cancha.");
            }
        }
        /* public async Task<ActionResult<CanchaDTO>> Get(int id)
         {
             var cancha = await _context.Canchas
                 .Include(c => c.Deporte) // Incluye el deporte asociado
                 .Where(c => c.Cancha_ID == id)
                 .Select(c => new CanchaDTO
                 {
                     Cancha_ID = c.Cancha_ID,
                     Deporte_ID = c.Deporte_ID,
                     Tipo = c.Deporte.Tipo // Asigna el tipo del deporte al DTO
                 })
                 .FirstOrDefaultAsync();

             if (cancha == null)
             {
                 return NotFound(); // Devuelve 404 si no se encuentra la cancha
             }

             return Ok(cancha);
         }
         */
        // POST: api/canchas (Alta de una nueva cancha)
        [HttpPost]
        public async Task<IActionResult> AltaCancha([FromBody] CanchaDTO nuevaCancha)
        {
            if (nuevaCancha == null)
                return BadRequest("Datos de cancha inválidos.");

            try
            {
                await _canchasLogic.AltaCancha(nuevaCancha);
                return Ok("Cancha creada correctamente.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al crear la cancha.");
            }
        }
            /* public async Task<ActionResult<Canchas>> Post([FromBody] CanchaDTO nuevaCanchaDto)
             {
                 var nuevaCancha = new Canchas
                 {
                     Deporte_ID = nuevaCanchaDto.Deporte_ID // Solo guardamos el Deporte_ID
                 };

                 _context.Canchas.Add(nuevaCancha);
                 await _context.SaveChangesAsync();

                 return CreatedAtAction(nameof(Get), new { id = nuevaCancha.Cancha_ID }, nuevaCancha);

             }
            */

            // PUT: api/canchas/{id} (Modificar una cancha existente)
            [HttpPut("{id}")]
        public async Task<IActionResult> ModificarCancha(int id, [FromBody] CanchaDTO canchaModificada)
        {
            if (canchaModificada == null)
                return BadRequest("Datos de cancha inválidos.");

            try
            {
                await _canchasLogic.ModificarCancha(id, canchaModificada);
                return Ok("Cancha actualizada correctamente.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Cancha no encontrada.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al actualizar la cancha.");
            }
        }
        /*  public async Task<ActionResult> Put(int id, [FromBody] CanchaDTO canchaModificadaDto)
          {
              var cancha = await _context.Canchas.FindAsync(id);
              if (cancha == null)
              {
                  return NotFound(); // Devuelve 404 si no se encuentra la cancha
              }

              // Actualizamos los datos de la cancha
              cancha.Deporte_ID = canchaModificadaDto.Deporte_ID; // Solo actualizamos el Deporte_ID

              await _context.SaveChangesAsync();

              return NoContent(); // Devuelve 204 No Content
          }
        */

        // DELETE: api/canchas/{id} (Eliminar una cancha)
        [HttpDelete("{id}")]
        public async Task<IActionResult> BajaCancha(int id)
        {
            try
            {
                await _canchasLogic.BajaCancha(id);
                return Ok("Cancha eliminada correctamente.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Cancha no encontrada.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al eliminar la cancha.");
            }
        }
        /*public async Task<ActionResult> Delete(int id)
        {
            var cancha = await _context.Canchas.FindAsync(id);
            if (cancha == null)
            {
                return NotFound(); // Devuelve 404 si no se encuentra la cancha
            }

            _context.Canchas.Remove(cancha);
            await _context.SaveChangesAsync();

            return NoContent(); // Devuelve 204 No Content
        }
        */
    }
}
