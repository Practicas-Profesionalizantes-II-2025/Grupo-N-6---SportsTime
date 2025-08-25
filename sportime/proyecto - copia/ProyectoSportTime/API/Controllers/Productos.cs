using CDatos.Data;
using CNegocio.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductos _productosLogic;

        public ProductosController(IProductos productosLogic)
        {
            _productosLogic = productosLogic;
        }
        /*  private readonly ProyectoDbContext _context;

         // Constructor donde se inyecta el DbContext
         public ProductosController(ProyectoDbContext context)
         {
             _context = context;
         }
        */
        // GET: api/productos (Obtener todos los productos)
        [HttpGet]
        public async Task<IActionResult> ObtenerTodosLosProductos()
        {
            try
            {
                var productos = await _productosLogic.ObtenerTodosLosProductos();
                return Ok(productos);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al obtener los productos.");
            }
        }
        /*public async Task<ActionResult<List<ProductoDTO>>> Get()
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
        */
        // GET: api/productos/{id} (Obtener un producto específico por ID)
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerProductoPorId(int id)
        {
            try
            {
                var producto = await _productosLogic.ObtenerProductoPorId(id);
                if (producto == null)
                    return NotFound("Producto no encontrado.");

                return Ok(producto);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al obtener el producto.");
            }
        }
        /* public async Task<ActionResult<ProductoDTO>> Get(int id)
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
        */

        // POST: api/productos (Alta de un nuevo producto)
        [HttpPost]
        public async Task<IActionResult> AltaProducto([FromBody] ProductoDTO nuevoProducto)
        {
            if (nuevoProducto == null)
                return BadRequest("Datos de producto inválidos.");

            try
            {
                await _productosLogic.AltaProducto(nuevoProducto);
                return Ok("Producto creado correctamente.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al crear el producto.");
            }
        }
        /* public async Task<ActionResult<ProductoDTO>> Post([FromBody] ProductoDTO nuevoProducto)
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
        */

        // PUT: api/productos/{id} (Modificar un producto existente)
        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarProducto(int id, [FromBody] ProductoDTO productoModificado)
        {
            if (productoModificado == null)
                return BadRequest("Datos de producto inválidos.");

            try
            {
                await _productosLogic.ModificarProducto(id, productoModificado);
                return Ok("Producto actualizado correctamente.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Producto no encontrado.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al actualizar el producto.");
            }
        }
        /*public async Task<ActionResult> Put(int id, [FromBody] ProductoDTO productoModificado)
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
        */
        // DELETE: api/productos/{id} (Eliminar un producto)
        [HttpDelete("{id}")]
        public async Task<IActionResult> BajaProducto(int id)
        {
            try
            {
                await _productosLogic.BajaProducto(id);
                return Ok("Producto eliminado correctamente.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Producto no encontrado.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al eliminar el producto.");
            }
        }
        /* public async Task<ActionResult> Delete(int id)
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
        */
    }

}