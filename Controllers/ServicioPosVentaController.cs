using Microsoft.AspNetCore.Mvc;
using technical_tests_backend_ssr.Models;
using AutoMapper;

[ApiController]
[Route("api/[controller]")]
public class ServicioPosVentaController : ControllerBase
{
    private readonly ServicioPosVentaService _servicioPosVentaService;
    private readonly IMapper _mapper;

    public ServicioPosVentaController(ServicioPosVentaService servicioPosVentaService, IMapper mapper)
    {
        _servicioPosVentaService = servicioPosVentaService;
        _mapper = mapper;
    }

  
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServicioPosVenta>>> GetAll()
    {
        var servicios = await _servicioPosVentaService.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<ServicioPosVentaDTO>>(servicios));
    }

   
    [HttpGet("{id}")]
    public async Task<ActionResult<ServicioPosVenta>> GetById(int id)
    {
        var servicio = await _servicioPosVentaService.GetByIdAsync(id);
        if (servicio == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<ServicioPosVentaDTO>(servicio));
    }

   
    [HttpPost]
    public async Task<ActionResult<ServicioPosVentaDTO>> Create(ServicioPosVentaDTO servicioDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var servicio = _mapper.Map<ServicioPosVenta>(servicioDTO);
        await _servicioPosVentaService.AddAsync(servicio);
        return CreatedAtAction(nameof(GetById), new { id = servicio.Id }, _mapper.Map<ServicioPosVentaDTO>(servicio));
    }

   
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ServicioPosVentaDTO servicioDTO)
    {
        if (id != servicioDTO.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid) return BadRequest(ModelState);

        var servicio = _mapper.Map<ServicioPosVenta>(servicioDTO);
        await _servicioPosVentaService.UpdateAsync(servicio);
        return Ok(_mapper.Map<ServicioPosVentaDTO>(servicio));
    }

   
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var servicio = await _servicioPosVentaService.GetByIdAsync(id);
        if (servicio == null) return NotFound();
        await _servicioPosVentaService.DeleteAsync(id);
        return NoContent();
    }
}