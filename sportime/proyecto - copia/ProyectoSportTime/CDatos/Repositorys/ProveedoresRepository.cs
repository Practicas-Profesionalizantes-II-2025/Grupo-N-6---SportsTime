using CDatos.Data;
using CDatos.Repositorys.IRepositorys;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
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
        private readonly ProyectoDbContext _context;

        public ProveedoresRepository(ProyectoDbContext context)
        {
            _context = context;
        }
        // Crear un nuevo proveedor
        public async Task CrearProveedor(ProveedorDTO proveedor)
        {
            ArgumentNullException.ThrowIfNull(proveedor);

            var nuevoProveedor = new Proveedores
            {
                Nombre = proveedor.Nombre,
                Telefono = proveedor.Telefono,
                Email = proveedor.Email
            };

            _context.Proveedores.Add(nuevoProveedor);
            await _context.SaveChangesAsync();
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
        public async Task ModificarProveedor(int proveedorID, ProveedorDTO proveedorModificado)
        {
            ArgumentNullException.ThrowIfNull(proveedorModificado);

            var proveedor = await _context.Proveedores.FindAsync(proveedorID);
            if (proveedor == null)
                throw new KeyNotFoundException("Proveedor no encontrado.");

            proveedor.Nombre = proveedorModificado.Nombre;
            proveedor.Telefono = proveedorModificado.Telefono;
            proveedor.Email = proveedorModificado.Email;
            await _context.SaveChangesAsync();
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
        public async Task EliminarProveedor(int proveedorID)
        {
            var proveedor = await _context.Proveedores.FindAsync(proveedorID);
            if (proveedor == null)
                throw new KeyNotFoundException("Proveedor no encontrado.");

            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();
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
        public async Task<List<ProveedorDTO>> ObtenerTodosLosProveedores()
        {
            return await _context.Proveedores
                .Select(p => new ProveedorDTO
                {
                    Proveedor_ID = p.Proveedor_ID,
                    Nombre = p.Nombre,
                    Telefono = p.Telefono,
                    Email = p.Email
                })
                .ToListAsync();
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
        public async Task<ProveedorDTO?> ObtenerProveedorPorId(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null) return null;

            return new ProveedorDTO
            {
                Proveedor_ID = proveedor.Proveedor_ID,
                Nombre = proveedor.Nombre,
                Telefono = proveedor.Telefono,
                Email = proveedor.Email
            };
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
