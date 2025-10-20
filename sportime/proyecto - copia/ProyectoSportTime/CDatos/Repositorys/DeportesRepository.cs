using CDatos.Data;
using CDatos.Repositorys.IRepositorys;
using Microsoft.EntityFrameworkCore;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDatos.Repositorys
{
    public class DeportesRepository : IDeportesRepository
    {
        private readonly DataContext _context;
        public DeportesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Deportes> CrearDeporte(Deportes deporte)
        {
            if (deporte == null) throw new ArgumentNullException(nameof(deporte));
            _context.Deportes.Add(deporte);
            await _context.SaveChangesAsync();
            return deporte;
        }

        public async Task EliminarDeporte(int id)
        {
            var d = await _context.Deportes.FindAsync(id);
            if (d != null)
            {
                _context.Deportes.Remove(d);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Deportes>> ObtenerTodosLosDeportes()
        {
            return await _context.Deportes.ToListAsync();
        }

        public async Task<Deportes?> ObtenerDeportePorId(int id)
        {
            return await _context.Deportes.FindAsync(id);
        }

        public async Task<Deportes> ModificarDeporte(Deportes deporte)
        {
            if (deporte == null) throw new ArgumentNullException(nameof(deporte));
            var existente = await _context.Deportes.FindAsync(deporte.Deporte_ID);
            if (existente == null) throw new Exception("Deporte no encontrado.");
            existente.Nombre = deporte.Nombre;
            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> ExisteDeporte(int id)
        {
            return await _context.Deportes.AnyAsync(d => d.Deporte_ID == id);
        }
    }
}