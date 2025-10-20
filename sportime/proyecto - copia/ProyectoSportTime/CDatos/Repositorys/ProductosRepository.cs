using CDatos.Data;
using CDatos.Repositorys.IRepositorys;
using Shared.Dtos;
using Shared.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDatos.Repositorys
{
    public class ProductosRepository : IProductoRepository
    {
        private readonly DataContext _context;

        public ProductosRepository(DataContext context)
        {
            _context = context;
        }
        // Crear un nuevo producto
        public async Task CrearProducto(ProductoDTO producto)
        {
            ArgumentNullException.ThrowIfNull(producto);
            var nuevoProducto = new Productos
            {
                TipoProducto = producto.TipoProducto,
                Proveedor_ID = producto.Proveedor_ID,
                Precio = producto.Precio
            };
            _context.Productos.Add(nuevoProducto);
            await _context.SaveChangesAsync();
            producto.Producto_ID = nuevoProducto.Producto_ID;
        }

        // Actualizar un producto existente
        public async Task ModificarProducto(int productoID, ProductoDTO productoModificado)
        {
            ArgumentNullException.ThrowIfNull(productoModificado);
            var producto = await _context.Productos.FindAsync(productoID);
            if (producto == null)
                throw new KeyNotFoundException("Producto no encontrado.");
            producto.TipoProducto = productoModificado.TipoProducto;
            producto.Proveedor_ID = productoModificado.Proveedor_ID;
            producto.Precio = productoModificado.Precio;
            await _context.SaveChangesAsync();
        }

        // Eliminar un producto
        public async Task EliminarProducto(int productoID)
        {
            var producto = await _context.Productos.FindAsync(productoID);
            if (producto == null)
                throw new KeyNotFoundException("Producto no encontrado.");
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
        }

        // Obtener todos los productos
        public async Task<List<ProductoDTO>> ObtenerTodosLosProductos()
        {
            return await _context.Productos
                .Select(p => new ProductoDTO
                {
                    Producto_ID = p.Producto_ID,
                    TipoProducto = p.TipoProducto,
                    Proveedor_ID = p.Proveedor_ID,
                    Precio = p.Precio
                })
                .ToListAsync();
        }

        // Obtener un producto por su ID
        public async Task<ProductoDTO?> ObtenerProductoPorId(int productoID)
        {
            var producto = await _context.Productos.FindAsync(productoID);
            if (producto == null)
                return null;
            return new ProductoDTO
            {
                Producto_ID = producto.Producto_ID,
                TipoProducto = producto.TipoProducto,
                Proveedor_ID = producto.Proveedor_ID,
                Precio = producto.Precio
            };
        }
    }
}
