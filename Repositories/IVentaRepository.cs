using ConcesionariaVehiculos.Models

namespace ConcesionariaVehiculos.Repositories

{
    public interface IVentaRepository
    {
        Task<IEnumerable<Venta>> GetAllAsync();
        Task<Venta?> GetByIdAsync(int id);
        Task AddAsync(Venta venta);
        Task UpdateAsync(Venta venta);
        Task DeleteAsync(int id);
        Task<IEnumerable<Venta>> GetAllVentasByClienteId(int clienteId);
    }
}