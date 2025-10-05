using System.ComponentModel.DataAnnotations;

namespace MVCSPortTime1.Models.Entidades
{
    public class Proveedores
    {
        [Key]
        public int Proveedor_ID { get; set; } // Clave primaria
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
    }
}
