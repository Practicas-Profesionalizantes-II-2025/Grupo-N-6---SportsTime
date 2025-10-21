using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entidades
{
    public class Usuarios
    {
        [Key]
        public int Usuario_ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; }
        public string NumeroTelefono { get; set; }
        // Almacena hash seguro de la contraseña
        public string PasswordHash { get; set; }
        public string Rol { get; set; } 
    }
}
