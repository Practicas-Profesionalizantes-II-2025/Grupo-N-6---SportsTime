using Microsoft.AspNetCore.Mvc;
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
    public class AdministradorRepository
    {
        private static readonly HttpClient client = ApiServer.ObtenerClientHttp(); // Instancia global de HttpClient

        // Crear un nuevo administrador
        public static async Task CreateAdministrador(AdministradorDTO adminDTO)
        {
            ArgumentNullException.ThrowIfNull(adminDTO);

            var url = ApiServer.ObtenerUrlEndPoint("/api/administrador/singup");
            var content = new StringContent(JsonConvert.SerializeObject(new
            {
                Email = adminDTO.Email,
                Nombre = adminDTO.Nombre,
                PasswordHash = adminDTO.PasswordHash,
                IsSuperAdmin = adminDTO.IsSuperAdmin
            }), Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode(); // Esto lanzará una excepción si el código no es 2xx
            }
            catch (Exception ex)
            {
                // Manejar error (por ejemplo, loguear o retornar un mensaje detallado)
                throw new ApplicationException($"Error al crear administrador: {ex.Message}");
            }
        }

        // Obtener todos los administradores
        public static async Task<List<AdministradorDTO>> GetAllAdministradores()
        {
            var url = ApiServer.ObtenerUrlEndPoint("/api/administrador/get");

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
        }

        // Actualizar un administrador
        public static async Task UpdateAdministrador(int adminID, AdministradorDTO updatedAdmin)
        {
            ArgumentNullException.ThrowIfNull(updatedAdmin);

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
        }

        // Eliminar un administrador
        public static async Task DeleteAdministrador(int adminID)
        {
            var url = ApiServer.ObtenerUrlEndPoint($"/api/administrador/{adminID}");

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
        }

        // Obtener un administrador por ID
        public static async Task<AdministradorDTO?> GetAdministradorById(int id)
        {
            var url = ApiServer.ObtenerUrlEndPoint($"/api/administrador/{id}");

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
        }
    }

}
