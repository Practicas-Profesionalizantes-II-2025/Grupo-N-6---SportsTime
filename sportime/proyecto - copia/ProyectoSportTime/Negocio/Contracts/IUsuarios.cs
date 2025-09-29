using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Contracts
{
    public interface IUsuarios
    {
        Task CrearAdministrador(UsuarioDTO adminDTO);
        Task<List<UsuarioDTO>> ObtenerTodos();
        Task<UsuarioDTO?> ObtenerPorId(int id);
        Task ActualizarAdministrador(int id, UsuarioDTO adminDTO);
        Task EliminarAdministrador(int id);
        Task<UsuarioDTO?> Login(LoginDTO loginDto);

    }
    /* void AltaAdministrador(string nombre, string email);
     void ModificarAdministrador(int adminID, string nuevoNombre, string nuevoEmail);
     void BajaAdministrador(int adminID);
    */
}

