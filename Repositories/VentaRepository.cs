using ConcesionariaVehiculos.Data;
using ConcesionariaVehiculos.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcesionariaVehiculos.Repositories
{
    public class VentaRepository : IVentaRepository
    {
        private readonly AppDbContext _context;

        public VentaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Venta>> GetAllAsync()
        {
            return await _context.Ventas.ToListAsync();
        }

        public async Task<Venta?> GetByIdAsync(int id)
        {
            return await _context.Ventas.FindAsync(id);
        }

        public async Task AddAsync(Venta venta)
        {
            await _context.Ventas.AddAsync(venta);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Venta venta)
        {
            _context.Ventas.Update(venta);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta != null)
            {
                _context.Ventas.Remove(venta);
                await _context.SaveChangesAsync();
            }
        }

        
    }
}