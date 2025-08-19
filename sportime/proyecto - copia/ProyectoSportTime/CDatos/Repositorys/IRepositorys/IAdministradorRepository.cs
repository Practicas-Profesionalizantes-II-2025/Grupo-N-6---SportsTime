using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos;

namespace CDatos.Repositorys.IRepositorys
{
    public interface IAdministradorRepository
    {
        Task CreateAdministrador(AdministradorDTO adminDTO);
        Task<List<AdministradorDTO>> GetAllAdministradores();
        Task<AdministradorDTO?> GetAdministradorById(int id);
        Task UpdateAdministrador(int adminID, AdministradorDTO updatedAdmin);
        Task DeleteAdministrador(int adminID);
        Task<AdministradorDTO?> GetAdministradorByEmail(string email);

    }
}
