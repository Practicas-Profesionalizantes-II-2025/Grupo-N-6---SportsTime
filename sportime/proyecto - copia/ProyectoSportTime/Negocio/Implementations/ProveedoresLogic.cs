using Microsoft.EntityFrameworkCore;
using CNegocio.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDatos.Repositorys.IRepositorys;
using Shared.Dtos;

namespace CNegocio.Implementations
{
    public class ProveedoresLogic : IProveedores
    {
        private readonly IProveedoresRepository _repo;

        public ProveedoresLogic(IProveedoresRepository repo)
        {
            _repo = repo;
        }
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

            await _repo.CrearProveedor(nuevoProveedor);
        }

        // Modificar un proveedor existente
        public async Task ModificarProveedor(int proveedorID, ProveedorDTO proveedorModificado)
        {
            ArgumentNullException.ThrowIfNull(proveedorModificado);

            if (proveedorID <= 0)
                throw new ArgumentException("Proveedor_ID debe ser mayor a cero");
         
            if (string.IsNullOrWhiteSpace(proveedorModificado.Nombre))
                throw new ArgumentException("El nombre del proveedor no puede estar vacío");

            if (string.IsNullOrWhiteSpace(proveedorModificado.Email))
                throw new ArgumentException("El email del proveedor no puede estar vacío");

            if (string.IsNullOrWhiteSpace(proveedorModificado.Telefono))
                throw new ArgumentException("El teléfono del proveedor no puede estar vacío");

            await _repo.ModificarProveedor(proveedorID, proveedorModificado);
        }

        // Baja de un proveedor
        public async Task BajaProveedor(int proveedorID)
        {
            if (proveedorID <= 0)
                throw new ArgumentException("Proveedor_ID debe ser mayor a cero");

            await _repo.EliminarProveedor(proveedorID);
        }

        // Obtener todos los proveedores
        public async Task<List<ProveedorDTO>> ObtenerTodosLosProveedores()
        {
            return await _repo.ObtenerTodosLosProveedores();
        }

        // Obtener un proveedor por ID
        public async Task<ProveedorDTO?> ObtenerProveedorPorId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Proveedor_ID debe ser mayor a cero");

            return await _repo.ObtenerProveedorPorId(id);
        }
    }
}
