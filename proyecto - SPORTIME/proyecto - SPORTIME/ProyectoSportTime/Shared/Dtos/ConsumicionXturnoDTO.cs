using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ConsumicionXturnoDTO
    {
        public int ConsumicionXturno_ID { get; set; }
        public int Turno_ID { get; set; }
        public int Producto_ID { get; set; } 
        public int Cantidad { get; set; }
    }


}
