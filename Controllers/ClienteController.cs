using AutoMapper;
using ConcesionariaVehiculos.DTOs;
using ConcesionariaVehiculos.Models;
using ConcesionariaVehiculos.Services;
using Microsoft.AspNetCore.Mvc;



/// <summary>
///  Controlador de Cliente
///  </summary>
[Route("api/[controller]")]
[ApiController]

public class ClienteController : ControllerBase
{
    private readonly ClienteService _clienteService;
    private readonly IMapper _mapper;

    /// <summary>
    /// Constructor del controlador de Cliente
    /// </summary>
  
    public ClienteController (ClienteService clienteService, IMapper mapper)
    {
        _clienteService = clienteService;
        _mapper = mapper;
    }

    /// <summary>
    /// Obtener todos los clientes.
    /// </summary>
   
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetAll()
    {
        var clientes = await _clienteService.GetAllClientesAsync();
        return Ok(_mapper.Map<IEnumerable<ClienteDTO>>(clientes));
    }

    /// <summary>
    /// Obtener un cliente por su ID.
    /// </summary>

    [HttpGet("{id}")]
    public async Task<ActionResult<ClienteDTO>> GetById(int id)
    {
        var cliente = await _clienteService.GetClienteByIdAsync(id);
        if (cliente == null) return NotFound();
        return Ok(_mapper.Map<ClienteDTO>(cliente));
    }

    /// <summary>
    /// Agregar un nuevo cliente
    /// </summary>

    [HttpPost]
    public async Task<ActionResult<ClienteDTO>> Create(ClienteDTO clienteDTO)
    {
        // FluentValidation se hace automáticamente al verificar ModelState.IsValid.
        if (!ModelState.IsValid) return BadRequest(ModelState);
        // Mapear el DTO a la entidad Cliente y agregarlo a la base de datos.
        var cliente = _mapper.Map<Cliente>(clienteDTO);
        await _clienteService.AddClienteAsync(cliente);
        return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, _mapper.Map<ClienteDTO>(cliente));
    }

    /// <summary>
    /// Actualizar un cliente existente
    /// </summary>
   
    [HttpPut("{id}")]
    public async Task<ActionResult<ClienteDTO>> Update(int id, ClienteDTO clienteDTO)
    {
        if (id != clienteDTO.Id) return BadRequest("El ID del cliente no coincide.");
        if (!ModelState.IsValid) return BadRequest(ModelState);
        // Mapear el DTO a la entidad Cliente y actualizarlo en la base de datos.
        var cliente = _mapper.Map<Cliente>(clienteDTO);
        Console.WriteLine($"Actualizando cliente: {cliente.Id} en el controller con {id}");
        await _clienteService.UpdateClienteAsync(cliente);
        return Ok(_mapper.Map<ClienteDTO>(cliente));
    }

    /// <summary>
    /// Eliminar un cliente por su ID
    /// </summary>
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var cliente = await _clienteService.GetClienteByIdAsync(id);
        if (cliente == null) return NotFound();
        await _clienteService.DeleteClienteAsync(cliente);
        return NoContent();
    }


    //procesar en paralelo cuatro grupos diferentes de clientes (los que empiezan con C, D , M y R), y que cada grupo se procese en su propia tarea independiente
    public async Task<(List<string> C, List<string> D, List<string> M, List<string>R)> GetClientesPorLetraAsync()
    {
        var clientes = await _clienteService.GetAllAsync();

        var clientesC = clientes
            .Where(c => !string.IsNullOrWhiteSpace(c.Nombre) &&
                        char.ToUpper(c.Nombre[0]) == 'C')
            .ToList();

        var clientesD = clientes
            .Where(c => !string.IsNullOrWhiteSpace(c.Nombre) &&
                        char.ToUpper(c.Nombre[0]) == 'D')
            .ToList();

        var clientesM = clientes
            .Where(c => !string.IsNullOrWhiteSpace(c.Nombre) &&
                        char.ToUpper(c.Nombre[0]) == 'M')
            .ToList();

        var clientesR = clientes
            .Where(c => !string.IsNullOrWhiteSpace(c.Nombre) &&
                        char.ToUpper(c.Nombre[0]) == 'R')
            .ToList();



        // Cada grupo se procesa en una tarea independiente
        var tareaC = Task.Run(() =>
        {
            return clientesC
                .AsParallel()
                .WithDegreeOfParallelism(4) // opcional
                .Select(c => c.Nombre.ToUpper())
                .ToList();
        });

        var tareaD = Task.Run(() =>
        {
            return clientesD
                .AsParallel()
                .WithDegreeOfParallelism(4)
                .Select(c => c.Nombre.ToUpper())
                .ToList();
        });

        var tareaM = Task.Run(() =>
        {
            return clientesM
                .AsParallel()
                .WithDegreeOfParallelism(4)
                .Select(c => c.Nombre.ToUpper())
                .ToList();
        });
        var tareaR = Task.Run(() =>
        {
            return clientesR
                .AsParallel()
                .WithDegreeOfParallelism(4)
                .Select(c => c.Nombre.ToUpper())
                .ToList();
        });



        // Esperar a que terminen todas las tareas
        await Task.WhenAll(tareaC, tareaD, tareaM, tareaR);

        // Devolver los resultados como tupla
        return (tareaC.Result, tareaD.Result, tareaM.Result, tareaR.Result);
    }

}
