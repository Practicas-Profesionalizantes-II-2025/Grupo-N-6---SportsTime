using CDatos.Repositorys;
using CDatos.Repositorys.IRepositorys;
using CNegocio.Contracts;
using Microsoft.EntityFrameworkCore;
using Negocio.Contracts;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNegocio.Implementations
{
    public class ConsumicionesLogic : IConsumiciones
    {
        private readonly IConsumicionesRepository _repo;

        public ConsumicionesLogic(IConsumicionesRepository repo)
        {
            _repo = repo;
        }
        public async Task AltaConsumicion(ConsumicionDTO nuevaConsumicion)
        {
            ArgumentNullException.ThrowIfNull(nuevaConsumicion);
            await _repo.CrearConsumicion(nuevaConsumicion);      // Llamamos al repositorio para crear la consumición
        }

        public async Task ModificarConsumicion(int consumicionID, ConsumicionDTO consumicionModificada)
        {
            ArgumentNullException.ThrowIfNull(consumicionModificada);
            if (consumicionID <= 0)
                throw new ArgumentException("Id debe ser mayor a cero");

            await _repo.ModificarConsumicion(consumicionID, consumicionModificada);    // Llamamos al repositorio para modificar la consumición
        }

        public async Task BajaConsumicion(int consumicionID)
        {
            if (consumicionID <= 0)
                throw new ArgumentException("Id debe ser mayor a cero");

            await _repo.EliminarConsumicion(consumicionID);     // Llamamos al repositorio para eliminar la consumición
        }

        public async Task<List<ConsumicionDTO>> ObtenerTodasLasConsumiciones()
        {
            return await _repo.ObtenerTodasLasConsumiciones();  // Llamamos al repositorio para obtener todas las consumiciones
        }

        public async Task<ConsumicionDTO?> ObtenerConsumicionPorId(int consumicionID)
        {
            if (consumicionID <= 0)
            {
                throw new ArgumentException("Id debe ser mayor a cero");
            }

            return await _repo.ObtenerConsumicionPorId(consumicionID);  // Llamamos al repositorio para obtener una consumición por ID
        }
    }

}
