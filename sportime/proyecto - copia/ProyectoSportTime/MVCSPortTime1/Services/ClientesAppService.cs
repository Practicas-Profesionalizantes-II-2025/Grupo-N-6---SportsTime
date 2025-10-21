using CDatos.Data;
using CNegocio.Contracts;
using Microsoft.EntityFrameworkCore;
using Shared.Entidades;

namespace MVCSPortTime1.Services
{
    // Servicio de aplicación para Clientes (EF directo)
    public interface IClientesAppService
    {
        Task<(bool ok, string msg)> Crear(ClienteInput input);
        Task<(bool ok, string msg)> Actualizar(int id, ClienteInput input);
        Task<(bool ok, string msg)> Eliminar(int id);
        Task<List<Clientes>> Listar();
        Task<Clientes?> Obtener(int id);
    }

    public class ClienteInput
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string Telefono { get; set; } = string.Empty;
    }

    public class ClientesAppService : IClientesAppService
    {
        private readonly DataContext _db;
        public ClientesAppService(DataContext db) { _db = db; }

        public async Task<(bool ok, string msg)> Crear(ClienteInput input)
        {
            var e = Validar(input);
            if (e != null) return (false, e);

            var entidad = new Clientes
            {
                Nombre = input.Nombre,
                Email = input.Email ?? string.Empty,
                NumeroTelefono = input.Telefono
            };
            _db.Add(entidad);
            await _db.SaveChangesAsync();
            return (true, "Cliente guardado");
        }

        public async Task<(bool ok, string msg)> Actualizar(int id, ClienteInput input)
        {
            var e = Validar(input);
            if (e != null) return (false, e);
            var cli = await _db.Set<Clientes>().FindAsync(id);
            if (cli == null) return (false, "Cliente no encontrado");
            cli.Nombre = input.Nombre;
            cli.Email = input.Email ?? string.Empty;
            cli.NumeroTelefono = input.Telefono;
            await _db.SaveChangesAsync();
            return (true, "Cliente actualizado");
        }

        public async Task<(bool ok, string msg)> Eliminar(int id)
        {
            var cli = await _db.Set<Clientes>().FindAsync(id);
            if (cli == null) return (false, "Cliente no encontrado");
            _db.Remove(cli);
            await _db.SaveChangesAsync();
            return (true, "Cliente eliminado");
        }

        public Task<List<Clientes>> Listar() => _db.Set<Clientes>().AsNoTracking().ToListAsync();
        public Task<Clientes?> Obtener(int id) => _db.Set<Clientes>().FindAsync(id).AsTask();

        private static string? Validar(ClienteInput input)
        {
            if (string.IsNullOrWhiteSpace(input.Nombre)) return "El nombre es obligatorio";
            if (string.IsNullOrWhiteSpace(input.Telefono)) return "El teléfono es obligatorio";
            return null;
        }
    }
}
