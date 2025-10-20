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
    public class CanchasRepository : ICanchasRepository
    {
        private readonly DataContext _context;
        public CanchasRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Canchas> CrearCancha(Canchas cancha)
        {
            if (cancha == null) throw new ArgumentNullException(nameof(cancha));
            _context.Canchas.Add(cancha);
            await _context.SaveChangesAsync();
            return cancha;
        }

        public async Task<Canchas> ModificarCancha(Canchas cancha)
        {
            if (cancha == null) throw new ArgumentNullException(nameof(cancha));
            var existente = await _context.Canchas.FindAsync(cancha.Cancha_ID);
            if (existente == null) throw new Exception("Cancha no encontrada.");

            existente.Deporte_ID = cancha.Deporte_ID;
            existente.Activa = cancha.Activa;
            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task EliminarCancha(int canchaId)
        {
            var cancha = await _context.Canchas.FindAsync(canchaId);
            if (cancha != null)
            {
                _context.Canchas.Remove(cancha);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Canchas>> ObtenerTodasLasCanchas()
        {
            return await _context.Canchas.ToListAsync();
        }

        public async Task<Canchas?> ObtenerCanchaPorId(int id)
        {
            return await _context.Canchas.FindAsync(id);
        }

        public async Task<List<Canchas>> ObtenerCanchasPorDeporte(int deporteId)
        {
            return await _context.Canchas
                .Where(c => c.Deporte_ID == deporteId)
                .ToListAsync();
        }

        public async Task<List<Canchas>> ObtenerCanchasActivas()
        {
            return await _context.Canchas
                .Where(c => c.Activa)
                .ToListAsync();
        }

        public async Task<bool> ExisteCanchaPorId(int canchaId)
        {
            return await _context.Canchas.AnyAsync(c => c.Cancha_ID == canchaId);
        }

        public async Task<bool> ExisteCanchaConMismoDeporte(int deporteId)
        {
            // Devuelve true si existe al menos una cancha con ese Deporte_ID
            return await _context.Canchas.AnyAsync(c => c.Deporte_ID == deporteId);
        }
    }
}