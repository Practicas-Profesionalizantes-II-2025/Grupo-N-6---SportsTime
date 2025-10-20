using Microsoft.AspNetCore.Mvc;

namespace MVCSPortTime1.Controllers
{
    public class ClientesController : Controller
    {
        public IActionResult Index()
        {
            ViewData["BodyId"] = "clientes-page";
            return View();
        }
    }
}
