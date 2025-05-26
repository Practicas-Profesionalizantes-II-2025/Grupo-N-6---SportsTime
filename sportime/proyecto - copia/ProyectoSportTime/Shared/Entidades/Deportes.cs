using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entidades
{
    public class Deportes
    {
        [Key]
        public int Deporte_ID { get; set; }
        public string Tipo { get; set; }
        
        public ICollection<Canchas>? Canchas { get; set; }
    }
}
