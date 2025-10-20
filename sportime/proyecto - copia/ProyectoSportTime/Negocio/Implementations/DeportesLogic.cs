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

        public async Task<DeporteDTO> Crear(DeporteDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (string.IsNullOrWhiteSpace(dto.Nombre)) throw new ArgumentException("El nombre del deporte es obligatorio.");

            var entidad = new Deportes
            {
                Nombre = dto.Nombre.Trim()
            };

            var created = await _repo.CrearDeporte(entidad);

            return new DeporteDTO
            {
                Deporte_ID = created.Deporte_ID,
                Nombre = created.Nombre
            };
        }

        public async Task Eliminar(int id)
        {
            if (id <= 0) throw new ArgumentException("Id inválido.");
            var existe = await _repo.ObtenerDeportePorId(id);
            if (existe == null) throw new InvalidOperationException("Deporte no encontrado.");
            await _repo.EliminarDeporte(id);
        }

        public async Task<List<DeporteDTO>> ObtenerTodos()
        {
            var list = await _repo.ObtenerTodosLosDeportes();
            return list.Select(d => new DeporteDTO { Deporte_ID = d.Deporte_ID, Nombre = d.Nombre }).ToList();
        }

        public async Task<DeporteDTO?> ObtenerPorId(int id)
        {
            if (id <= 0) return null;
            var d = await _repo.ObtenerDeportePorId(id);
            if (d == null) return null;
            return new DeporteDTO { Deporte_ID = d.Deporte_ID, Nombre = d.Nombre };
        }

        public async Task<DeporteDTO> Modificar(DeporteDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (dto.Deporte_ID <= 0) throw new ArgumentException("Id inválido.");
            if (string.IsNullOrWhiteSpace(dto.Nombre)) throw new ArgumentException("El nombre del deporte es obligatorio.");

            var existente = await _repo.ObtenerDeportePorId(dto.Deporte_ID);
            if (existente == null) throw new InvalidOperationException("Deporte no encontrado.");

            existente.Nombre = dto.Nombre.Trim();
            var updated = await _repo.ModificarDeporte(existente);

            return new DeporteDTO { Deporte_ID = updated.Deporte_ID, Nombre = updated.Nombre };
        }
    }
}