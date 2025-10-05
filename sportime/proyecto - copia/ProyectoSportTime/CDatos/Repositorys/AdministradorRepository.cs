////using Microsoft.AspNetCore.Mvc;
////using Negocio.ClienteHttp;
////using Newtonsoft.Json;
//using Shared.Dtos;
//using System;
//using CDatos.Data;
//using Shared.Entidades;
//using Microsoft.EntityFrameworkCore;
//using CDatos.Repositorys.IRepositorys;

//namespace CDatos.Repositorys
//{
//    public class AdministradorRepository : IAdministradorRepository
//    {
//        //private static readonly HttpClient client = ApiServer.ObtenerClientHttp(); // Instancia global de HttpClient

//        //private readonly DataContext _context;

//        //public AdministradorRepository(DataContext context)
//        //{
//        //    _context = context;
//        //}
//        //// Crear un nuevo administrador
//        //public async Task CrearAdministrador(Administrador administrador)
//        //{
//        //    _context.Administradores.Add(administrador);
//        //    await _context.SaveChangesAsync();
//        //}

//        //// Obtener todos los administradores
//        //public async Task<List<Administrador>> ObtenerTodosLosAdministradores()
//        //{
//        //    return await _context.Administradores.ToListAsync();

//        //}

//        //// Actualizar un administrador
//        //public async Task ModificarAdministrador(Administrador administradorModificado)
//        //{
//        //    _context.Administradores.Update(administradorModificado);
//        //    await _context.SaveChangesAsync();
//        //}

//        //// Eliminar un administrador
//        //public async Task EliminiarAdministrador(int administradorID)
//        //{
//        //    var administrador = await ObtenerAdministradorPorId(administradorID);
//        //    if (administrador == null)
//        //    {
//        //        throw new Exception("Producto no encontrado.");
//        //    }

//        //    _context.Administradores.Remove(administrador);

//        //    await _context.SaveChangesAsync();

//        //    /* var url = ApiServer.ObtenerUrlEndPoint($"/api/administrador/{adminID}");

//        //     try
//        //     {
//        //         var response = await client.DeleteAsync(url);
//        //         response.EnsureSuccessStatusCode();
//        //     }
//        //     catch (Exception ex)
//        //     {
//        //         // Manejar error
//        //         throw new ApplicationException($"Error al eliminar el administrador {adminID}: {ex.Message}");
//        //     }
//        //     */
//        //}

//        //// Obtener un administrador por ID
//        //public async Task<Administrador?> ObtenerAdministradorPorId(int id)
//        //{
//        //    return await _context.Administradores.FindAsync(id);

//        //}

//       /* public async Task<AdministradorDTO?> GetAdministradorByEmail(string email)
//        {
//            var admin = await _context.Administradores
//                .FirstOrDefaultAsync(a => a.Email == email);

//            if (admin == null) return null;

//            return new AdministradorDTO
//            {
//                Admin_ID = admin.Admin_ID,
//                Nombre = admin.Nombre,
//                Email = admin.Email,
                
//                // Otros campos si es necesario
//            };
//        }
//       */
//    }

//}
