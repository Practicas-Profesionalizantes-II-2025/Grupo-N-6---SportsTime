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
        public string Tipo { get; set; }
        public string Descripcion { get; set; }

        public string DisplayName => $"{Producto_ID} - {Descripcion}";

        [ForeignKey("Proveedores")]
        public int Proveedor_ID { get; set; } // Clave Foránea
        public Proveedores Proveedores { get; set; }

        public ICollection<ConsumicionProducto> ConsumicionProductos { get; set; } 
    }
}
