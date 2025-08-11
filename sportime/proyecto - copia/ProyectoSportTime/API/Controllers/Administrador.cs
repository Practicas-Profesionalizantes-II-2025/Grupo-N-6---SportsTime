using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Entidades;
using System.Collections.Generic;
using System.Linq;
using BCrypt.Net;
using CDatos.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;



namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AdministradorController : ControllerBase
    {
        private readonly ProyectoDbContext _context;

        public AdministradorController(ProyectoDbContext context)
        {
            _context = context;
        }

        [HttpPost("singup")]
        public ActionResult SingUp([FromBody] AdministradorDTO adminDTO)
        {
            if(adminDTO != null)
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
        }
        // POST: api/administrador/login (Autenticación del administrador)
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDTO loginDto)
        {
            var administrador = _context.Administradores.FirstOrDefault(a => a.Email == loginDto.Email);

            if (administrador != null && BCrypt.Net.BCrypt.Verify(loginDto.Password, administrador.Contraseña))
            {
                return Ok(new
                {
                    message = "Login successful",
                    adminId = administrador.Admin_ID // Ahora devolvemos el ID del admin autenticado
                });
            }

            return Unauthorized("Invalid credentials");
        }

        // GET: api/administrador (Obtener los datos de los administradores)
        [HttpGet("get")]
        public ActionResult<List<Administrador>> Get()
        {
            List<Administrador> administradores = _context.Administradores.ToList();
            return Ok(administradores);
        }

        // PUT: api/administrador (Actualizar los datos del administrador)
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] AdministradorDTO updatedAdmin)
        {
           var administrador = _context.Administradores.Find(id);
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
        }


        // DELETE: api/administrador (Eliminar al administrador)
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var administrador = _context.Administradores.Find(id);

            if(administrador == null)
            {

                return NoContent();
            }

            _context.Remove(administrador);
            _context.SaveChanges();

            return Ok();
        }
    }
}

