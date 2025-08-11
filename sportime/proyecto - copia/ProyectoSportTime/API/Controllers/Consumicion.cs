using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Shared.Entidades;
using CDatos.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsumicionesController : ControllerBase
    {
        private readonly ProyectoDbContext _context;

        // Constructor donde se inyecta el DbContext
        public ConsumicionesController(ProyectoDbContext context)
        {
            _context = context;
        }

        // GET: api/consumiciones (Obtener todas las consumiciones)
        [HttpGet]
        public async Task<ActionResult<List<ConsumicionDTO>>> Get()
        {
            var consumiciones = await _context.Consumiciones
                .Include(c => c.ConsumicionProductos)  // Incluir los productos asociados
                .ThenInclude(cp => cp.Producto)        // Incluir los productos
                .ToListAsync();

            var consumicionDtos = consumiciones.Select(c => new ConsumicionDTO
            {
                Consumicion_ID = c.Consumicion_ID,
                Cantidad = c.Cantidad,
                Precio = c.Precio,
                Productos = c.ConsumicionProductos.Select(cp => new ProductoDTO
                {
                    Producto_ID = cp.Producto.Producto_ID,
                    Tipo = cp.Producto.Tipo,
                    Descripcion = cp.Producto.Descripcion,
                    Proveedor_ID = cp.Producto.Proveedor_ID
                }).ToList()
            }).ToList();

            return Ok(consumicionDtos);
        }

        // GET: api/consumiciones/{id} (Obtener una consumición específica por ID)
        [HttpGet("{id}")]
        public async Task<ActionResult<ConsumicionDTO>> Get(int id)
        {
            var consumicion = await _context.Consumiciones
                .Include(c => c.ConsumicionProductos)  // Incluir los productos asociados
                .ThenInclude(cp => cp.Producto)        // Incluir los productos
                .FirstOrDefaultAsync(c => c.Consumicion_ID == id);

            if (consumicion == null)
            {
                return NotFound(); // Devuelve 404 si no se encuentra la consumición
            }

            var consumicionDto = new ConsumicionDTO
            {
                Consumicion_ID = consumicion.Consumicion_ID,
                Cantidad = consumicion.Cantidad,
                Precio = consumicion.Precio,
                Productos = consumicion.ConsumicionProductos.Select(cp => new ProductoDTO
                {
                    Producto_ID = cp.Producto.Producto_ID,
                    Tipo = cp.Producto.Tipo,
                    Descripcion = cp.Producto.Descripcion,
                    Proveedor_ID = cp.Producto.Proveedor_ID
                }).ToList()
            };

            return Ok(consumicionDto);
        }

        // POST: api/consumiciones (Alta de una nueva consumición)
        [HttpPost]
        public async Task<ActionResult<ConsumicionDTO>> Post([FromBody] ConsumicionDTO nuevaConsumicionDto)
        {
            var nuevaConsumicion = new Consumiciones
            {
                Cantidad = nuevaConsumicionDto.Cantidad,
                Precio = nuevaConsumicionDto.Precio
            };

            // Asociar los productos a la consumición
            foreach (var productoDto in nuevaConsumicionDto.Productos)
            {
                var producto = await _context.Productos.FindAsync(productoDto.Producto_ID);
                if (producto == null)
                {
                    return NotFound($"Producto con ID {productoDto.Producto_ID} no encontrado.");
                }

                // Crear una entrada en la tabla intermedia
                nuevaConsumicion.ConsumicionProductos.Add(new ConsumicionProducto
                {
                    Producto = producto,
                    Consumicion = nuevaConsumicion
                });
            }

            _context.Consumiciones.Add(nuevaConsumicion);
            await _context.SaveChangesAsync();

            nuevaConsumicionDto.Consumicion_ID = nuevaConsumicion.Consumicion_ID; // Asignar el ID generado al DTO
            return CreatedAtAction(nameof(Get), new { id = nuevaConsumicion.Consumicion_ID }, nuevaConsumicionDto);
        }

        // PUT: api/consumiciones/{id} (Modificar una consumición existente)
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ConsumicionDTO consumicionModificadaDto)
        {
            var consumicion = await _context.Consumiciones
                .Include(c => c.ConsumicionProductos)  // Incluir los productos asociados
                .FirstOrDefaultAsync(c => c.Consumicion_ID == id);

            if (consumicion == null)
            {
                return NotFound(); // Devuelve 404 si no se encuentra la consumición
            }

            // Actualizamos los datos de la consumición
            consumicion.Cantidad = consumicionModificadaDto.Cantidad;
            consumicion.Precio = consumicionModificadaDto.Precio;

            // Limpiar productos antiguos
            _context.consumicionProductos.RemoveRange(consumicion.ConsumicionProductos);

            // Asociar los nuevos productos a la consumición
            foreach (var productoDto in consumicionModificadaDto.Productos)
            {
                var producto = await _context.Productos.FindAsync(productoDto.Producto_ID);
                if (producto == null)
                {
                    return NotFound($"Producto con ID {productoDto.Producto_ID} no encontrado.");
                }

                // Crear una nueva entrada en la tabla intermedia
                consumicion.ConsumicionProductos.Add(new ConsumicionProducto
                {
                    Producto = producto,
                    Consumicion = consumicion
                });
            }

            await _context.SaveChangesAsync();

            return NoContent(); // Devuelve 204 No Content
        }

        // DELETE: api/consumiciones/{id} (Eliminar una consumición)
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var consumicion = await _context.Consumiciones
                .Include(c => c.ConsumicionProductos)  // Incluir los productos asociados
                .FirstOrDefaultAsync(c => c.Consumicion_ID == id);

            if (consumicion == null)
            {
                return NotFound(); // Devuelve 404 si no se encuentra la consumición
            }

            _context.consumicionProductos.RemoveRange(consumicion.ConsumicionProductos); // Eliminar las relaciones en la tabla intermedia
            _context.Consumiciones.Remove(consumicion);
            await _context.SaveChangesAsync();

            return NoContent(); // Devuelve 204 No Content
        }
    }
}