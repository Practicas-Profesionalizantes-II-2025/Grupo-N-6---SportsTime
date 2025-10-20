using Microsoft.AspNetCore.Mvc;

namespace MVCSPortTime1.Controllers
{
    public class TurnosController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["BodyClass"] = "turnos-page";
            return View();
        }
    }
}
