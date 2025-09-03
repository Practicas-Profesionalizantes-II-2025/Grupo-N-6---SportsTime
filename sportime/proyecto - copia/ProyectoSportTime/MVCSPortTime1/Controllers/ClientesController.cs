using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace MVCSPortTime1.Controllers
{
    public class ClientesController : Controller
    {
        // Lista en memoria para pruebas (simula la base de datos)
        private static List<ClienteDTO> clientes = new List<ClienteDTO>
        {
            new ClienteDTO { Cliente_ID = 1, Nombre = "Juan Pérez", NumeroTelefono = 123456789 },
            new ClienteDTO { Cliente_ID = 2, Nombre = "Ana López", NumeroTelefono = 987654321 }
        };

        public IActionResult Index()
        {
            return View(clientes);
        }

        [HttpPost]
        public IActionResult Guardar(string nombre, int telefono)
        {
            if (!string.IsNullOrWhiteSpace(nombre) && telefono > 0)
            {
                int nuevoId = clientes.Any() ? clientes.Max(c => c.Cliente_ID) + 1 : 1;
                clientes.Add(new ClienteDTO { Cliente_ID = nuevoId, Nombre = nombre, NumeroTelefono = telefono });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Modificar(int clienteId, string nombre, int telefono)
        {
            var cliente = clientes.FirstOrDefault(c => c.Cliente_ID == clienteId);
            if (cliente != null)
            {
                cliente.Nombre = nombre;
                cliente.NumeroTelefono = telefono;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Eliminar(int clienteId)
        {
            clientes.RemoveAll(c => c.Cliente_ID == clienteId);
            return RedirectToAction("Index");
        }
    }
}