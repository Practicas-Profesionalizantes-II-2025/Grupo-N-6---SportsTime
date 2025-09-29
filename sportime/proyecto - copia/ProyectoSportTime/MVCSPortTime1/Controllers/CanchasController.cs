using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using System.Collections.Generic;

namespace MVCSPortTime1.Controllers
{
    public class CanchasController : Controller
    {
       
        /*
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
        */
    }
}