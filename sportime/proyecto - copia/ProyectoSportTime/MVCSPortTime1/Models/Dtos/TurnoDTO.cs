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

        // Nuevo: producto asociado (id) y nombre para mostrar en la vista
        public int? Producto_ID { get; set; }
        public string ProductoNombre { get; set; } = string.Empty;

        // Nuevo: cantidad (por defecto 1)
        public int Cantidad { get; set; } = 1;

        // Estado puede venir como string desde la API; usar string para no perder info
        public string Estado { get; set; } = string.Empty;

        // Fechas internas
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }

        // Wrappers compatibles con las vistas existentes (Inicio / Fin)
        // permiten que las vistas que usan Prop(t,"Inicio") / Prop(t,"Fin") funcionen sin cambios.
        public DateTime Inicio
        {
            get => HoraInicio;
            set => HoraInicio = value;
        }

        public DateTime Fin
        {
            get => HoraFin;
            set => HoraFin = value;
        }

        // Campos auxiliares para la vista
        public string UsuarioNombre { get; set; } = string.Empty;
        public string CanchaNombre { get; set; } = string.Empty;
    }
}