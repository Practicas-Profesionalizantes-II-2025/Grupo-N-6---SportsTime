using CDatos.Data;
using CDatos.Repositorys.IRepositorys;
using Microsoft.EntityFrameworkCore;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDatos.Repositorys
{
    public class ProveedoresRepository : IProveedoresRepository
    {
        private readonly DataContext _context;

        public ProveedoresRepository(DataContext context)
        {
            _context = context;
        }
        // Crear un nuevo proveedor
        public async Task<Proveedores> CrearProveedor(Proveedores proveedor)
        {
            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();
            return proveedor;
        }
        /* public static async Task CreateProveedor(ProveedorDTO proveedor)
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
        */

        // Actualizar un proveedor existente
        public void  ModificarProveedor(Proveedores proveedor)
        {
            ArgumentNullException.ThrowIfNull(proveedor);

            var proveedorExistente =  _context.Proveedores.Find(proveedor.Proveedor_ID);
            if (proveedorExistente == null)
            {
                throw new Exception("Proveedor no encontrado.");
            }

            proveedorExistente.Nombre = proveedor.Nombre;
            proveedorExistente.Telefono = proveedor.Telefono;
            proveedorExistente.Direccion = proveedor.Direccion;
            proveedorExistente.Email = proveedor.Email;

            _context.SaveChangesAsync();
        }
        /* public static async Task UpdateProveedor(int proveedorID, ProveedorDTO proveedorModificado)
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
        */

        // Eliminar un proveedor
        public void EliminarProveedor(int ProveedorID)
        {
            var Proveedor = _context.Proveedores.FirstOrDefault(x => x.Proveedor_ID == ProveedorID);
            if (Proveedor != null)
            {
                _context.Proveedores.Remove(Proveedor);
                _context.SaveChanges();
            }
        }
        /* public static async Task DeleteProveedor(int proveedorID)
         {
             var client = ApiServer.ObtenerClientHttp();
             var url = ApiServer.ObtenerUrlEndPoint($"/api/proveedores/{proveedorID}");

             var response = await client.DeleteAsync(url);
             response.EnsureSuccessStatusCode();
         }
        */

        // Obtener todos los proveedores
        public async Task<List<Proveedores>> ObtenerTodosLosProveedores()
        {
            return await _context.Proveedores.ToListAsync();
        }
        /*public static async Task<List<ProveedorDTO>> GetAllProveedores()
        {
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint("/api/proveedores");

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ProveedorDTO>>(result);
        }
        */
        // Obtener un proveedor por ID
        public async Task<Proveedores?> ObtenerProveedorPorId(int ProveedorID)
        {
            return await _context.Proveedores.FindAsync(ProveedorID);
        }
        /* public static async Task<ProveedorDTO?> GetProveedorById(int id)
        {
            var client = ApiServer.ObtenerClientHttp();
            var url = ApiServer.ObtenerUrlEndPoint($"/api/proveedores/{id}");

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ProveedorDTO>(result);
        }
        */
    }
}
