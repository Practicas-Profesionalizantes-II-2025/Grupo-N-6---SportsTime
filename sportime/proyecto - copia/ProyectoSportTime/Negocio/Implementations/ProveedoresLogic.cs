using CDatos.Repositorys.IRepositorys;
using CNegocio.Contracts;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

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
        public async Task<ProveedorDTO> CrearProveedor(ProveedorDTO proveedorDTO)
        {
            List<string> camposErroneos = new List<string>();
            if (string.IsNullOrEmpty(proveedorDTO.Nombre) || !IsValidName(proveedorDTO.Nombre))
                camposErroneos.Add("Nombre");

            if (camposErroneos.Count > 0)
            {
                throw new ArgumentException("Los siguientes campos son inválidos: ", string.Join(", ", camposErroneos));
            }

            var proveedor = new Proveedores
            {
                Nombre = proveedorDTO.Nombre,
                Direccion = proveedorDTO.Direccion,
                Telefono = proveedorDTO.Telefono,
                Email = proveedorDTO.Email
            };

            var nuevoProveedor = await _repo.CrearProveedor(proveedor);

            proveedorDTO.Proveedor_ID = nuevoProveedor.Proveedor_ID;

            return proveedorDTO;
        }

        // Modificar un proveedor existente
        public async Task ModificarProveedor(ProveedorDTO proveedorDTO)
        {
            if (proveedorDTO.Proveedor_ID <= 0)
                throw new ArgumentException("El Id del proveedor no es válido.");

            var existente = await _repo.ObtenerProveedorPorId(proveedorDTO.Proveedor_ID);
            if (existente == null)
                throw new InvalidOperationException("No se encontró el proveedor a actualizar.");

            ValidarProveedorDTO(proveedorDTO, esNuevo: false);

            var proveedor = new Proveedores
            {
                Proveedor_ID = proveedorDTO.Proveedor_ID,
                Nombre = proveedorDTO.Nombre,
                Direccion = proveedorDTO.Direccion,
                Telefono = proveedorDTO.Telefono,
                Email = proveedorDTO.Email
            };
            _repo.ModificarProveedor(proveedor);
        }

        // Baja de un proveedor
        public async Task BajaProveedor(int ProveedorID)
        {
            if (ProveedorID <= 0)
                throw new ArgumentException("El Id del proveedor debe ser mayor a cero");
            var existente = await _repo.ObtenerProveedorPorId(ProveedorID);
            if (existente == null)
                throw new InvalidOperationException("No se encontró el proveedor a eliminar.");

             _repo.EliminarProveedor(ProveedorID);
        }

        // Obtener todos los proveedores
        public async Task<List<ProveedorDTO>> ObtenerTodosLosProveedores()
        {
            var proveedores = await _repo.ObtenerTodosLosProveedores();
            return proveedores.Select(p => new ProveedorDTO
            {
                Proveedor_ID = p.Proveedor_ID,
                Nombre = p.Nombre,
                Direccion = p.Direccion,
                Telefono = p.Telefono,
                Email = p.Email
            }).ToList();
        }

        // Obtener un proveedor por ID
        public async Task<ProveedorDTO?> ObtenerProveedorPorId(int ProveedorID)
        {
            if (ProveedorID <= 0)
                throw new ArgumentException("El ID del proveedor debe ser mayor que cero.");

            var proveedor = await _repo.ObtenerProveedorPorId(ProveedorID);
            if (proveedor == null)
                throw new ArgumentException($"No se encontró un proveedor con el ID {ProveedorID}");

            return new ProveedorDTO
            {
                Proveedor_ID = proveedor.Proveedor_ID,
                Nombre = proveedor.Nombre,
                Direccion = proveedor.Direccion,
                Telefono = proveedor.Telefono,
                Email = proveedor.Email
            };
        }

        private void ValidarProveedorDTO(ProveedorDTO proveedorDTO, bool esNuevo)
        {

            if (proveedorDTO == null)
                throw new ArgumentNullException(nameof(proveedorDTO), "El proveedor no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(proveedorDTO.Nombre))
                throw new ArgumentException("El nombre del proveedor es obligatorio.");

            if (string.IsNullOrWhiteSpace(proveedorDTO.Direccion))
                throw new ArgumentException("La dirección del proveedor es obligatoria.");

            if (string.IsNullOrWhiteSpace(proveedorDTO.Telefono))
                throw new ArgumentException("El teléfono del proveedor es obligatorio.");

            if (!Regex.IsMatch(proveedorDTO.Telefono, @"^\+?\d{7,15}$"))
                throw new ArgumentException("El teléfono no tiene un formato válido.");

            if (string.IsNullOrWhiteSpace(proveedorDTO.Email))
                throw new ArgumentException("El email del proveedor es obligatorio.");

            if (!Regex.IsMatch(proveedorDTO.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("El email del proveedor no tiene un formato válido.");
        }

        private bool ContainsInvalidCharacter(string text)
        {
            char[] caracteres = { '!', '"', '#', '$', '%', '/', '(', ')', '=', '.', ',' };
            return caracteres.Any(c => text.Contains(c));
        }
        private bool IsValidName(string nombre)
        {
            return nombre.Length < 15 && !ContainsInvalidCharacter(nombre);
        }

    }
}

