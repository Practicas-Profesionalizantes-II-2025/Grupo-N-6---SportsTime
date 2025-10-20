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
    public class CanchasController : ControllerBase
    {
        private readonly ICanchas _canchasLogic;

        public CanchasController(ICanchas canchasLogic)
        {
            _canchasLogic = canchasLogic;
        }

        // GET: api/canchas
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var list = await _canchasLogic.ObtenerTodas();
                return Ok(list);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al obtener canchas.");
            }
        }

        // GET: api/canchas/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var c = await _canchasLogic.ObtenerPorId(id);
                if (c == null) return NotFound("Cancha no encontrada.");
                return Ok(c);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al obtener la cancha.");
            }
        }

        // GET: api/canchas/por-deporte/{deporteId}
        [HttpGet("por-deporte/{deporteId}")]
        public async Task<IActionResult> GetByDeporte(int deporteId)
        {
            try
            {
                var list = await _canchasLogic.ObtenerPorDeporte(deporteId);
                return Ok(list);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al obtener canchas por deporte.");
            }
        }

        // GET: api/canchas/activas
        [HttpGet("activas")]
        public async Task<IActionResult> GetActivas()
        {
            try
            {
                var list = await _canchasLogic.ObtenerActivas();
                return Ok(list);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al obtener canchas activas.");
            }
        }

        // POST: api/canchas
        [HttpPost]
        public async Task<IActionResult> Create(CanchaDTO dto)
        {
            if (dto == null) return BadRequest("Datos inválidos.");
            try
            {
                var created = await _canchasLogic.CrearCancha(dto);
                return Ok(created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al crear la cancha.");
            }
        }

        // PUT: api/canchas/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CanchaDTO dto)
        {
            if (dto == null) return BadRequest("Datos inválidos.");
            if (id <= 0 || dto.Cancha_ID <= 0 || id != dto.Cancha_ID) return BadRequest("Id inválido.");

            try
            {
                var updated = await _canchasLogic.ModificarCancha(dto);
                return Ok(updated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al actualizar la cancha.");
            }
        }

        // DELETE: api/canchas/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _canchasLogic.EliminarCancha(id);
                return Ok("Cancha eliminada correctamente.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al eliminar la cancha.");
            }
        }
    }
}