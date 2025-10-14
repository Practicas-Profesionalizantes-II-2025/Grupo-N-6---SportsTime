using Shared.Dtos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCSPortTime1.ViewModels
{
    public class DeporteIndexViewModel
    {
        [Required]
        public DeporteDTO NuevoDeporte { get; set; } = new();

        public List<DeporteDTO> Deportes { get; set; } = new();
    }
}