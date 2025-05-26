using Microsoft.EntityFrameworkCore;
using Negocio.Contracts;
using API.Data;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio.Repositorys;
using Shared.Dtos;

namespace Negocio.Implementations
{
    public class ProductosLogic 
    {
        public async Task AltaProducto(ProductoDTO nuevoProducto)
        {
            ArgumentNullException.ThrowIfNull(nuevoProducto);

            await ProductosRepository.CreateProducto(nuevoProducto);  // Llamamos al repositorio para crear el producto
        }

        public async Task ModificarProducto(int productoID, ProductoDTO productoModificado)
        {
            ArgumentNullException.ThrowIfNull(productoModificado);

            if (productoID <= 0)
            {
                throw new ArgumentException("Id debe ser mayor a cero");
            }

            await ProductosRepository.UpdateProducto(productoID, productoModificado);  // Llamamos al repositorio para modificar el producto
        }

        public async Task BajaProducto(int productoID)
        {
            if (productoID <= 0)
            {
                throw new ArgumentException("Id debe ser mayor a cero");
            }

            await ProductosRepository.DeleteProducto(productoID);  // Llamamos al repositorio para eliminar el producto
        }

        public async Task<List<ProductoDTO>> ObtenerTodosLosProductos()
        {
            return await ProductosRepository.GetAllProductos();  // Llamamos al repositorio para obtener todos los productos
        }

        public async Task<ProductoDTO?> ObtenerProductoPorId(int productoID)
        {
            if (productoID <= 0)
            {
                throw new ArgumentException("Id debe ser mayor a cero");
            }

            return await ProductosRepository.GetProductoById(productoID);  // Llamamos al repositorio para obtener un producto por ID
        }
    }

}
