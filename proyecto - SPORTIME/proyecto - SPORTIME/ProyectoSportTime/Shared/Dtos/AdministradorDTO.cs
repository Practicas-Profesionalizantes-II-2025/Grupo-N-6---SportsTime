using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class AdministradorDTO
    {
        public int Admin_ID { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }  // Para almacenar la contraseña de forma segura
        public DateTime LastLogin { get; set; }    // Para registrar el último inicio de sesión
        public bool IsSuperAdmin { get; set; } = true; // Ya que solo hay un administrador, por defecto es superadmin
    }

}
