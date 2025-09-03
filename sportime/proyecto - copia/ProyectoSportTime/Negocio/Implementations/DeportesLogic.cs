using CDatos.Repositorys.IRepositorys;
using CNegocio.Contracts;
using Microsoft.EntityFrameworkCore;
using Negocio.Repositorys;
using Shared.Dtos;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNegocio.Implementations
{
    public class DeportesLogic : IDeportes
    {
        private readonly IDeportesRepository _repo;

        public DeportesLogic(IDeportesRepository repo)
        {
            _repo = repo;
        }

        public async Task AltaDeporte(DeporteDTO nuevoDeporte)
        {
            ArgumentNullException.ThrowIfNull(nuevoDeporte);

            if (string.IsNullOrWhiteSpace(nuevoDeporte.Tipo))
                throw new ArgumentException("El tipo de deporte no puede estar vacío.");

            await _repo.CrearDeporte(nuevoDeporte);
        }
        /* public async Task AltaDeporte(DeporteDTO nuevoDeporte)
        {
            ArgumentNullException.ThrowIfNull(nuevoDeporte);

            await DeportesRepository.CreateDeporte(nuevoDeporte);  // Llamamos al repositorio para crear el deporte
        }
        */

        public async Task ModificarDeporte(int deporteID, DeporteDTO deporteModificado)
        {
            ArgumentNullException.ThrowIfNull(deporteModificado);

            if (deporteID <= 0)
            {
                throw new ArgumentException("Id debe ser mayor a cero");
            }

            await _repo.ModificarDeporte(deporteID, deporteModificado);  // Llamamos al repositorio para modificar el deporte
        }

        public async Task BajaDeporte(int deporteID)
        {
            if (deporteID <= 0)
            {
                throw new ArgumentException("Id debe ser mayor a cero");
            }

            await _repo.EliminarDeporte(deporteID);  // Llamamos al repositorio para eliminar el deporte
        }

        public async Task<List<DeporteDTO>> ObtenerTodosLosDeportes()
        {
            return await _repo.ObtenerTodosLosDeportes();  // Llamamos al repositorio para obtener todos los deportes
        }

        public async Task<DeporteDTO?> ObtenerDeportePorId(int deporteID)
        {
            if (deporteID <= 0)
            {
                throw new ArgumentException("Id debe ser mayor a cero");
            }

            return await _repo.ObtenerDeportePorId(deporteID);  // Llamamos al repositorio para obtener un deporte por ID
        }
    }

}
