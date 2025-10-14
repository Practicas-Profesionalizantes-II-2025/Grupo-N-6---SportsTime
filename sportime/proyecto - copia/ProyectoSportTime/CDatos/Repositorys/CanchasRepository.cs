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
            _context.Canchas.Add(cancha);
            await _context.SaveChangesAsync();
            return cancha;
        }

        public async Task<Canchas> ModificarCancha(Canchas cancha)
        {
            var existente = _context.Canchas.Find(cancha.Cancha_ID);
            if (existente == null)
                throw new Exception("Cancha no encontrada.");

            existente.Deporte_ID = cancha.Deporte_ID;
            existente.Activa = cancha.Activa;

            await _context.SaveChangesAsync();
            return existente;
        }

        public void EliminarCancha(int canchaId)
        {
            var cancha = _context.Canchas.FirstOrDefault(c => c.Cancha_ID == canchaId);
            if (cancha != null)
            {
                _context.Canchas.Remove(cancha);
                _context.SaveChanges();
            }
        }

        public async Task<List<Canchas>> ObtenerTodasLasCanchas()
        {
            return await _context.Canchas.ToListAsync();
        }

        public async Task<Canchas?> ObtenerCanchaPorId(int canchaId)
        {
            return await _context.Canchas.FindAsync(canchaId);
        }

        public async Task<List<Canchas>> ObtenerCanchasPorDeporteId(int deporteId)
        {
            return await _context.Canchas.Where(c => c.Deporte_ID == deporteId).ToListAsync();
        }
    }
}
