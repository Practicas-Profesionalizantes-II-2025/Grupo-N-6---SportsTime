using CNegocio.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;

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
        // GET: api/turnos/por-canchas/{canchaId} (Obtener turnos por cancha)
        [HttpGet("por-canchas/{canchaId}")]
        public async Task<IActionResult> ObtenerTurnosPorCancha(int canchaId)
        {
            try
            {
                var turnos = await _turnosLogic.ObtenerTurnosPorCancha(canchaId);
                return Ok(turnos);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al obtener los turnos de la cancha.");
            }
        }
        // GET: api/turnos/por-usuario/{usuarioId} (Obtener turnos por usuario)
        [HttpGet("por-usuario/{usuarioId}")]
        public async Task<IActionResult> ObtenerTurnosPorUsuario(int usuarioId)
        {
            try
            {
                var turnos = await _turnosLogic.ObtenerTurnosPorUsuario(usuarioId);
                return Ok(turnos);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al obtener los turnos del usuario.");
            }
        }
        // POST: api/turnos (Alta de un nuevo turno)
        [HttpPost]
        public async Task<ActionResult<TurnoDTO>> CrearTurno(TurnoDTO nuevoTurno)
        {
            if (nuevoTurno == null)
                return BadRequest("Datos de turno inválidos.");

            try
            {
                await _turnosLogic.CrearTurno(nuevoTurno);
                return Ok("Turno creado correctamente.");
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
                return StatusCode(500, "Error al crear el turno.");
            }
        }
        // PUT: api/turnos/{id} (Modificar un turno existente)
        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarTurno(int id, TurnoDTO turno)
        {
            if (turno == null)
                return BadRequest("Datos de turno inválidos.");

            if (id <= 0 || turno.Turno_ID <= 0 || id != turno.Turno_ID)
                return BadRequest("El Id del turno no es válido.");

            try
            {
                await _turnosLogic.ModificarTurno(turno);
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
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al actualizar el turno.");
            }
        }
        // DELETE: api/turnos/{id} (Eliminar un turno)
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarTurno(int id)
        {
            try
            {
                await _turnosLogic.EliminarTurno(id);
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
    }
}