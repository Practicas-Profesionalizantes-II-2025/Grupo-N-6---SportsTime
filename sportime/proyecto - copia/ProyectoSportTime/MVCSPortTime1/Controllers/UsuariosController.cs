using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using CNegocio.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSPortTime1.Controllers
{
    public class UsuariosController : Controller
    {
        /*
        // Lista en memoria para pruebas (simula la base de datos)
        private static List<ClienteDTO> clientes = new List<ClienteDTO>
        {
            new ClienteDTO { Cliente_ID = 1, Nombre = "Juan Pérez", NumeroTelefono = 123456789 },
            new ClienteDTO { Cliente_ID = 2, Nombre = "Ana López", NumeroTelefono = 987654321 }
        };
        */
        private readonly IClientes _clientesLogic;

        public UsuariosController(IClientes clientesLogic)
        {
            _clientesLogic = clientesLogic;
        }

        // Listado
        public async Task<IActionResult> Index()
        {
            var clientes = await _clientesLogic.ObtenerTodosLosClientes();

            return View(clientes); // Muestra la vista Index.cshtml
        }

        // Alta (GET)
        public IActionResult Crear()
        {
            return View(); // Muestra el formulario Crear.cshtml
        }

        // Alta (POST)
        [HttpPost]
        public async Task<IActionResult> Crear(ClienteDTO cliente)
        {
            if (ModelState.IsValid)
            {
                await _clientesLogic.AltaCliente(cliente);
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // Modificación (GET)
        public async Task<IActionResult> Editar(int id)
        {
            var cliente = await _clientesLogic.ObtenerClientePorId(id);
            if (cliente == null) return NotFound();
            return View(cliente); // Muestra Editar.cshtml
        }

        // Modificación (POST)
        [HttpPost]
        public async Task<IActionResult> Editar(ClienteDTO cliente)
        {
            if (ModelState.IsValid)
            {
                await _clientesLogic.ModificarCliente(cliente.Cliente_ID, cliente);
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // Detalles (GET)
        public async Task<IActionResult> Detalles(int id)
        {
            var cliente = await _clientesLogic.ObtenerClientePorId(id);
            if (cliente == null) return NotFound();
            return View(cliente); // Muestra Detalles.cshtml
        }

        // Baja (GET para confirmar)
        public async Task<IActionResult> Eliminar(int id)
        {
            var cliente = await _clientesLogic.ObtenerClientePorId(id);
            if (cliente == null) return NotFound();
            return View(cliente); // Muestra Eliminar.cshtml
        }

        // Baja (POST para confirmar)
        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> EliminarConfirmed(int id)
        {
            await _clientesLogic.BajaCliente(id);
            return RedirectToAction("Index");
        }
        
       
        


    }
}