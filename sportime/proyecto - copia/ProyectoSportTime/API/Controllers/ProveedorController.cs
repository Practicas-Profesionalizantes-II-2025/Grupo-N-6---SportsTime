using API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.Entidades;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedoresController : ControllerBase
    {
        private readonly ProyectoDbContext _context;

        // Constructor donde se inyecta el DbContext
        public ProveedoresController(ProyectoDbContext context)
        {
            _context = context;
        }

        // GET: api/proveedores (Obtener todos los proveedores)
        [HttpGet]
        public async Task<ActionResult<List<ProveedorDTO>>> Get()
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

        // GET: api/proveedores/{id} (Obtener un proveedor específico por ID)
        [HttpGet("{id}")]
        public async Task<ActionResult<ProveedorDTO>> Get(int id)
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

        // POST: api/proveedores (Alta de un nuevo proveedor)
        [HttpPost]
        public async Task<ActionResult<ProveedorDTO>> Post([FromBody] ProveedorDTO nuevoProveedor)
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

        // PUT: api/proveedores/{id} (Modificar un proveedor existente)
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProveedorDTO proveedorModificado)
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

        // DELETE: api/proveedores/{id} (Eliminar un proveedor)
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
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
    }

}
