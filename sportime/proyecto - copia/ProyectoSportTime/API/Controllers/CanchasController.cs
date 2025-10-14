using CNegocio.Contracts;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using System;
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

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            try
            {
                var lista = await _canchasLogic.ObtenerTodasLasCanchas();
                return Ok(lista);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al obtener las canchas.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var cancha = await _canchasLogic.ObtenerCanchaPorId(id);
                if (cancha == null) return NotFound("Cancha no encontrada.");
                return Ok(cancha);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al obtener la cancha.");
            }
        }

        [HttpGet("por-deporte/{deporteId}")]
        public async Task<IActionResult> ObtenerPorDeporte(int deporteId)
        {
            try
            {
                var lista = await _canchasLogic.ObtenerCanchasPorDeporteId(deporteId);
                return Ok(lista);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al obtener las canchas por deporte.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CanchaDTO cancha)
        {
            if (cancha == null) return BadRequest("Datos inválidos.");
            try
            {
                var creada = await _canchasLogic.CrearCancha(cancha);
                return Ok(creada);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Modificar(int id, CanchaDTO cancha)
        {
            if (cancha == null || id != cancha.Cancha_ID) return BadRequest("Ids no coinciden.");
            try
            {
                var modificada = await _canchasLogic.ModificarCancha(cancha);
                return Ok(modificada);
            }
            catch (InvalidOperationException)
            {
                return NotFound("Cancha no encontrada.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al modificar la cancha.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _canchasLogic.BajaCancha(id);
                return Ok("Cancha eliminada correctamente.");
            }
            catch (InvalidOperationException)
            {
                return NotFound("Cancha no encontrada.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al eliminar la cancha.");
            }
        }
    }
}