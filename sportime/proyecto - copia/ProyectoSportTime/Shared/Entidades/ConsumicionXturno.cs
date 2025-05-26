using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entidades
{
    public class ConsumicionXturno
    {
        [Key]
        public int ConsumicionXturno_ID { get; set; }

        [ForeignKey("Turnos")]
        public int Turno_ID { get; set; }
        public Turnos Turnos { get; set; }

        [ForeignKey("Productos")]
        public int Producto_ID { get; set; }
        public Productos Productos { get; set; }

        public int Cantidad { get; set; }
    }

}
