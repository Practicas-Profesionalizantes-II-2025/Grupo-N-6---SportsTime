using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Shared.Entidades;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly ProyectoDbContext _context;

        // Constructor donde se inyecta el DbContext
        public ProductosController(ProyectoDbContext context)
        {
            _context = context;
        }

        // GET: api/productos (Obtener todos los productos)
        [HttpGet]
        public async Task<ActionResult<List<ProductoDTO>>> Get()
        {
            var productos = await _context.Productos.ToListAsync();
            return Ok(productos.Select(p => new ProductoDTO
            {
                Producto_ID = p.Producto_ID,
                Tipo = p.Tipo,
                Descripcion = p.Descripcion,
                Proveedor_ID = p.Proveedor_ID
            }).ToList());
        }

        // GET: api/productos/{id} (Obtener un producto específico por ID)
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDTO>> Get(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound(); // Devuelve 404 si no se encuentra el producto
            }

            return Ok(new ProductoDTO
            {
                Producto_ID = producto.Producto_ID,
                Tipo = producto.Tipo,
                Descripcion = producto.Descripcion,
                Proveedor_ID = producto.Proveedor_ID
            });
        }

        // POST: api/productos (Alta de un nuevo producto)
        [HttpPost]
        public async Task<ActionResult<ProductoDTO>> Post([FromBody] ProductoDTO nuevoProducto)
        {
            var producto = new Productos // Asegúrate de que el modelo sea el correcto
            {
                Tipo = nuevoProducto.Tipo,
                Descripcion = nuevoProducto.Descripcion,
                Proveedor_ID = nuevoProducto.Proveedor_ID
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = producto.Producto_ID }, nuevoProducto);
        }

        // PUT: api/productos/{id} (Modificar un producto existente)
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductoDTO productoModificado)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound(); // Devuelve 404 si no se encuentra el producto
            }

            // Actualizamos los datos del producto
            producto.Tipo = productoModificado.Tipo;
            producto.Descripcion = productoModificado.Descripcion;
            producto.Proveedor_ID = productoModificado.Proveedor_ID;

            await _context.SaveChangesAsync();

            return NoContent(); // Devuelve 204 No Content
        }

        // DELETE: api/productos/{id} (Eliminar un producto)
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound(); // Devuelve 404 si no se encuentra el producto
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent(); // Devuelve 204 No Content
        }
    }

}