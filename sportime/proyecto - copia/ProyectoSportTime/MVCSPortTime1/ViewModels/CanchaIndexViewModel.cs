using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Dtos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCSPortTime1.ViewModels
{
    public class CanchaViewModel
    {
        public int Cancha_ID { get; set; }
        public int Deporte_ID { get; set; }
        public string DeporteNombre { get; set; }
        public bool Activa { get; set; }
    }

    public class CanchaIndexViewModel
    {
        [Required]
        public CanchaDTO NuevaCancha { get; set; } = new();

        public List<CanchaViewModel> Canchas { get; set; } = new();

        public List<SelectListItem> Deportes { get; set; } = new();
    }
}