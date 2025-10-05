using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entidades
{
    public class Productos
    {
        [Key]
        public int Producto_ID { get; set; }
        public string TipoProducto { get; set; }
        public int Proveedor_ID { get; set; } // Clave Foránea
        public decimal Precio { get; set; }

    }
}
