using CDatos.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Entidades;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumisionProdController : ControllerBase
    {
        private readonly ProyectoDbContext _context;

        public ConsumisionProdController(ProyectoDbContext context)
        {
            _context = context;
        }

        // POST: api/consumisionprod (Agregar un producto a una consumición)
        [HttpPost]
        public async Task<ActionResult> AddProduct([FromBody] ConsumicionProductoDTO consumicionDTO)
        {
            // Verificar que el producto y la consumición existan
            var existProduct = await _context.Productos.AnyAsync(p => p.Producto_ID == consumicionDTO.Producto_ID);
            var existConsumicion = await _context.Consumiciones.AnyAsync(c => c.Consumicion_ID == consumicionDTO.Consumicion_ID);

            if (existConsumicion && existProduct)
            {
                // Crear una nueva instancia de ConsumicionProducto
                var consProduct = new ConsumicionProducto
                {
                    Consumicion_ID = consumicionDTO.Consumicion_ID,
                    Producto_ID = consumicionDTO.Producto_ID,
                    Cantidad = consumicionDTO.Cantidad // Aquí agregamos la cantidad del producto
                };

                // Agregar el producto a la consumición
                _context.consumicionProductos.Add(consProduct);
                await _context.SaveChangesAsync();

                return Ok("Producto agregado correctamente");
            }
            return NotFound("La consumición o el producto seleccionado no existe");
        }
    }

}
