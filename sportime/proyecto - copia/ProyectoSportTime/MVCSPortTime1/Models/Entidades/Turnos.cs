using System.ComponentModel.DataAnnotations;

namespace MVCSPortTime1.Models.Entidades
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
