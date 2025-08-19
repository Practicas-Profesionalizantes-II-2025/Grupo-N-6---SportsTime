//using Microsoft.AspNetCore.Mvc;
//using Negocio.ClienteHttp;
//using Newtonsoft.Json;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDatos.Data;
using Shared.Entidades;
using Shared.Dtos;
using Microsoft.EntityFrameworkCore;
using CDatos.Repositorys.IRepositorys;

namespace CDatos.Repositorys
{
    public class AdministradorRepository : IAdministradorRepository
    {
        //private static readonly HttpClient client = ApiServer.ObtenerClientHttp(); // Instancia global de HttpClient

        private readonly ProyectoDbContext _context;

        public AdministradorRepository(ProyectoDbContext context)
        {
            _context = context;
        }
        // Crear un nuevo administrador
        public async Task CreateAdministrador(AdministradorDTO adminDTO)
        {
            ArgumentNullException.ThrowIfNull(adminDTO);

            //var url = ApiServer.ObtenerUrlEndPoint("/api/administrador/singup");
            //var content = new StringContent(JsonConvert.SerializeObject(new
            var admin = new Administrador
            {
                Email = adminDTO.Email,
                Nombre = adminDTO.Nombre,
                Contraseña = adminDTO.PasswordHash,
                IsSuperAdmin = adminDTO.IsSuperAdmin
            };
            _context.Administradores.Add(admin);
            await _context.SaveChangesAsync();
        }

        // Obtener todos los administradores
        public async Task<List<AdministradorDTO>> GetAllAdministradores()
        {
            return await _context.Administradores
              .Select(a => new AdministradorDTO
              {
                  Admin_ID = a.Admin_ID,
                  Nombre = a.Nombre,
                  Email = a.Email,
                  IsSuperAdmin = a.IsSuperAdmin
              })
              .ToListAsync();

            /*var url = ApiServer.ObtenerUrlEndPoint("/api/administrador/get");

            try
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode(); // Asegura que la respuesta sea exitosa

                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<AdministradorDTO>>(result);
            }
            catch (Exception ex)
            {
                // Manejar error
                throw new ApplicationException($"Error al obtener administradores: {ex.Message}");
            }
            */
        }

        // Actualizar un administrador
        public async Task UpdateAdministrador(int adminID, AdministradorDTO updatedAdmin)
        {
            var admin = await _context.Administradores.FindAsync(adminID);
            if (admin == null) throw new KeyNotFoundException("Administrador no encontrado");

            admin.Nombre = updatedAdmin.Nombre;
            admin.Email = updatedAdmin.Email;
            if (!string.IsNullOrEmpty(updatedAdmin.PasswordHash))
                admin.Contraseña = updatedAdmin.PasswordHash; // Hashear si corresponde
            admin.IsSuperAdmin = updatedAdmin.IsSuperAdmin;

            await _context.SaveChangesAsync();

            /* ArgumentNullException.ThrowIfNull(updatedAdmin);

             var url = ApiServer.ObtenerUrlEndPoint($"/api/administrador/{adminID}");
             var content = new StringContent(JsonConvert.SerializeObject(new
             {
                 Nombre = updatedAdmin.Nombre,
                 Email = updatedAdmin.Email,
                 PasswordHash = updatedAdmin.PasswordHash,
                 IsSuperAdmin = updatedAdmin.IsSuperAdmin
             }), Encoding.UTF8, "application/json");

             try
             {
                 var response = await client.PutAsync(url, content);
                 response.EnsureSuccessStatusCode();
             }
             catch (Exception ex)
             {
                 // Manejar error
                 throw new ApplicationException($"Error al actualizar el administrador {adminID}: {ex.Message}");
             }
             */
        }

        // Eliminar un administrador
        public async Task DeleteAdministrador(int adminID)
        {
            var admin = await _context.Administradores.FindAsync(adminID);
            if (admin == null) throw new KeyNotFoundException("Administrador no encontrado");

            _context.Administradores.Remove(admin);
            await _context.SaveChangesAsync();

            /* var url = ApiServer.ObtenerUrlEndPoint($"/api/administrador/{adminID}");

             try
             {
                 var response = await client.DeleteAsync(url);
                 response.EnsureSuccessStatusCode();
             }
             catch (Exception ex)
             {
                 // Manejar error
                 throw new ApplicationException($"Error al eliminar el administrador {adminID}: {ex.Message}");
             }
             */
        }

        // Obtener un administrador por ID
        public async Task<AdministradorDTO?> GetAdministradorById(int id)
        {
            var admin = await _context.Administradores.FindAsync(id);
            if (admin == null) return null;

            return new AdministradorDTO
            {
                Admin_ID = admin.Admin_ID,
                Nombre = admin.Nombre,
                Email = admin.Email,
                IsSuperAdmin = admin.IsSuperAdmin
            };
            /*var url = ApiServer.ObtenerUrlEndPoint($"/api/administrador/{id}");

            try
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AdministradorDTO>(result);
            }
            catch (Exception ex)
            {
                // Manejar error
                throw new ApplicationException($"Error al obtener el administrador con ID {id}: {ex.Message}");
            }
            */
        }

        public async Task<AdministradorDTO?> GetAdministradorByEmail(string email)
        {
            var admin = await _context.Administradores
                .FirstOrDefaultAsync(a => a.Email == email);

            if (admin == null) return null;

            return new AdministradorDTO
            {
                Admin_ID = admin.Admin_ID,
                Nombre = admin.Nombre,
                Email = admin.Email,
                PasswordHash = admin.Contraseña,
                // Otros campos si es necesario
            };
        }
    }

}
