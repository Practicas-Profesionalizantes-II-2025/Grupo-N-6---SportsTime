using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entidades
{
    public class Clientes
    {
        [Key]
        public int Cliente_ID { get; set; }
        public string Nombre { get; set; }
        public int NumeroTelefono { get; set; }

        public string DisplayName => $"{Cliente_ID} - {Nombre}";
    }
}
