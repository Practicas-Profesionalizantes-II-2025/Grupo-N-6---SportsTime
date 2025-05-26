using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ConsumicionDTO
    {
        public int Consumicion_ID { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public List<ProductoDTO> Productos { get; set; }
    }

}
