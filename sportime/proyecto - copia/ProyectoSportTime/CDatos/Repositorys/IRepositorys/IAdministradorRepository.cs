using Shared.Dtos;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDatos.Repositorys.IRepositorys
{
    public interface IAdministradorRepository
    {
        Task CrearAdministrador(Administrador administrador);
        Task<List<Administrador>> ObtenerTodosLosAdministradores();
        Task<Administrador?> ObtenerAdministradorPorId(int id);
        Task ModificarAdministrador(Administrador administradorModificado);
        Task EliminiarAdministrador(int administradorID);
      //  Task<AdministradorDTO?> GetAdministradorByEmail(string email);

    }
}
