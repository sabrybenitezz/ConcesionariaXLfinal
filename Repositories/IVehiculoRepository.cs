using ConcesionariaVehiculos.Models;

namespace ConcesionariaVehiculos.Repositories
{
    public interface IVehiculoRepository
    {
        Task<List<Vehiculo>> GetAllAsync();
        Task<Vehiculo?> GetByIdAsync(int id);
        Task AddAsync(Vehiculo vehiculo);
        Task UpdateAsync(Vehiculo vehiculo);
        Task DeleteAsync(int id);
    }
}
