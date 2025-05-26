using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entidades
{
    public class ConsumicionProducto
    {
        [Key]
        public int ConsumicionProducto_ID { get; set; }

        [ForeignKey("Consumicion")]
        public int Consumicion_ID { get; set; }
        public Consumiciones Consumicion { get; set; }

        [ForeignKey("Producto")]
        public int Producto_ID { get; set; }
        public Productos Producto { get; set; }

        public int Cantidad { get; set; } // Representa la cantidad del producto
    }
}
