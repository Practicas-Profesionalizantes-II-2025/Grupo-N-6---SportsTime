using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ConsumicionProductoDTO
    {
        public int Consumicion_ID { get; set; }
        public int Producto_ID { get; set; }
        public int Cantidad { get; set; }  
    }
}
