using Microsoft.EntityFrameworkCore;
using CNegocio.Contracts;
using CDatos.Repositorys.IRepositorys;
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
            if (string.IsNullOrWhiteSpace(nuevoProducto.TipoProducto))
                throw new ArgumentException("El tipo de producto es obligatorio.");
            if (nuevoProducto.Precio < 0)
                throw new ArgumentException("El precio no puede ser negativo.");
            if (nuevoProducto.Proveedor_ID <= 0)
                throw new ArgumentException("Debe seleccionar un proveedor válido.");

            await _repo.CrearProducto(nuevoProducto);
        }

        public async Task ModificarProducto(int productoID, ProductoDTO productoModificado)
        {
            ArgumentNullException.ThrowIfNull(productoModificado);
            if (productoID <= 0) throw new ArgumentException("Id inválido");
            if (string.IsNullOrWhiteSpace(productoModificado.TipoProducto))
                throw new ArgumentException("El tipo de producto es obligatorio.");
            if (productoModificado.Precio < 0)
                throw new ArgumentException("El precio no puede ser negativo.");
            if (productoModificado.Proveedor_ID <= 0)
                throw new ArgumentException("Debe seleccionar un proveedor válido.");

            await _repo.ModificarProducto(productoID, productoModificado);
        }

        public async Task BajaProducto(int productoID)
        {
            if (productoID <= 0) throw new ArgumentException("Id inválido");
            await _repo.EliminarProducto(productoID);
        }

        public Task<List<ProductoDTO>> ObtenerTodosLosProductos()
            => _repo.ObtenerTodosLosProductos();

        public Task<ProductoDTO?> ObtenerProductoPorId(int id)
            => _repo.ObtenerProductoPorId(id);
    }
}
