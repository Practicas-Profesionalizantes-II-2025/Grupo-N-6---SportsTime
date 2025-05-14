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
    public class ProveedoresRepository
    {
        public static async Task CreateProveedor(ProveedorDTO proveedor)
        {
            ArgumentNullException.ThrowIfNull(proveedor);

            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint("/api/proveedores/");
            var content = new StringContent(JsonConvert.SerializeObject(new
            {
                proveedor.Nombre,
                proveedor.Telefono,
                proveedor.Email
            }), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
        }

        // Actualizar un proveedor existente
        public static async Task UpdateProveedor(int proveedorID, ProveedorDTO proveedorModificado)
        {
            ArgumentNullException.ThrowIfNull(proveedorModificado);

            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint($"/api/proveedores/{proveedorID}");
            var content = new StringContent(JsonConvert.SerializeObject(new
            {
                proveedorModificado.Nombre,
                proveedorModificado.Telefono,
                proveedorModificado.Email
            }), Encoding.UTF8, "application/json");

            var response = await client.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
        }

        // Eliminar un proveedor
        public static async Task DeleteProveedor(int proveedorID)
        {
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint($"/api/proveedores/{proveedorID}");

            var response = await client.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
        }

        // Obtener todos los proveedores
        public static async Task<List<ProveedorDTO>> GetAllProveedores()
        {
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint("/api/proveedores");

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ProveedorDTO>>(result);
        }

        // Obtener un proveedor por ID
        public static async Task<ProveedorDTO?> GetProveedorById(int id)
        {
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint($"/api/proveedores/{id}");

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ProveedorDTO>(result);
        }
    }
}
