using Microsoft.EntityFrameworkCore;
using Negocio.Contracts;
using API.Data;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio.Repositorys;
using Shared.Dtos;

namespace Negocio.Implementations
{
    public class ProveedoresLogic
    {
        // Alta de un proveedor
        public async Task AltaProveedor(ProveedorDTO nuevoProveedor)
        {
            ArgumentNullException.ThrowIfNull(nuevoProveedor);

            // Validaciones adicionales si las necesitas
            if (string.IsNullOrWhiteSpace(nuevoProveedor.Nombre))
                throw new ArgumentException("El nombre del proveedor no puede estar vacío");

            if (string.IsNullOrWhiteSpace(nuevoProveedor.Email))
                throw new ArgumentException("El email del proveedor no puede estar vacío");

            if (string.IsNullOrWhiteSpace(nuevoProveedor.Telefono))
                throw new ArgumentException("El teléfono del proveedor no puede estar vacío");

            await ProveedoresRepository.CreateProveedor(nuevoProveedor);
        }

        // Modificar un proveedor existente
        public async Task ModificarProveedor(int proveedorID, ProveedorDTO proveedorModificado)
        {
            ArgumentNullException.ThrowIfNull(proveedorModificado);

            if (proveedorID <= 0)
                throw new ArgumentException("Proveedor_ID debe ser mayor a cero");

            // Validaciones adicionales si las necesitas
            if (string.IsNullOrWhiteSpace(proveedorModificado.Nombre))
                throw new ArgumentException("El nombre del proveedor no puede estar vacío");

            if (string.IsNullOrWhiteSpace(proveedorModificado.Email))
                throw new ArgumentException("El email del proveedor no puede estar vacío");

            if (string.IsNullOrWhiteSpace(proveedorModificado.Telefono))
                throw new ArgumentException("El teléfono del proveedor no puede estar vacío");

            await ProveedoresRepository.UpdateProveedor(proveedorID, proveedorModificado);
        }

        // Baja de un proveedor
        public async Task BajaProveedor(int proveedorID)
        {
            if (proveedorID <= 0)
                throw new ArgumentException("Proveedor_ID debe ser mayor a cero");

            await ProveedoresRepository.DeleteProveedor(proveedorID);
        }

        // Obtener todos los proveedores
        public async Task<List<ProveedorDTO>> ObtenerTodosLosProveedores()
        {
            return await ProveedoresRepository.GetAllProveedores();
        }

        // Obtener un proveedor por ID
        public async Task<ProveedorDTO?> ObtenerProveedorPorId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Proveedor_ID debe ser mayor a cero");

            return await ProveedoresRepository.GetProveedorById(id);
        }
    }
}
