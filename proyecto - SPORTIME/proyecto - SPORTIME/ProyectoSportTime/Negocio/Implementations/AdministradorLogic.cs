using API.Data;
using Microsoft.EntityFrameworkCore;
using Negocio.Contracts;
using Negocio.Repositorys;
using Shared.Dtos;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Implementations
{
    public class AdministradorLogic
    {
        public async Task<(bool success, string message)> AltaAdministrador(AdministradorDTO nuevoAdmin)
        {
            ArgumentNullException.ThrowIfNull(nuevoAdmin);
            ValidarAdministrador(nuevoAdmin);

            // Validar duplicados
            var administradores = await AdministradorRepository.GetAllAdministradores();
            bool existeDuplicado = administradores.Any(a =>
                a.Email.Equals(nuevoAdmin.Email, StringComparison.OrdinalIgnoreCase)
            );

            if (existeDuplicado)
            {
                return (false, "Ya existe un administrador con el mismo correo electrónico.");
            }

            try
            {
                await AdministradorRepository.CreateAdministrador(nuevoAdmin);
            }
            catch (Exception ex)
            {
                return (false, $"Ocurrió un error al guardar el administrador: {ex.Message}");
            }

            return (true, "Administrador guardado correctamente.");
        }

        public async Task ModificarAdministrador(int adminID, AdministradorDTO administradorModificado)
        {
            ArgumentNullException.ThrowIfNull(administradorModificado);
            if (adminID <= 0) throw new ArgumentException("El ID debe ser mayor a cero.");
            ValidarAdministrador(administradorModificado);

            // Obtener todos los administradores excepto el actual
            var administradores = await ObtenerTodosLosAdministradores();
            bool existeDuplicado = administradores.Any(a =>
                a.Admin_ID != adminID &&  // Ignorar el administrador actual
                a.Email.Equals(administradorModificado.Email, StringComparison.OrdinalIgnoreCase)
            );

            if (existeDuplicado)
            {
                throw new ArgumentException("Ya existe un administrador con el mismo correo electrónico.");
            }

            await AdministradorRepository.UpdateAdministrador(adminID, administradorModificado);
        }

        public async Task BajaAdministrador(int adminID)
        {
            if (adminID <= 0) throw new ArgumentException("El ID debe ser mayor a cero.");

            var admin = await AdministradorRepository.GetAdministradorById(adminID);
            if (admin == null)
            {
                throw new ArgumentException($"El administrador con ID {adminID} no existe.");
            }

            await AdministradorRepository.DeleteAdministrador(adminID);
        }

        public async Task<List<AdministradorDTO>> ObtenerTodosLosAdministradores() => await AdministradorRepository.GetAllAdministradores();

        public async Task<AdministradorDTO?> ObtenerAdministradorPorId(int adminID)
        {
            if (adminID <= 0) throw new ArgumentException("El ID debe ser mayor a cero.");
            return await AdministradorRepository.GetAdministradorById(adminID);
        }

        private void ValidarAdministrador(AdministradorDTO admin)
        {
            if (string.IsNullOrEmpty(admin.Nombre))
            {
                throw new ArgumentException("El nombre del administrador no puede estar vacío.");
            }
            if (string.IsNullOrEmpty(admin.Email) || !IsValidEmail(admin.Email))
            {
                throw new ArgumentException("El correo electrónico es inválido.");
            }
        }

        // Validar si el email tiene un formato adecuado
        private bool IsValidEmail(string email)
        {
            try
            {
                var mail = new System.Net.Mail.MailAddress(email);
                return mail.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }


}

