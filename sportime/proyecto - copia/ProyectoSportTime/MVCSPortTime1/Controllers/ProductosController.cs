using Microsoft.AspNetCore.Mvc;

namespace MVCSPortTime1.Controllers
{
    public class ProductosController : Controller
    {
        // Muestra la lista de productos
        public IActionResult Index()
        {
            return View();
        }

        // Muestra el formulario para crear un producto
        public IActionResult Crear()
        {
            return View();
        }

        // Muestra el formulario para modificar un producto
        public IActionResult Modificar(int id)
        {
            ViewBag.ProductoId = id;
            return View();
        }

        // Muestra el formulario para eliminar un producto
        public IActionResult Eliminar(int id)
        {
            ViewBag.ProductoId = id;
            return View();
        }
    }
}
