using CDatos.Data;
using Microsoft.AspNetCore.Mvc;
using Shared.Entidades;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ProyectoDbContext _context;

        // Constructor donde se inyecta el DbContext
        public ClientesController(ProyectoDbContext context)
        {
            _context = context;
        }

        // GET: api/clientes (Obtener todos los clientes)
        [HttpGet]
        public async Task<ActionResult<List<ClienteDTO>>> Get()
        {
            var clientes = await _context.Clientes.ToListAsync();
            var clienteDtos = clientes.Select(c => new ClienteDTO
            {
                Cliente_ID = c.Cliente_ID,
                Nombre = c.Nombre,
                NumeroTelefono = c.NumeroTelefono
            }).ToList();

            return Ok(clienteDtos);
        }

        // GET: api/clientes/{id} (Obtener un cliente específico por ID)
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDTO>> Get(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound(); // Devuelve 404 si no se encuentra el cliente
            }

            var clienteDto = new ClienteDTO
            {
                Cliente_ID = cliente.Cliente_ID,
                Nombre = cliente.Nombre,
                NumeroTelefono = cliente.NumeroTelefono
            };

            return Ok(clienteDto);
        }

        // POST: api/clientes (Alta de un nuevo cliente)
        [HttpPost]
        public async Task<ActionResult<ClienteDTO>> Post([FromBody] ClienteDTO nuevoClienteDto)
        {
            var nuevoCliente = new Clientes
            {
                Nombre = nuevoClienteDto.Nombre,
                NumeroTelefono = nuevoClienteDto.NumeroTelefono,
            };

            _context.Clientes.Add(nuevoCliente);
            await _context.SaveChangesAsync();

            nuevoClienteDto.Cliente_ID = nuevoCliente.Cliente_ID; // Asignar el ID generado al DTO

            return CreatedAtAction(nameof(Get), new { id = nuevoCliente.Cliente_ID }, nuevoClienteDto);
        }

        // PUT: api/clientes/{id} (Modificar un cliente existente)
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClienteDTO clienteModificadoDto)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound(); // Devuelve 404 si no se encuentra el cliente
            }

            // Actualizamos los datos del cliente
            cliente.Nombre = clienteModificadoDto.Nombre;
            cliente.NumeroTelefono = clienteModificadoDto.NumeroTelefono; // Asegúrate de que esto exista en tu modelo

            await _context.SaveChangesAsync();

            return NoContent(); // Devuelve 204 No Content
        }

        // DELETE: api/clientes/{id} (Eliminar un cliente)
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound(); // Devuelve 404 si no se encuentra el cliente
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent(); // Devuelve 204 No Content
        }
    }

}

