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
            _context.Deportes.Add(deporte);
            await _context.SaveChangesAsync();
            return deporte;
        }

        public async Task<Deportes> ModificarDeporte(Deportes deporte)
        {
            var existente = _context.Deportes.Find(deporte.Deporte_ID);
            if (existente == null)
                throw new Exception("Deporte no encontrado.");

            existente.Nombre = deporte.Nombre;
            await _context.SaveChangesAsync();
            return existente;
        }

        public void EliminarDeporte(int deporteId)
        {
            var deporte = _context.Deportes.FirstOrDefault(d => d.Deporte_ID == deporteId);
            if (deporte != null)
            {
                _context.Deportes.Remove(deporte);
                _context.SaveChanges();
            }
        }

        public async Task<List<Deportes>> ObtenerTodosLosDeportes()
        {
            return await _context.Deportes.ToListAsync();
        }

        public async Task<Deportes?> ObtenerDeportePorId(int deporteId)
        {
            return await _context.Deportes.FindAsync(deporteId);
        }
    }
}


