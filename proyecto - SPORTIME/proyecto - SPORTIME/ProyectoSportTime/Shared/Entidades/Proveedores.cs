using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entidades
{
    public class Proveedores
    {
        [Key]
        public int Proveedor_ID { get; set; } // Clave primaria
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }

        public ICollection<Productos> Productos { get; set; } = new List<Productos>();
    }
}
