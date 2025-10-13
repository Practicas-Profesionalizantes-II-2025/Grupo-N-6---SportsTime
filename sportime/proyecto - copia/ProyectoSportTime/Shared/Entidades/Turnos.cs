using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entidades
{
    public class Turnos
    {
        [Key]
        public int Turno_ID { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
        public string Estado { get; set; } // "Pendiente", "Confirmado", "Cancelado"
        
        //Foreing  Keys
        public int Usuario_ID { get; set; }
        //public Usuarios Usuario { get; set; } // Propiedad de navegación

        public int Cancha_ID { get; set; }
        //public Canchas Cancha { get; set; } // Propiedad de navegación

    }
}
