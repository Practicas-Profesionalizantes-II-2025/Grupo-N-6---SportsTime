using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Shared.Entidades;
using CDatos.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeportesController : ControllerBase
    {
        private readonly ProyectoDbContext _context;

        // Constructor donde se inyecta el DbContext
        public DeportesController(ProyectoDbContext context)
        {
            _context = context;
        }

        // GET: api/deportes (Obtener todos los deportes)
        [HttpGet]
        public async Task<ActionResult<List<Deportes>>> Get()
        {
            var deportes = await _context.Deportes.ToListAsync();
            return Ok(deportes);
        }

        // GET: api/deportes/{id} (Obtener un deporte específico por ID)
        [HttpGet("{id}")]
        public async Task<ActionResult<Deportes>> Get(int id)
        {
            var deporte = await _context.Deportes.FindAsync(id);
            if (deporte == null)
            {
                return NotFound(); // Devuelve 404 si no se encuentra el deporte
            }
            return Ok(deporte);
        }

        // POST: api/deportes (Alta de un nuevo deporte)
        [HttpPost]
        public async Task<ActionResult<Deportes>> Post([FromBody] Deportes nuevoDeporte)
        {
            _context.Deportes.Add(nuevoDeporte);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = nuevoDeporte.Deporte_ID }, nuevoDeporte);
        }

        // PUT: api/deportes/{id} (Modificar un deporte existente)
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Deportes deporteModificado)
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

        // DELETE: api/deportes/{id} (Eliminar un deporte)
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
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
    }

}
