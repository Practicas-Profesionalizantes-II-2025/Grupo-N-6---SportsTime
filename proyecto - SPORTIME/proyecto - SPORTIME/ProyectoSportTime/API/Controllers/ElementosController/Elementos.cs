using API.Data;
using Microsoft.AspNetCore.Mvc;
using Shared.Entidades;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;

namespace API.Controllers.ElementosController
{
    [ApiController]
    [Route("api/[controller]")]
    public class ElementosController : ControllerBase
    {
        private readonly ProyectoDbContext _context;

        // Constructor donde se inyecta el DbContext
        public ElementosController(ProyectoDbContext context)
        {
            _context = context;
        }

        // GET: api/elementos (Obtener todos los elementos)
        [HttpGet]
        public async Task<ActionResult<List<ElementoDTO>>> Get()
        {
            var elementos = await _context.Elementos.ToListAsync();
            // Mapeo de la entidad a DTO
            var elementosDto = elementos.Select(e => new ElementoDTO
            {
                Elemento_ID = e.Elemento_ID,
                Nombre = e.Nombre,
                Cantidad = e.Cantidad
            }).ToList();

            return Ok(elementosDto);
        }

        // GET: api/elementos/{id} (Obtener un elemento específico por ID)
        [HttpGet("{id}")]
        public async Task<ActionResult<ElementoDTO>> Get(int id)
        {
            var elemento = await _context.Elementos.FindAsync(id);
            if (elemento == null)
            {
                return NotFound(); // Devuelve 404 si no se encuentra el elemento
            }

            // Mapeo de la entidad a DTO
            var elementoDto = new ElementoDTO
            {
                Elemento_ID = elemento.Elemento_ID,
                Nombre = elemento.Nombre,
                Cantidad = elemento.Cantidad
            };

            return Ok(elementoDto);
        }

        // POST: api/elementos (Alta de un nuevo elemento)
        [HttpPost]
        public async Task<ActionResult<ElementoDTO>> Post([FromBody] ElementoDTO nuevoElementoDto)
        {
            if (nuevoElementoDto == null)
            {
                return BadRequest("Elemento no puede ser nulo.");
            }

            var nuevoElemento = new Elementos
            {
                Nombre = nuevoElementoDto.Nombre,
                Cantidad = nuevoElementoDto.Cantidad
            };

            _context.Elementos.Add(nuevoElemento);
            await _context.SaveChangesAsync();

            // Mapeo de la entidad a DTO
            nuevoElementoDto.Elemento_ID = nuevoElemento.Elemento_ID; // Asignar el ID generado

            return CreatedAtAction(nameof(Get), new { id = nuevoElemento.Elemento_ID }, nuevoElementoDto);
        }

        // PUT: api/elementos/{id} (Modificar un elemento existente)
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ElementoDTO elementoDto)
        {
            var elemento = await _context.Elementos.FindAsync(id);
            if (elemento == null)
            {
                return NotFound(); // Devuelve 404 si no se encuentra el elemento
            }

            // Actualizamos los datos del elemento
            elemento.Nombre = elementoDto.Nombre;
            elemento.Cantidad = elementoDto.Cantidad;

            await _context.SaveChangesAsync();

            return NoContent(); // Devuelve 204 No Content
        }

        // DELETE: api/elementos/{id} (Eliminar un elemento)
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var elemento = await _context.Elementos.FindAsync(id);
            if (elemento == null)
            {
                return NotFound(); // Devuelve 404 si no se encuentra el elemento
            }

            _context.Elementos.Remove(elemento);
            await _context.SaveChangesAsync();

            return NoContent(); // Devuelve 204 No Content
        }
    }
}

