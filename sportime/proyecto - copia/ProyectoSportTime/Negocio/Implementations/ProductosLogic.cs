using Microsoft.EntityFrameworkCore;
using CNegocio.Contracts;
using CDatos.Repositorys.IRepositorys;   
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos;

namespace CNegocio.Implementations
{
    public class ProductosLogic : IProductos
    {
        private readonly IProductoRepository _repo;

        public ProductosLogic(IProductoRepository repo)
        {
            _repo = repo;
        }
        public async Task AltaProducto(ProductoDTO nuevoProducto)
        {
            ArgumentNullException.ThrowIfNull(nuevoProducto);

            if (string.IsNullOrWhiteSpace(nuevoProducto.Descripcion))
                throw new ArgumentException("La descripción no puede estar vacía.");

            if (nuevoProducto.Precio < 0)
                throw new ArgumentException("El precio no puede ser negativo.");

            await _repo.CrearProducto(nuevoProducto);

        }

        public async Task ModificarProducto(int productoID, ProductoDTO productoModificado)
        {
            ArgumentNullException.ThrowIfNull(productoModificado);

            if (productoID <= 0)
            {
                throw new ArgumentException("Id debe ser mayor a cero");
            }

            await _repo.ModificarProducto(productoID, productoModificado);  // Llamamos al repositorio para modificar el producto
        }

        public async Task BajaProducto(int productoID)
        {
            if (productoID <= 0)
            {
                throw new ArgumentException("Id debe ser mayor a cero");
            }

            await _repo.EliminarProducto(productoID);  // Llamamos al repositorio para eliminar el producto
        }

        public async Task<List<ProductoDTO>> ObtenerTodosLosProductos()
        {
            return await _repo.ObtenerTodosLosProductos();  // Llamamos al repositorio para obtener todos los productos
        }

        public async Task<ProductoDTO?> ObtenerProductoPorId(int productoID)
        {
            if (productoID <= 0)
            {
                throw new ArgumentException("Id debe ser mayor a cero");
            }

            return await _repo.ObtenerProductoPorId(productoID);  // Llamamos al repositorio para obtener un producto por ID
        }
    }

}
