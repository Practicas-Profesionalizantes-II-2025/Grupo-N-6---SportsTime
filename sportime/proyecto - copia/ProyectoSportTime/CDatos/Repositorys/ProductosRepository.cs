using Negocio.ClienteHttp;
using Newtonsoft.Json;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Repositorys
{
    public class ProductosRepository
    {
        // Crear un nuevo producto
        public static async Task CreateProducto(ProductoDTO productoDto)
        {
            ArgumentNullException.ThrowIfNull(productoDto);

            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint("/api/productos");
            var content = new StringContent(JsonConvert.SerializeObject(productoDto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
        }

        // Actualizar un producto existente
        public static async Task UpdateProducto(int productoID, ProductoDTO productoModificadoDto)
        {
            ArgumentNullException.ThrowIfNull(productoModificadoDto);

            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint($"/api/productos/{productoID}");
            var content = new StringContent(JsonConvert.SerializeObject(productoModificadoDto), Encoding.UTF8, "application/json");

            var response = await client.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
        }

        // Eliminar un producto
        public static async Task DeleteProducto(int productoID)
        {
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint($"/api/productos/{productoID}");

            var response = await client.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
        }

        // Obtener todos los productos
        public static async Task<List<ProductoDTO>> GetAllProductos()
        {
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint("/api/productos");

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ProductoDTO>>(result);
        }

        // Obtener un producto por su ID
        public static async Task<ProductoDTO?> GetProductoById(int id)
        {
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint($"/api/productos/{id}");

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ProductoDTO>(result);
        }
    }

}
