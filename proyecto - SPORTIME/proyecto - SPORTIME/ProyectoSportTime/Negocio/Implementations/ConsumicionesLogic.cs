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
    public class ConsumicionesLogic 
    {
        public async Task AltaConsumicion(ConsumicionDTO nuevaConsumicion)
        {
            ArgumentNullException.ThrowIfNull(nuevaConsumicion);

            await ConsumicionesRepository.CreateConsumicion(nuevaConsumicion);  // Llamamos al repositorio para crear la consumición
        }

        public async Task ModificarConsumicion(int consumicionID, ConsumicionDTO consumicionModificada)
        {
            ArgumentNullException.ThrowIfNull(consumicionModificada);

            if (consumicionID <= 0)
            {
                throw new ArgumentException("Id debe ser mayor a cero");
            }

            await ConsumicionesRepository.UpdateConsumicion(consumicionID, consumicionModificada);  // Llamamos al repositorio para modificar la consumición
        }

        public async Task BajaConsumicion(int consumicionID)
        {
            if (consumicionID <= 0)
            {
                throw new ArgumentException("Id debe ser mayor a cero");
            }

            await ConsumicionesRepository.DeleteConsumicion(consumicionID);  // Llamamos al repositorio para eliminar la consumición
        }

        public async Task<List<ConsumicionDTO>> ObtenerTodasLasConsumiciones()
        {
            return await ConsumicionesRepository.GetAllConsumiciones();  // Llamamos al repositorio para obtener todas las consumiciones
        }

        public async Task<ConsumicionDTO?> ObtenerConsumicionPorId(int consumicionID)
        {
            if (consumicionID <= 0)
            {
                throw new ArgumentException("Id debe ser mayor a cero");
            }

            return await ConsumicionesRepository.GetConsumicionById(consumicionID);  // Llamamos al repositorio para obtener una consumición por ID
        }
    }

}
