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
    public class TurnoProducto
    {
        [Key]
        public int TurnoProducto_ID { get; set; }
        public int Turno_ID { get; set; }         
        public int Producto_ID { get; set; }
        public int Cantidad { get; set; } // Representa la cantidad del producto
    }
}
