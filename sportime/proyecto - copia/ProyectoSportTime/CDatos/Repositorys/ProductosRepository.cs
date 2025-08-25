using CDatos.Data;
using CDatos.Repositorys.IRepositorys;
using Shared.Dtos;
using System;
using Shared.Entidades;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CDatos.Repositorys
{
    public class ProductosRepository : IProductoRepository
    {
        private readonly ProyectoDbContext _context;

        public ProductosRepository(ProyectoDbContext context)
        {
            _context = context;
        }
        // Crear un nuevo producto
        public async Task CrearProducto(ProductoDTO producto)
        {
            ArgumentNullException.ThrowIfNull(producto);
            var nuevoProducto = new Productos
            {
                Tipo = producto.Tipo,
                Precio = producto.Precio,
                Descripcion = producto.Descripcion
            };
            _context.Productos.Add(nuevoProducto);
            await _context.SaveChangesAsync();
        }
        /* public static async Task CreateProducto(ProductoDTO productoDto)
        {
            ArgumentNullException.ThrowIfNull(productoDto);

            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint("/api/productos");
            var content = new StringContent(JsonConvert.SerializeObject(productoDto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
        }
        */

        // Actualizar un producto existente
        public async Task ModificarProducto(int productoID, ProductoDTO productoModificado)
        {
            ArgumentNullException.ThrowIfNull(productoModificado);
            var producto = await _context.Productos.FindAsync(productoID);
            if (producto == null)
                throw new KeyNotFoundException("Producto no encontrado.");
            producto.Tipo = productoModificado.Tipo;
            producto.Precio = productoModificado.Precio;
            producto.Descripcion = productoModificado.Descripcion;
            await _context.SaveChangesAsync();
        }
        /* public static async Task UpdateProducto(int productoID, ProductoDTO productoModificadoDto)
        {
            ArgumentNullException.ThrowIfNull(productoModificadoDto);

            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint($"/api/productos/{productoID}");
            var content = new StringContent(JsonConvert.SerializeObject(productoModificadoDto), Encoding.UTF8, "application/json");

            var response = await client.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
        }
        */

        // Eliminar un producto
        public async Task EliminarProducto(int productoID)
        {
            var producto = await _context.Productos.FindAsync(productoID);
            if (producto == null)
                throw new KeyNotFoundException("Producto no encontrado.");

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
        }

        /* public static async Task DeleteProducto(int productoID)
        {
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint($"/api/productos/{productoID}");

            var response = await client.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
        }
        */

        // Obtener todos los productos
        public async Task<List<ProductoDTO>> ObtenerTodosLosProductos()
        {
            return await _context.Productos
                .Select(p => new ProductoDTO
                {
                    Producto_ID = p.Producto_ID,
                    Tipo = p.Tipo,
                    Precio = p.Precio,
                    Descripcion = p.Descripcion
                })
                .ToListAsync();
        }
        /* public static async Task<List<ProductoDTO>> GetAllProductos()
        {
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint("/api/productos");

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ProductoDTO>>(result);
        }
        */

        // Obtener un producto por su ID
        public async Task<ProductoDTO?> ObtenerProductoPorId(int productoID)
        {
            var producto = await _context.Productos.FindAsync(productoID);
            if (producto == null)
                return null;
            return new ProductoDTO
            {
                Producto_ID = producto.Producto_ID,
                Tipo = producto.Tipo,
                Precio = producto.Precio,
                Descripcion = producto.Descripcion
            };
        }
        /*public static async Task<ProductoDTO?> GetProductoById(int id)
        {
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint($"/api/productos/{id}");

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ProductoDTO>(result);
        }
        */
    }

}
