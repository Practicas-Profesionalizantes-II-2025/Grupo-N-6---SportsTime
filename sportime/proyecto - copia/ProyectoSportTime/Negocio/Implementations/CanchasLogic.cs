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
    public class CanchasLogic : ICanchas
    {
        private readonly ICanchasRepository _repo;

        public CanchasLogic(ICanchasRepository repo)
        {
            _repo = repo;
        }

        public async Task<CanchaDTO> CrearCancha(CanchaDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (dto.Deporte_ID <= 0) throw new ArgumentException("El Deporte_ID es obligatorio y debe ser mayor a cero.");

            // Activa por defecto si no se especificó (ya viene en DTO como true por defecto)
            var entidad = new Canchas
            {
                Deporte_ID = dto.Deporte_ID,
                Activa = dto.Activa
            };

            // (Regla adicional) No permitir duplicados exactos: si una cancha con mismo Deporte_ID y Activa existe,
            // esto es una decisión de negocio: aquí no lo bloqueamos por defecto, pero dejamos disponible el check.
            // Si querés impedir crear múltiples canchas para el mismo deporte, descomenta lo siguiente:
            /*
            if (await _repo.ExisteCanchaConMismoDeporte(dto.Deporte_ID))
                throw new InvalidOperationException("Ya existe una cancha asociada a ese deporte.");
            */

            var created = await _repo.CrearCancha(entidad);
            return new CanchaDTO
            {
                Cancha_ID = created.Cancha_ID,
                Deporte_ID = created.Deporte_ID,
                Activa = created.Activa
            };
        }

        public async Task<CanchaDTO> ModificarCancha(CanchaDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (dto.Cancha_ID <= 0) throw new ArgumentException("El ID de la cancha es inválido.");
            if (dto.Deporte_ID <= 0) throw new ArgumentException("El Deporte_ID es obligatorio y debe ser mayor a cero.");

            var existente = await _repo.ObtenerCanchaPorId(dto.Cancha_ID);
            if (existente == null) throw new InvalidOperationException("No se encontró la cancha a modificar.");

            // Validación: una cancha solo tiene un Deporte_ID (ya modelado). No permitimos asignar un deporte inválido.
            existente.Deporte_ID = dto.Deporte_ID;
            existente.Activa = dto.Activa;

            var updated = await _repo.ModificarCancha(existente);
            return new CanchaDTO
            {
                Cancha_ID = updated.Cancha_ID,
                Deporte_ID = updated.Deporte_ID,
                Activa = updated.Activa
            };
        }

        public async Task EliminarCancha(int id)
        {
            if (id <= 0) throw new ArgumentException("El ID de la cancha es inválido.");
            var existente = await _repo.ObtenerCanchaPorId(id);
            if (existente == null) throw new InvalidOperationException("No se encontró la cancha a eliminar.");
            await _repo.EliminarCancha(id);
        }

        public async Task<List<CanchaDTO>> ObtenerTodas()
        {
            var list = await _repo.ObtenerTodasLasCanchas();
            return list.Select(c => new CanchaDTO
            {
                Cancha_ID = c.Cancha_ID,
                Deporte_ID = c.Deporte_ID,
                Activa = c.Activa
            }).ToList();
        }

        public async Task<CanchaDTO?> ObtenerPorId(int id)
        {
            var c = await _repo.ObtenerCanchaPorId(id);
            if (c == null) return null;
            return new CanchaDTO
            {
                Cancha_ID = c.Cancha_ID,
                Deporte_ID = c.Deporte_ID,
                Activa = c.Activa
            };
        }

        public async Task<List<CanchaDTO>> ObtenerPorDeporte(int deporteId)
        {
            var list = await _repo.ObtenerCanchasPorDeporte(deporteId);
            return list.Select(c => new CanchaDTO
            {
                Cancha_ID = c.Cancha_ID,
                Deporte_ID = c.Deporte_ID,
                Activa = c.Activa
            }).ToList();
        }

        public async Task<List<CanchaDTO>> ObtenerActivas()
        {
            var list = await _repo.ObtenerCanchasActivas();
            return list.Select(c => new CanchaDTO
            {
                Cancha_ID = c.Cancha_ID,
                Deporte_ID = c.Deporte_ID,
                Activa = c.Activa
            }).ToList();
        }

        public async Task<bool> CanchaExiste(int id)
        {
            return await _repo.ExisteCanchaPorId(id);
        }
    }
}