using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.Entidades;
using System.Collections.Generic;
using System.Linq;
using Negocio.Contracts;
using static System.Runtime.InteropServices.JavaScript.JSType;



namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AdministradorController : ControllerBase
    {
        private readonly IAdministrador _administradorLogic;

        public AdministradorController(IAdministrador administradorLogic)
        {
            _administradorLogic = administradorLogic;
        }

        [HttpPost("singup")]
        public async Task<IActionResult> SingUp([FromBody] AdministradorDTO adminDTO)
        {
            if (adminDTO == null)
                return BadRequest("Datos de administrador inválidos.");

            try
            {
                await _administradorLogic.CrearAdministrador(adminDTO);
                return Ok(new
                {
                    success = true,
                    message = "Administrador creado exitosamente."
                });
            }
            catch (ArgumentException ex)
            {

                // Errores de validación de datos
                return BadRequest(new
                {
                    error = true,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                // Errores inesperados
                return StatusCode(500, new
                {
                    error = true,
                    message = "Ocurrió un error inesperado al crear el administrador.",
                    details = ex.Message
                });
            }
            /* if(adminDTO != null)
             {
                 var admin = new Administrador
                 {
                     Email = adminDTO.Email,
                     Nombre = adminDTO.Nombre,
                     Contraseña = BCrypt.Net.BCrypt.HashPassword(adminDTO.PasswordHash),
                     //LastLogin = adminDTO.LastLogin,


                 };

                 _context.Administradores.Add(admin);    
                 _context.SaveChanges();

                 return Ok();
             }

             return NoContent();
            */
        }
        // POST: api/administrador/login (Autenticación del administrador)
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            if (loginDto == null)
                return BadRequest(new { error = true, message = "Datos de login inválidos." });

            try
            {
                var result = await _administradorLogic.Login(loginDto);
                if (result == null)
                    return Unauthorized(new { error = true, message = "Credenciales inválidas." });

                return Ok(new
                {
                    success = true,
                    message = "Login exitoso.",
                    adminId = result.Admin_ID
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = true, message = "Error inesperado en login.", details = ex.Message });
            }
            /*  var administrador = _context.Administradores.FirstOrDefault(a => a.Email == loginDto.Email);

              if (administrador != null && BCrypt.Net.BCrypt.Verify(loginDto.Password, administrador.Contraseña))
              {
                  return Ok(new
                  {
                      message = "Login successful",
                      adminId = administrador.Admin_ID // Ahora devolvemos el ID del admin autenticado
                  });
              }

              return Unauthorized("Invalid credentials");
              */
        }

            // GET: api/administrador (Obtener los datos de los administradores)
            [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var administradores = await _administradorLogic.ObtenerTodos();
                return Ok(administradores);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al obtener los administradores.");
            }

            /*  List<Administrador> administradores = _context.Administradores.ToList();
              return Ok(administradores);
            */
        }

        // PUT: api/administrador (Actualizar los datos del administrador)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AdministradorDTO updatedAdmin)
        {
            if (updatedAdmin == null)
                return BadRequest("Datos inválidos.");

            try
            {
                await _administradorLogic.ActualizarAdministrador(id, updatedAdmin);
                return Ok("Administrador actualizado correctamente.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Administrador no encontrado.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al actualizar el administrador.");
            }

            /* var administrador = _context.Administradores.Find(id);
              // Actualizamos solo los campos permitidos
              if (administrador != null) // Verifica que el administrador exista antes de actualizar
              {
                  administrador.Nombre = updatedAdmin.Nombre;
                  administrador.Email = updatedAdmin.Email;

                  // Si se desea actualizar la contraseña, la encriptamos
                  if (!string.IsNullOrEmpty(updatedAdmin.PasswordHash))
                  {
                      administrador.Contraseña = BCrypt.Net.BCrypt.HashPassword(updatedAdmin.PasswordHash);
                  }

                  updatedAdmin.LastLogin = DateTime.Now; // Actualizamos el último login

                      _context.SaveChanges();

                  return Ok(administrador); // No contenido, porque la actualización fue exitosa
              }
              return NotFound(new { message = "Administrador no encontrado" });
            */
        }


        // DELETE: api/administrador (Eliminar al administrador)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _administradorLogic.EliminarAdministrador(id);
                return Ok("Administrador eliminado correctamente.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Administrador no encontrado.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al eliminar el administrador.");
            }
            /* var administrador = _context.Administradores.Find(id);

             if(administrador == null)
             {

                 return NoContent();
             }

             _context.Remove(administrador);
             _context.SaveChanges();

             return Ok();
             */
        }
    }
}

