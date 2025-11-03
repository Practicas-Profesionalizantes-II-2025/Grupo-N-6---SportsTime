using System.Security.Cryptography;
using System.Text;
using CDatos.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Entidades;

namespace MVCSPortTime1.Services
{
    public interface IAuthService
    {
        Task<Usuarios?> ValidateAsync(string usuarioOEmail, string password);
        string HashPassword(string password);
    }

    public class AuthService : IAuthService
    {
        private const int Iterations = 100_000;
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private readonly DataContext _db;
        public AuthService(DataContext db) { _db = db; }

        public async Task<Usuarios?> ValidateAsync(string usuarioOEmail, string password)
        {
            var input = (usuarioOEmail ?? string.Empty).Trim();

            // 1) Buscar por Email o por Nombre de usuario
            var user = await _db.Usuarios
                .FirstOrDefaultAsync(u => u.Email == input || u.Nombre == input);

            // 2) Si no se encontró, permitir login por nombre del Cliente
            if (user == null)
            {
                var cli = await _db.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.Nombre == input && c.Usuario_ID != null);
                if (cli?.Usuario_ID != null)
                {
                    user = await _db.Usuarios.FindAsync(cli.Usuario_ID);
                }
            }

            if (user == null) return null;
            if (string.IsNullOrEmpty(user.PasswordHash)) return null;

            // Primero intentó validar como PBKDF2 v=1
            var ok = VerifyPassword(password, user.PasswordHash);

            // Compatibilidad: si no es válido como PBKDF2, intentamos texto plano heredado
            if (!ok && string.Equals(user.PasswordHash, password))
            {
                ok = true;
                // Migración silenciosa: actualizar a PBKDF2
                user.PasswordHash = HashPassword(password);
                try { await _db.SaveChangesAsync(); } catch { /* ignore */ }
            }

            return ok ? user : null;
        }

        public string HashPassword(string password)
        {
            using var rng = RandomNumberGenerator.Create();
            Span<byte> salt = stackalloc byte[SaltSize];
            rng.GetBytes(salt);
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt.ToArray(), Iterations, HashAlgorithmName.SHA256);
            var key = pbkdf2.GetBytes(KeySize);
            return $"v=1|{Convert.ToBase64String(salt)}|{Convert.ToBase64String(key)}";
        }

        private static bool VerifyPassword(string password, string stored)
        {
            if (!stored.StartsWith("v=1|")) return false;
            var parts = stored.Split('|');
            if (parts.Length != 3) return false;
            var salt = Convert.FromBase64String(parts[1]);
            var sub = Convert.FromBase64String(parts[2]);
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            var attempt = pbkdf2.GetBytes(sub.Length);
            return CryptographicOperations.FixedTimeEquals(attempt, sub);
        }
    }
}
