using CDatos.Data;
using CNegocio.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Entidades;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsumicionXturno : ControllerBase
    {
        private readonly IConsumicionXTurno _logic;

        public ConsumicionXturno(IConsumicionXTurno logic)
        {
            _logic = logic;
        }

        // 🔹 Agregar un producto a la consumición de un turno
        [HttpPost]
        public async Task<IActionResult> AgregarConsumicion([FromBody] ConsumicionXturnoDTO dto)
        {
            if (dto == null)
                return BadRequest("Datos de consumición inválidos.");

            try
            {
                await _logic.AgregarConsumicion(dto);
                return Ok("Consumición agregada correctamente.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al agregar la consumición.");
            }
        }
        /*  public async Task<IActionResult> AgregarConsumicion([FromBody] ConsumicionXturnoDTO nuevaConsumicion)
         {
             if (nuevaConsumicion == null || nuevaConsumicion.Turno_ID == 0 || nuevaConsumicion.Producto_ID == 0 || nuevaConsumicion.Cantidad <= 0)
             {
                 return BadRequest(new { mensaje = "Datos de la consumición no válidos" });
             }

             var consumicion = new Shared.Entidades.ConsumicionXturno
             {
                 Turno_ID = nuevaConsumicion.Turno_ID,
                 Producto_ID = nuevaConsumicion.Producto_ID, // 🔹 Cambio aquí
                 Cantidad = nuevaConsumicion.Cantidad
             };

             _context.consumicionXturnos.Add(consumicion);
             await _context.SaveChangesAsync();
             return Ok(new { mensaje = "Producto agregado al turno correctamente" });

         }
        */

        // 🔹 Quitar un producto de la consumición de un turno
        [HttpDelete("{turnoId}/{productoId}")]
        public async Task<IActionResult> QuitarConsumicion(int turnoId, int productoId)
        {
            try
            {
                await _logic.QuitarConsumicion(turnoId, productoId);
                return Ok("Consumición eliminada correctamente.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Consumición no encontrada para ese turno y producto.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al eliminar la consumición.");
            }
        }

        /* public async Task<IActionResult> QuitarConsumicion(int turnoId, int productoId) // 🔹 Cambio aquí
         {
             var consumicion = await _context.consumicionXturnos
                 .FirstOrDefaultAsync(c => c.Turno_ID == turnoId && c.Producto_ID == productoId); // 🔹 Cambio aquí

             if (consumicion == null)
                 return NotFound(new { mensaje = "Producto no encontrado en la consumición de este turno" });

             _context.consumicionXturnos.Remove(consumicion);
             await _context.SaveChangesAsync();
             return Ok(new { mensaje = "Producto eliminado de la consumición correctamente" });
         }
        */

        // 🔹 Obtener todos los productos consumidos en un turno específico
        [HttpGet("{turnoId}")]
        public async Task<IActionResult> ObtenerConsumicionesPorTurno(int turnoId)
        {
            try
            {
                var lista = await _logic.ObtenerConsumicionesPorTurno(turnoId);
                return Ok(lista);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al obtener las consumiciones del turno.");
            }
        }
        /* public async Task<IActionResult> ObtenerConsumicionesDeTurno(int turnoId)
         {
             var consumiciones = await _context.consumicionXturnos
                 .Where(c => c.Turno_ID == turnoId)
                 .Include(c => c.Productos) // 🔹 Cambio aquí: Incluye detalles del producto
                 .Select(c => new ConsumicionXturnoDTO
                 {
                     ConsumicionXturno_ID = c.ConsumicionXturno_ID,
                     Turno_ID = c.Turno_ID,
                     Producto_ID = c.Producto_ID, // 🔹 Cambio aquí
                     Cantidad = c.Cantidad
                 })
                 .ToListAsync();

             return Ok(consumiciones);
         }
        */
    }


}
