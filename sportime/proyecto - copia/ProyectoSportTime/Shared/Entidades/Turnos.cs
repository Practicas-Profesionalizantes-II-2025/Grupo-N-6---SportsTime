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

        [ForeignKey("Cliente")]
        public int Cliente_ID { get; set; }
        public Clientes Cliente { get; set; }

        [ForeignKey("Administrador")]
        public int Admin_ID { get; set; }
        public Administrador Administrador { get; set; }

        [ForeignKey("Canchas")]
        public int Cancha_ID { get; set; }
        public Canchas Canchas { get; set; }

        // Cambiar la relación de Consumicion_ID por una colección de ConsumicionProducto
        public List<ConsumicionProducto>? ConsumicionProductos { get; set; }
    }
}
