using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class CanchaDTO
    {
        public int Cancha_ID { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El Deporte_ID debe ser mayor a cero")]
        public int Deporte_ID { get; set; }

        public bool Activa { get; set; } = true;
    }
}