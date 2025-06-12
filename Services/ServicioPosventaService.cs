

namespace ConcesionariaVehiculos.Services
{
    public class ServicioPosVentaService
    {
        private readonly IServicioPosVentaRepository _servicioPosVentaRepository;

        public ServicioPosVentaService(IServicioPosVentaRepository servicioPosVentaRepository)
        {
            _servicioPosVentaRepository = servicioPosVentaRepository;
        }

        public async Task<IEnumerable<ServicioPosVenta>> GetAllAsync()
        {
            return await _servicioPosVentaRepository.GetAllAsync();
        }

        public async Task<ServicioPosVenta?> GetByIdAsync(int id)
        {
            return await _servicioPosVentaRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(ServicioPosVenta servicio)
        {
            await _servicioPosVentaRepository.AddAsync(servicio);
        }

        public async Task UpdateAsync(ServicioPosVenta servicio)
        {
            var existingServicio = await _servicioPosVentaRepository.GetByIdAsync(servicio.Id);
            if (existingServicio == null)
            {
                throw new KeyNotFoundException("Servicio no encontrado");
            }
            await _servicioPosVentaRepository.UpdateAsync(servicio);
        }

        public async Task DeleteAsync(int id)
        {
            await _servicioPosVentaRepository.DeleteAsync(id);
        }
    }