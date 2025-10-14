using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos
{
    public class DeporteDTO
    {
        public int Deporte_ID { get; set; }

        [Required(ErrorMessage = "El nombre del deporte es obligatorio")]
        [MaxLength(50)]
        public string Nombre { get; set; }
    }
}
