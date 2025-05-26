using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class TurnoDTO
    {
        public int Turno_ID { get; set; }
        public int Admin_ID { get; set; }
        public int Cancha_ID { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
        public int Cliente_ID { get; set; }
        public List<ConsumicionProductoDTO>? ConsumicionProductos { get; set; } // Productos con cantidades
    }
}
