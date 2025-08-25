using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ProductoDTO
    {
        public int Producto_ID { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public int Proveedor_ID { get; set; }
        public decimal Precio { get; set; }
    }

}
