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

        public async Task<CanchaDTO> CrearCancha(CanchaDTO canchaDTO)
        {
            if (canchaDTO.Deporte_ID <= 0)
                throw new ArgumentException("El Deporte_ID debe ser mayor a cero.");

            var cancha = new Canchas
            {
                Deporte_ID = canchaDTO.Deporte_ID,
                Activa = canchaDTO.Activa
            };

            var creada = await _repo.CrearCancha(cancha);
            canchaDTO.Cancha_ID = creada.Cancha_ID;
            return canchaDTO;
        }

        public async Task<CanchaDTO> ModificarCancha(CanchaDTO canchaDTO)
        {
            if (canchaDTO.Cancha_ID <= 0)
                throw new ArgumentException("El Id de la cancha no es válido.");

            var existente = await _repo.ObtenerCanchaPorId(canchaDTO.Cancha_ID);
            if (existente == null)
                throw new InvalidOperationException("No se encontró la cancha a actualizar.");

            var entidad = new Canchas
            {
                Cancha_ID = canchaDTO.Cancha_ID,
                Deporte_ID = canchaDTO.Deporte_ID,
                Activa = canchaDTO.Activa
            };

            var actualizada = await _repo.ModificarCancha(entidad);

            return new CanchaDTO
            {
                Cancha_ID = actualizada.Cancha_ID,
                Deporte_ID = actualizada.Deporte_ID,
                Activa = actualizada.Activa
            };
        }

        public async Task BajaCancha(int canchaId)
        {
            if (canchaId <= 0)
                throw new ArgumentException("El Id de la cancha debe ser mayor a cero.");

            var existente = await _repo.ObtenerCanchaPorId(canchaId);
            if (existente == null)
                throw new InvalidOperationException("No se encontró la cancha a eliminar.");

            _repo.EliminarCancha(canchaId);
        }

        public async Task<List<CanchaDTO>> ObtenerTodasLasCanchas()
        {
            var canchas = await _repo.ObtenerTodasLasCanchas();
            return canchas.Select(c => new CanchaDTO
            {
                Cancha_ID = c.Cancha_ID,
                Deporte_ID = c.Deporte_ID,
                Activa = c.Activa
            }).ToList();
        }

        public async Task<CanchaDTO?> ObtenerCanchaPorId(int canchaId)
        {
            if (canchaId <= 0)
                throw new ArgumentException("El Id de la cancha debe ser mayor a cero.");

            var cancha = await _repo.ObtenerCanchaPorId(canchaId);
            if (cancha == null) return null;

            return new CanchaDTO
            {
                Cancha_ID = cancha.Cancha_ID,
                Deporte_ID = cancha.Deporte_ID,
                Activa = cancha.Activa
            };
        }

        public async Task<List<CanchaDTO>> ObtenerCanchasPorDeporteId(int deporteId)
        {
            if (deporteId <= 0)
                throw new ArgumentException("El Id del deporte debe ser mayor a cero.");

            var canchas = await _repo.ObtenerCanchasPorDeporteId(deporteId);
            return canchas.Select(c => new CanchaDTO
            {
                Cancha_ID = c.Cancha_ID,
                Deporte_ID = c.Deporte_ID,
                Activa = c.Activa
            }).ToList();
        }
    }
}
