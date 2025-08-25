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

        /* private readonly ProyectoDbContext _context;

         // Constructor donde se inyecta el DbContext
         public ProveedoresController(ProyectoDbContext context)
         {
             _context = context;
         }
        */

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
        /* public async Task<ActionResult<List<ProveedorDTO>>> Get()
         {
             var proveedores = await _context.Proveedores
                 .Select(p => new ProveedorDTO
                 {
                     Proveedor_ID = p.Proveedor_ID,
                     Nombre = p.Nombre,
                     Telefono = p.Telefono,
                     Email = p.Email
                 }).ToListAsync();

             return Ok(proveedores);
         }
        */

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
        /* public async Task<ActionResult<ProveedorDTO>> Get(int id)
        {
            var proveedor = await _context.Proveedores
                .Select(p => new ProveedorDTO
                {
                    Proveedor_ID = p.Proveedor_ID,
                    Nombre = p.Nombre,
                    Telefono = p.Telefono,
                    Email = p.Email
                })
                .FirstOrDefaultAsync(p => p.Proveedor_ID == id);

            if (proveedor == null)
            {
                return NotFound(); // Devuelve 404 si no se encuentra el proveedor
            }
            return Ok(proveedor);
        }
        */
        // POST: api/proveedores (Alta de un nuevo proveedor)
        [HttpPost]
        public async Task<IActionResult> AltaProveedor([FromBody] ProveedorDTO nuevoProveedor)
        {
            if (nuevoProveedor == null)
                return BadRequest("Datos de proveedor inválidos.");

            try
            {
                await _proveedoresLogic.AltaProveedor(nuevoProveedor);
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
        /*public async Task<ActionResult<ProveedorDTO>> Post([FromBody] ProveedorDTO nuevoProveedor)
        {
            var proveedor = new Proveedores
            {
                Nombre = nuevoProveedor.Nombre,
                Telefono = nuevoProveedor.Telefono,
                Email = nuevoProveedor.Email
            };

            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();

            nuevoProveedor.Proveedor_ID = proveedor.Proveedor_ID; // Asignar el ID generado
            return CreatedAtAction(nameof(Get), new { id = proveedor.Proveedor_ID }, nuevoProveedor);
        }
        */

        // PUT: api/proveedores/{id} (Modificar un proveedor existente)
        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarProveedor(int id, [FromBody] ProveedorDTO proveedorModificado)
        {
            if (proveedorModificado == null)
                return BadRequest("Datos de proveedor inválidos.");

            try
            {
                await _proveedoresLogic.ModificarProveedor(id, proveedorModificado);
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
        /* public async Task<ActionResult> Put(int id, [FromBody] ProveedorDTO proveedorModificado)
         {
             var proveedor = await _context.Proveedores.FindAsync(id);
             if (proveedor == null)
             {
                 return NotFound(); // Devuelve 404 si no se encuentra el proveedor
             }

             // Actualizamos los datos del proveedor
             proveedor.Nombre = proveedorModificado.Nombre;
             proveedor.Telefono = proveedorModificado.Telefono;
             proveedor.Email = proveedorModificado.Email;

             await _context.SaveChangesAsync();

             return NoContent(); // Devuelve 204 No Content
         }
        */

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
        /* public async Task<ActionResult> Delete(int id)
         {
             var proveedor = await _context.Proveedores.FindAsync(id);
             if (proveedor == null)
             {
                 return NotFound(); // Devuelve 404 si no se encuentra el proveedor
             }

             _context.Proveedores.Remove(proveedor);
             await _context.SaveChangesAsync();

             return NoContent(); // Devuelve 204 No Content
         }
        */
    }

}
