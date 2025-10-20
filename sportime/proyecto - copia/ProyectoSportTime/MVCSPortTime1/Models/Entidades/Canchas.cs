using System.ComponentModel.DataAnnotations;

namespace MVCSPortTime1.Models.Entidades
{
    public class Canchas
    {
        [Key]
        public int Cancha_ID { get; set; }
        public int Deporte_ID { get; set; }
        public bool Activa { get; set; }
    }
}
