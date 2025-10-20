using System;
using System.ComponentModel.DataAnnotations;

namespace MVCSPortTime1.Models
{
    // DTO que se usa para comunicación con la API y para las vistas
    public class TurnoDTO
    {
        [Key]
        public int Turno_ID { get; set; }
        public int Usuario_ID { get; set; }
        public int Cancha_ID { get; set; }

        // Estado puede venir como string desde la API; usar string para no perder info
        public string Estado { get; set; } = string.Empty;

        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }

        // Campos auxiliares para la vista
        public string UsuarioNombre { get; set; } = string.Empty;
        public string CanchaNombre { get; set; } = string.Empty;

    }
}