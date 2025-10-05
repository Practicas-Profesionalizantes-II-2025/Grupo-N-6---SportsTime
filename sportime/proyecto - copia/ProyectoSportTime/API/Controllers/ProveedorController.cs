using CNegocio.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedoresController : ControllerBase
    {
        private readonly IProveedores _proveedoresLogic;

        public ProveedoresController(IProveedores proveedoresLogic)
        {
            _proveedoresLogic = proveedoresLogic;
        }

        

        // GET: api/proveedores (Obtener todos los proveedores)
        [HttpGet]
        public async Task<IActionResult> ObtenerTodosLosProveedores()
        {
            try
            {
                var proveedores = await _proveedoresLogic.ObtenerTodosLosProveedores();
                return Ok(proveedores);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al obtener los proveedores.");
            }
        }
        

        // GET: api/proveedores/{id} (Obtener un proveedor específico por ID)
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerProveedorPorId(int id)
        {
            try
            {
                var proveedor = await _proveedoresLogic.ObtenerProveedorPorId(id);
                if (proveedor == null)
                    return NotFound("Proveedor no encontrado.");

                return Ok(proveedor);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al obtener el proveedor.");
            }
        }
        
        // POST: api/proveedores (Alta de un nuevo proveedor)
        [HttpPost]
        public async Task<ActionResult<ProveedorDTO>> CrearProveedor(ProveedorDTO nuevoProveedor)
        {
            if (nuevoProveedor == null)
                return BadRequest("Datos de proveedor inválidos.");

            try
            {
                await _proveedoresLogic.CrearProveedor(nuevoProveedor);
                return Ok("Proveedor creado correctamente.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al crear el proveedor.");
            }
        }
        

        // PUT: api/proveedores/{id} (Modificar un proveedor existente)
        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarProveedor(int id, ProveedorDTO proveedor)
        {
            if (proveedor == null)
                return BadRequest("Datos de proveedor inválidos.");

            // Validación: el id de la URL y el del modelo deben coincidir y ser válidos
            if (id <= 0 || proveedor.Proveedor_ID <= 0 || id != proveedor.Proveedor_ID)
                return BadRequest("El Id del proveedor no es válido.");

            try
            {
                await _proveedoresLogic.ModificarProveedor(proveedor);
                return Ok("Proveedor actualizado correctamente.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Proveedor no encontrado.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al actualizar el proveedor.");
            }
        }
        

        // DELETE: api/proveedores/{id} (Eliminar un proveedor)
        [HttpDelete("{id}")]
        public async Task<IActionResult> BajaProveedor(int id)
        {
            try
            {
                await _proveedoresLogic.BajaProveedor(id);
                return Ok("Proveedor eliminado correctamente.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Proveedor no encontrado.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al eliminar el proveedor.");
            }
        }
        
    }

}
