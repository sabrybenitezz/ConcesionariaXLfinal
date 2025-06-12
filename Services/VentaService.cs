

namespace ConcesionariaVehiculos.Services
{
    public class VentaService
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IVehiculoRepository _vehiculoRepository;
        private readonly IMapper _mapper;

        public VentaService(IVentaRepository ventaRepository, IClienteRepository clienteRepository, IVehiculoRepository vehiculoRepository, IMapper mapper)
        {
            _ventaRepository = ventaRepository;
            _clienteRepository = clienteRepository;
            _vehiculoRepository = vehiculoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Venta>> GetAllAsync()
        {
            return await _ventaRepository.GetAllAsync();
        }

        public async Task<Venta?> GetByIdAsync(int id)
        {
            return await _ventaRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Venta venta)
        {
            await _ventaRepository.AddAsync(venta);
        }

        public async Task UpdateAsync(Venta venta)
        {
            await _ventaRepository.UpdateAsync(venta);
        }

        /// <summary>
        /// Elimina una venta por su identificador.
        /// </summary>
        /// <param name="id">El identificador de la venta a eliminar.</param>
        public async Task DeleteAsync(int id)
        {
            await _ventaRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<VentaDTO>> GetAllVentasByClienteId(int clienteId)
        {
            var ventas = await _ventaRepository.GetAllVentasByClienteId(clienteId);
            return ventas.Select(v => new VentaDTO
            {
                Id = v.Id,
                Fecha = v.Fecha,
                Total = v.Total,
                ClienteId = v.ClienteId
            });
        }

        internal async Task<FacturaVentaDTO?> GetFacturaByVentaIdAsync(int id)
        {
            var factura = await _ventaRepository.GetByIdAsync(id);
            if (factura == null)
            {
                return null;
            }

            // Obtengo el nombre del cliente y el vehículo asociado a la venta
            var cliente = _clienteRepository.GetByIdAsync(factura.ClienteId);
            var clienteNombre = cliente.Result != null ? cliente.Result.Nombre : "Cliente no encontrado";
            var vehiculo = _vehiculoRepository.GetByIdAsync(factura.VehiculoId);
            var vehiculoCaracteristicas = vehiculo.Result != null ?
                _mapper.Map<VehiculoCaracteristicasDTO>(vehiculo.Result) : null;


            return new FacturaVentaDTO
            {
                VentaId = factura.Id,
                ClienteNombre = clienteNombre,
                Vehiculo = vehiculoCaracteristicas,
                Total = factura.Total,
                Fecha = factura.Fecha
            };
        }

       
        /// El monto total de las ganancias de todas las ventas
        public async Task<decimal> GetGananciasSecuencialesAllTime()
        {
            // Obtengo todas las ventas y utilizando paralelismo calculo las ganancias
            var ventas = await _ventaRepository.GetAllAsync();
            if (ventas == null || !ventas.Any())
            {
                return 0;
            }
            //  AsParallel para realizar el cálculo de manera paralela
            // y luego sumo los totales de cada venta
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var ganancias = ventas.Select(v =>
            { return v.Total % 2 == 0 ? v.Total * 2m : v.Total * 0.5m; }
            ).Sum();
            stopwatch.Stop();
            Console.WriteLine($"Tiempo de ejecución (secuencial): {stopwatch.ElapsedMilliseconds} ms");
            return ganancias;
        }


            // Secuencial
            var swSec = System.Diagnostics.Stopwatch.StartNew();
            var resultadoSecuencial = ventas.Select(OperacionCostosa).Sum();
            swSec.Stop();

            // Paralelo
            var swPar = System.Diagnostics.Stopwatch.StartNew();
            var resultadoParalelo = ventas.AsParallel().Select(OperacionCostosa).Sum();
            swPar.Stop();

            Console.WriteLine($"Tiempo secuencial: {swSec.ElapsedMilliseconds} ms");
            Console.WriteLine($"Tiempo paralelo: {swPar.ElapsedMilliseconds} ms");

            return (swSec.ElapsedMilliseconds, swPar.ElapsedMilliseconds, resultadoSecuencial, resultadoParalelo);
        }

        // en paralela todas las ventas que superen un monto específico.
      
        public async Task<IEnumerable<Venta>> GetVentasByMontoParalelo(decimal monto)
        {
            var ventas = await _ventaRepository.GetAllAsync();
            if (ventas == null || !ventas.Any())
            {
                return Enumerable.Empty<Venta>();
            }

            // Utilizo AsParallel para filtrar las ventas que superen el monto especificado
            var ventasFiltradas = ventas.AsParallel().Where(v => v.Total > monto).ToList();

            return ventasFiltradas;
        }
    //Procesa venta con diferente filtro en paralelo.

    public class VentaService : IVentaService
    {
        private readonly List<VentaDTO> _ventas;

        public VentaService(List<VentaDTO> ventas)
        {
            _ventas = ventas;
        }

        public async Task<(List<VentaDTO> VentasAltas, List<VentaDTO> VentasRecientes, List<VentaDTO> VentasVehiculoGrande)>
            ProcesarVentasEnParaleloAsync()
        {
            var tareaVentasAltas = Task.Run(() =>
                _ventas.Where(v => v.Total > 15000000).ToList()
            );

            var tareaVentasRecientes = Task.Run(() =>
                _ventas.Where(v => v.Fecha.Year >= 2024).ToList()
            );

            var tareaVentasVehiculoIdGrande = Task.Run(() =>
                _ventas.Where(v => v.VehiculoId > 200).ToList()
            );

            await Task.WhenAll(tareaVentasAltas, tareaVentasRecientes, tareaVentasVehiculoIdGrande);

            return (
                await tareaVentasAltas,
                await tareaVentasRecientes,
                await tareaVentasVehiculoIdGrande
            );
        }
    }
    public async Task<List<VentaDTO>> ProcesarVentasConDataflowAsync()
    {
        var resultados = new List<VentaDTO>();

        // 1. Bloque que filtra ventas con Total > 15M
        var filtroBlock = new TransformBlock<VentaDTO, VentaDTO>(venta =>
        {
            if (venta.Total > 15000000)
                return venta;
            return null!;
        });

        // 2. Bloque que  transforma las ventas válidas
        var logBlock = new ActionBlock<VentaDTO>(venta =>
        {
            if (venta != null)
            {
                Console.WriteLine($"✔ Venta válida: ClienteId={venta.ClienteId}, Total={venta.Total}");
                lock (resultados)
                {
                    resultados.Add(venta);
                }
            }
        });

        // 3. Vincular los bloques (pipeline)
        filtroBlock.LinkTo(logBlock, new DataflowLinkOptions { PropagateCompletion = true });

        // 4. Postear las ventas al pipeline
        foreach (var venta in _ventas)
        {
            await filtroBlock.SendAsync(venta);
        }

        // 5. Completar el pipeline
        filtroBlock.Complete();
        await logBlock.Completion;

        return resultados;
    }
}



