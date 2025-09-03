using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNegocio.Contracts
{
    public interface IProductos
    {
        Task AltaProducto(ProductoDTO nuevoProducto);
        Task ModificarProducto(int productoID, ProductoDTO productoModificado);
        Task BajaProducto(int productoID);
        Task<List<ProductoDTO>> ObtenerTodosLosProductos();
        Task<ProductoDTO?> ObtenerProductoPorId(int id);

    }
}
