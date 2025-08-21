using Microsoft.EntityFrameworkCore;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDatos.Repositorys.IRepositorys;
using Shared.Dtos;
using CNegocio.Contracts;


namespace Negocio.Implementations
{
    public class CanchasLogic : ICanchas
    {
        private readonly ICanchasRepository _repo;

        public CanchasLogic(ICanchasRepository repo)
        {
            _repo = repo;
        }
        public async Task AltaCancha(CanchaDTO nuevaCancha)
        {
            ArgumentNullException.ThrowIfNull(nuevaCancha);

            // Validaciones adicionales 
            if (nuevaCancha.Deporte_ID <= 0)
                throw new ArgumentException("Deporte_ID debe ser mayor a cero");

            await _repo.CreateCancha(nuevaCancha);
        }

        public async Task ModificarCancha(int canchaID, CanchaDTO canchaModificada)
        {
            ArgumentNullException.ThrowIfNull(canchaModificada);

            if (canchaID <= 0)
                throw new ArgumentException("Cancha_ID debe ser mayor a cero");

            await _repo.UpdateCancha(canchaID, canchaModificada);  
        }

        public async Task BajaCancha(int canchaID)
        {
            if (canchaID <= 0)
                throw new ArgumentException("Cancha_ID debe ser mayor a cero");

            await _repo.DeleteCancha(canchaID);  
        }

        public async Task<List<CanchaDTO>> ObtenerTodasLasCanchas()
        {
            return await _repo.GetAllCanchas();  
        }

        public async Task<CanchaDTO?> ObtenerCanchaPorId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Cancha_ID debe ser mayor a cero");

            return await _repo.GetCanchaById(id);  
        }
    }

}
