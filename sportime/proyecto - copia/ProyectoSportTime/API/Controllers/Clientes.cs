using CDatos.Data;
using Microsoft.AspNetCore.Mvc;
using Shared.Entidades;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using CNegocio.Contracts;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClientes _clientesLogic;

        public ClientesController(IClientes clientesLogic)
        {
            _clientesLogic = clientesLogic;
        }

        // GET: api/clientes (Obtener todos los clientes)
        [HttpGet]
        public async Task<IActionResult> ObtenerTodosLosClientes()
        {
            try
            {
                var clientes = await _clientesLogic.ObtenerTodosLosClientes();
                return Ok(clientes);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al obtener los clientes.");
            }
        }
        /* public async Task<ActionResult<List<ClienteDTO>>> Get()
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
        */

        // GET: api/clientes/{id} (Obtener un cliente específico por ID)
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerClientePorId(int id)
        {
            try
            {
                var cliente = await _clientesLogic.ObtenerClientePorId(id);
                if (cliente == null)
                    return NotFound("Cliente no encontrado.");

                return Ok(cliente);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al obtener el cliente.");
            }
        }
        /* public async Task<ActionResult<ClienteDTO>> Get(int id)
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
        */

        // POST: api/clientes (Alta de un nuevo cliente)
        [HttpPost]
        public async Task<IActionResult> AltaCliente([FromBody] ClienteDTO nuevoCliente)
        {
            if (nuevoCliente == null)
                return BadRequest("Datos de cliente inválidos.");

            try
            {
                await _clientesLogic.AltaCliente(nuevoCliente);
                return Ok("Cliente creado correctamente.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al crear el cliente.");
            }
        }
            /* public async Task<ActionResult<ClienteDTO>> Post([FromBody] ClienteDTO nuevoClienteDto)
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
            */

            // PUT: api/clientes/{id} (Modificar un cliente existente)
            [HttpPut("{id}")]
        public async Task<IActionResult> ModificarCliente(int id, [FromBody] ClienteDTO clienteModificado)
        {
            if (clienteModificado == null)
                return BadRequest("Datos de cliente inválidos.");

            try
            {
                await _clientesLogic.ModificarCliente(id, clienteModificado);
                return Ok("Cliente actualizado correctamente.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Cliente no encontrado.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al actualizar el cliente.");
            }
        }
        /* public async Task<ActionResult> Put(int id, [FromBody] ClienteDTO clienteModificadoDto)
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
        */

        // DELETE: api/clientes/{id} (Eliminar un cliente)
        [HttpDelete("{id}")]
        public async Task<IActionResult> BajaCliente(int id)
        {
            try
            {
                await _clientesLogic.BajaCliente(id);
                return Ok("Cliente eliminado correctamente.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Cliente no encontrado.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al eliminar el cliente.");
            }
        }
        /* public async Task<ActionResult> Delete(int id)
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
        */
    }

}

