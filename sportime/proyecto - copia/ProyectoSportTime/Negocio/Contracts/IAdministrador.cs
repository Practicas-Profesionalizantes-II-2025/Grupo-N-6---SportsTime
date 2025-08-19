using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Contracts
{
    public interface IAdministrador
    {
        Task CrearAdministrador(AdministradorDTO adminDTO);
        Task<List<AdministradorDTO>> ObtenerTodos();
        Task<AdministradorDTO?> ObtenerPorId(int id);
        Task ActualizarAdministrador(int id, AdministradorDTO adminDTO);
        Task EliminarAdministrador(int id);
        Task<AdministradorDTO?> Login(LoginDTO loginDto);

    }
    /* void AltaAdministrador(string nombre, string email);
     void ModificarAdministrador(int adminID, string nuevoNombre, string nuevoEmail);
     void BajaAdministrador(int adminID);
    */
}

