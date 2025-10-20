using CNegocio.Contracts;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        // GET: api/deportes
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var list = await _deportesLogic.ObtenerTodos();
                return Ok(list);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al obtener deportes.");
            }
        }

        // GET: api/deportes/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var d = await _deportesLogic.ObtenerPorId(id);
                if (d == null) return NotFound("Deporte no encontrado.");
                return Ok(d);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al obtener el deporte.");
            }
        }

        // POST: api/deportes
        [HttpPost]
        public async Task<IActionResult> Create(DeporteDTO dto)
        {
            if (dto == null) return BadRequest("Datos inválidos.");
            try
            {
                var created = await _deportesLogic.Crear(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Deporte_ID }, created);
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
            catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
            catch (Exception) { return StatusCode(500, "Error al crear el deporte."); }
        }

        // PUT: api/deportes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DeporteDTO dto)
        {
            if (dto == null) return BadRequest("Datos inválidos.");
            if (id <= 0 || dto.Deporte_ID <= 0 || id != dto.Deporte_ID) return BadRequest("Id inválido.");

            try
            {
                var updated = await _deportesLogic.Modificar(dto);
                return Ok(updated);
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
            catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
            catch (Exception) { return StatusCode(500, "Error al actualizar el deporte."); }
        }

        // DELETE: api/deportes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _deportesLogic.Eliminar(id);
                return Ok("Deporte eliminado correctamente.");
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
            catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
            catch (Exception) { return StatusCode(500, "Error al eliminar el deporte."); }
        }
    }
}