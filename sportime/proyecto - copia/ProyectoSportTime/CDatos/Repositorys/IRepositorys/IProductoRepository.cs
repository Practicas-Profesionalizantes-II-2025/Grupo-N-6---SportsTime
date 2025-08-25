using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos;

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
