using Microsoft.AspNetCore.Mvc;
using Shared.Entidades;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using API.Data;
using Shared.Dtos;

namespace API.Controllers.CanchasController
{
    [ApiController]
    [Route("api/[controller]")]
    public class CanchasController : ControllerBase
    {
        private readonly ProyectoDbContext _context;

        // Constructor donde se inyecta el DbContext
        public CanchasController(ProyectoDbContext context)
        {
            _context = context;
        }

        // GET: api/canchas (Obtener todas las canchas)
        [HttpGet]
        public async Task<ActionResult<List<CanchaDTO>>> Get()
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

        // GET: api/canchas/{id} (Obtener una cancha específica por ID)
        [HttpGet("{id}")]
        public async Task<ActionResult<CanchaDTO>> Get(int id)
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

        // POST: api/canchas (Alta de una nueva cancha)
        [HttpPost]
        public async Task<ActionResult<Canchas>> Post([FromBody] CanchaDTO nuevaCanchaDto)
        {
            var nuevaCancha = new Canchas
            {
                Deporte_ID = nuevaCanchaDto.Deporte_ID // Solo guardamos el Deporte_ID
            };

            _context.Canchas.Add(nuevaCancha);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = nuevaCancha.Cancha_ID }, nuevaCancha);
        }

        // PUT: api/canchas/{id} (Modificar una cancha existente)
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CanchaDTO canchaModificadaDto)
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

        // DELETE: api/canchas/{id} (Eliminar una cancha)
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
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
    }
}
