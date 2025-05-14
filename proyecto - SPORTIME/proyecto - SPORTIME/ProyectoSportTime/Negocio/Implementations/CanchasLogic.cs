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
    public class CanchasLogic
    {
        public async Task AltaCancha(CanchaDTO nuevaCancha)
        {
            ArgumentNullException.ThrowIfNull(nuevaCancha);

            // Validaciones adicionales si las necesitas
            if (nuevaCancha.Deporte_ID <= 0)
                throw new ArgumentException("Deporte_ID debe ser mayor a cero");

            await CanchasRepository.CreateCancha(nuevaCancha);  
        }

        public async Task ModificarCancha(int canchaID, CanchaDTO canchaModificada)
        {
            ArgumentNullException.ThrowIfNull(canchaModificada);

            if (canchaID <= 0)
                throw new ArgumentException("Cancha_ID debe ser mayor a cero");

            await CanchasRepository.UpdateCancha(canchaID, canchaModificada);  
        }

        public async Task BajaCancha(int canchaID)
        {
            if (canchaID <= 0)
                throw new ArgumentException("Cancha_ID debe ser mayor a cero");

            await CanchasRepository.DeleteCancha(canchaID);  
        }

        public async Task<List<CanchaDTO>> ObtenerTodasLasCanchas()
        {
            return await CanchasRepository.GetAllCanchas();  
        }

        public async Task<CanchaDTO?> ObtenerCanchaPorId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Cancha_ID debe ser mayor a cero");

            return await CanchasRepository.GetCanchaById(id);  
        }
    }

}
