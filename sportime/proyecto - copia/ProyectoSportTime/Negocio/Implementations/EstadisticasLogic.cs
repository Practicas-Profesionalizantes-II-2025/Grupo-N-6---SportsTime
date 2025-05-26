using API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Implementations
{
    public class EstadisticasLogic
    {
        private readonly ProyectoDbContext _context;

        public EstadisticasLogic(ProyectoDbContext context)
        {
            _context = context;
        }

        public async Task<int> ObtenerCantidadTurnos()
        {
            return await _context.Turnos.CountAsync();
        }

        public async Task<Dictionary<string, int>> ObtenerTurnosPorCancha()
        {
            return await _context.Turnos
                .GroupBy(t => t.Canchas.DisplayName)
                .Select(g => new { Cancha = g.Key, Cantidad = g.Count() })
                .ToDictionaryAsync(x => x.Cancha, x => x.Cantidad);
        }

        public async Task<decimal> ObtenerIngresosPorConsumiciones()
        {
            return await _context.Consumiciones.SumAsync(c => c.Precio);
        }
    }
}
