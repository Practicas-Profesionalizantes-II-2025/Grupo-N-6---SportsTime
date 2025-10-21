using System.ComponentModel.DataAnnotations;

namespace Shared.Entidades
{
    public class Clientes
    {
        [Key]
        public int Cliente_ID { get; set; }
        [Required]
        public string Nombre { get; set; } = string.Empty;
        public string NumeroTelefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
