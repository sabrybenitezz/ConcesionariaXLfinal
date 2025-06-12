using ConcesionariaVehiculos.Models


namespace ConcesionariaVehiculos.Repositories

{
  public interface IServicioPosVentaRepository
{
    Task<IEnumerable<ServicioPosVenta>> GetAllAsync();
    Task<ServicioPosVenta?> GetByIdAsync(int id);
    Task AddAsync(ServicioPosVenta servicioPosVenta);
    Task UpdateAsync(ServicioPosVenta servicioPosVenta);
    Task DeleteAsync(int id);
}
}