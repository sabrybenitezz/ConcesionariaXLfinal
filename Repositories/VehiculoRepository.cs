using ConcesionariaVehiculos.Data;
using ConcesionariaVehiculos.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcesionariaVehiculos.Repositories
{
    public class VehiculoRepository : IVehiculoRepository
    {
        private readonly AppDbContext _context;

        public VehiculoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Vehiculo>> GetAllAsync()
        {
            return await _context.Vehiculos.ToListAsync();
        }

        public async Task<Vehiculo?> GetByIdAsync(int id)
        {
            return await _context.Vehiculos.FindAsync(id);
        }

        public async Task AddAsync(Vehiculo vehiculo)
        {
            await _context.Vehiculos.AddAsync(vehiculo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Vehiculo vehiculo)
        {
            _context.Vehiculos.Update(vehiculo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo != null)
            {
                _context.Vehiculos.Remove(vehiculo);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Vehiculo>> BuscarPorMarcaOModeloAsync(string texto)
        {
            return await _context.Vehiculos
                .Where(v => v.Marca.Contains(texto) || v.Modelo.Contains(texto))
                .ToListAsync();
        }
    }
}


