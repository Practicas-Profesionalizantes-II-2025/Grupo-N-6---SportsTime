using CNegocio.Contracts;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using System;
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

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var lista = await _deportesLogic.ObtenerTodosLosDeportes();
                return Ok(lista);
            }
            catch
            {
                return StatusCode(500, "Error al obtener los deportes.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var deporte = await _deportesLogic.ObtenerDeportePorId(id);
                if (deporte == null) return NotFound("Deporte no encontrado.");
                return Ok(deporte);
            }
            catch
            {
                return StatusCode(500, "Error al obtener el deporte.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Crear(DeporteDTO dto)
        {
            if (dto == null) return BadRequest("Datos inválidos.");
            try
            {
                var creado = await _deportesLogic.CrearDeporte(dto);
                return Ok(creado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Error al crear el deporte.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Modificar(int id, DeporteDTO dto)
        {
            if (dto == null || id != dto.Deporte_ID) return BadRequest("Ids no coinciden.");
            try
            {
                var actualizado = await _deportesLogic.ModificarDeporte(dto);
                return Ok(actualizado);
            }
            catch (InvalidOperationException)
            {
                return NotFound("Deporte no encontrado.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Error al actualizar el deporte.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _deportesLogic.BajaDeporte(id);
                return Ok("Deporte eliminado correctamente.");
            }
            catch (InvalidOperationException)
            {
                return NotFound("Deporte no encontrado.");
            }
            catch
            {
                return StatusCode(500, "Error al eliminar el deporte.");
            }
        }
    }
}