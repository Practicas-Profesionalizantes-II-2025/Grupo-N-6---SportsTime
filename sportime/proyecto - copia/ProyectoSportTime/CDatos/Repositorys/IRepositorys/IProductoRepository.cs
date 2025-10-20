using Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDatos.Repositorys.IRepositorys
{
    public interface IProductoRepository
    {
        Task CrearProducto(ProductoDTO producto);
        Task ModificarProducto(int productoID, ProductoDTO productoModificado);
        Task EliminarProducto(int productoID);
        Task<List<ProductoDTO>> ObtenerTodosLosProductos();
        Task<ProductoDTO?> ObtenerProductoPorId(int id);
    }
}
