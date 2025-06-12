using ConcesionariaVehiculos.Data;
using ConcesionariaVehiculos.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcesionariaVehiculos.Repositories
{
    public class ServicioPosventaRepository : IServicioPosVentaRepository
    {
        private readonly AppDbContext _context;

        public ServicioPosventaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ServicioPosventa>> GetAllAsync()
        {
            return await _context.ServiciosPosventa.ToListAsync();
        }

        public async Task<ServicioPosventa?> GetByIdAsync(int id)
        {
            return await _context.ServiciosPosventa.FindAsync(id);
        }

        public async Task AddAsync(ServicioPosventa servicio)
        {
            await _context.ServiciosPosventa.AddAsync(servicio);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ServicioPosventa servicio)
        {
            _context.ServiciosPosventa.Update(servicio);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var servicio = await _context.ServiciosPosventa.FindAsync(id);
            if (servicio != null)
            {
                _context.ServiciosPosventa.Remove(servicio);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ServicioPosventa>> GetServiciosPendientesAsync()
        {
            return await _context.ServiciosPosventa
                .Where(s => s.Estado == "Pendiente")
                .ToListAsync();
        }
    }
}

