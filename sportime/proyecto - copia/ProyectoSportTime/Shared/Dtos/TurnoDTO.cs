using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class TurnoDTO
    {
        [Key]
        public int Turno_ID { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
        public string Estado { get; set; } // "Pendiente", "Confirmado", "Cancelado"
        public int Usuario_ID { get; set; }       
        public int Cancha_ID { get; set; }
        public int? Cliente_ID { get; set; }
    }
}
