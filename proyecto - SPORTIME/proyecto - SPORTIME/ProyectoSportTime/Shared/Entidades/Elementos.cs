using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entidades
{
    public class Elementos
    {
        [Key]
        public int Elemento_ID { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }

        [ForeignKey("Canchas")]
        public int Cancha_ID { get; set; } // Clave Foránea
        public Canchas Canchas { get; set; }
    }
}

