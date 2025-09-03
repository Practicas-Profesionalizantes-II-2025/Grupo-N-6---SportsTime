using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using System.Collections.Generic;

namespace MVCSPortTime1.Controllers
{
    public class CanchasController : Controller
    {
        // Lista de ejemplo en memoria
        private static readonly List<CanchaDTO> canchasEjemplo = new List<CanchaDTO>
        {
            new CanchaDTO { Cancha_ID = 1, Deporte_ID = 1, Tipo = "Fútbol 5" },
            new CanchaDTO { Cancha_ID = 2, Deporte_ID = 2, Tipo = "Tenis" },
            new CanchaDTO { Cancha_ID = 3, Deporte_ID = 1, Tipo = "Fútbol 7" }
        };

        public IActionResult Index(string? search)
        {
            var canchas = canchasEjemplo;
            if (!string.IsNullOrWhiteSpace(search))
                canchas = canchas.FindAll(c => (c.Tipo ?? "").ToLower().Contains(search.ToLower()) || c.Cancha_ID.ToString().Contains(search));

            // No hay deportes de ejemplo, pero la vista espera algo en ViewBag.Deportes
            ViewBag.Deportes = new List<object>();
            ViewBag.Search = search;
            return View(canchas);
        }
    }
}