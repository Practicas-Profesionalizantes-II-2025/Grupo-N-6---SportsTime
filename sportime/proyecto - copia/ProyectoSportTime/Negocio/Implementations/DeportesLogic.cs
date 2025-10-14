using CDatos.Repositorys.IRepositorys;
using CNegocio.Contracts;
using Shared.Dtos;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<DeporteDTO> CrearDeporte(DeporteDTO deporteDTO)
        {
            if (string.IsNullOrWhiteSpace(deporteDTO.Nombre))
                throw new ArgumentException("El nombre del deporte es obligatorio.");

            var entidad = new Deportes { Nombre = deporteDTO.Nombre };
            var creado = await _repo.CrearDeporte(entidad);
            deporteDTO.Deporte_ID = creado.Deporte_ID;
            return deporteDTO;
        }

        public async Task<DeporteDTO> ModificarDeporte(DeporteDTO deporteDTO)
        {
            if (deporteDTO.Deporte_ID <= 0)
                throw new ArgumentException("El Id del deporte no es válido.");
            if (string.IsNullOrWhiteSpace(deporteDTO.Nombre))
                throw new ArgumentException("El nombre del deporte es obligatorio.");

            var existente = await _repo.ObtenerDeportePorId(deporteDTO.Deporte_ID);
            if (existente == null)
                throw new InvalidOperationException("No se encontró el deporte a actualizar.");

            var entidad = new Deportes
            {
                Deporte_ID = deporteDTO.Deporte_ID,
                Nombre = deporteDTO.Nombre
            };

            var actualizado = await _repo.ModificarDeporte(entidad);

            return new DeporteDTO
            {
                Deporte_ID = actualizado.Deporte_ID,
                Nombre = actualizado.Nombre
            };
        }

        public async Task BajaDeporte(int deporteId)
        {
            if (deporteId <= 0)
                throw new ArgumentException("El Id del deporte debe ser mayor a cero.");

            var existente = await _repo.ObtenerDeportePorId(deporteId);
            if (existente == null)
                throw new InvalidOperationException("No se encontró el deporte a eliminar.");

            _repo.EliminarDeporte(deporteId);
        }

        public async Task<List<DeporteDTO>> ObtenerTodosLosDeportes()
        {
            var deportes = await _repo.ObtenerTodosLosDeportes();
            return deportes.Select(d => new DeporteDTO
            {
                Deporte_ID = d.Deporte_ID,
                Nombre = d.Nombre
            }).ToList();
        }

        public async Task<DeporteDTO?> ObtenerDeportePorId(int deporteId)
        {
            if (deporteId <= 0)
                throw new ArgumentException("El Id del deporte debe ser mayor a cero.");

            var deporte = await _repo.ObtenerDeportePorId(deporteId);
            if (deporte == null) return null;

            return new DeporteDTO
            {
                Deporte_ID = deporte.Deporte_ID,
                Nombre = deporte.Nombre
            };
        }
    }
}
