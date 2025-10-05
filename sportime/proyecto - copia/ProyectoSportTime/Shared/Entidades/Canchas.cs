using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entidades
{
    public class Canchas
    {
        [Key]
        public int Cancha_ID { get; set; }               
        public int Deporte_ID { get; set; }              
    
    }

}
