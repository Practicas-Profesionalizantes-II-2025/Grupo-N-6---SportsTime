using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos;

namespace CDatos.Repositorys.IRepositorys
{
    public interface ICanchasRepository
    {
        Task CreateCancha(CanchaDTO cancha);
        Task UpdateCancha(int canchaID, CanchaDTO canchaModificada);
        Task DeleteCancha(int canchaID);
        Task<List<CanchaDTO>> GetAllCanchas();
        Task<CanchaDTO?> GetCanchaById(int id);
       // Task<List<CanchaDTO>> GetCanchasByDeporteId(int deporteId);
    }
}
