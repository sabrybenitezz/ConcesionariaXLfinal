using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using technical_tests_backend_ssr.Models;

[Route("api/[controller]")]
[ApiController]
public class VentaController : ControllerBase
{
    private readonly VentaService _ventaService;
    private readonly IMapper _mapper;

    public VentaController(VentaService ventaService, IMapper mapper)
    {
        _ventaService = ventaService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VentaDTO>>> GetAll()
    {
        var ventas = await _ventaService.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<VentaDTO>>(ventas));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VentaDTO>> GetById(int id)
    {
        var venta = await _ventaService.GetByIdAsync(id);
        if (venta == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<VentaDTO>(venta));
    }

    [HttpPost]
    public async Task<ActionResult<VentaDTO>> Create(VentaDTO ventaDTO)
    {
        var venta = _mapper.Map<Venta>(ventaDTO);
        await _ventaService.AddAsync(venta);
        return CreatedAtAction(nameof(GetById), new { id = venta.Id }, _mapper.Map<VentaDTO>(venta));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<VentaDTO>> Update(int id, VentaDTO ventaDTO)
    {
        var venta = _mapper.Map<Venta>(ventaDTO);
        await _ventaService.UpdateAsync(venta);
        return Ok(_mapper.Map<VentaDTO>(venta));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _ventaService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id}/Factura")]
    public async Task<ActionResult<FacturaVentaDTO>> GetFactura(int id)
    {
        var factura = await _ventaService.GetFacturaByVentaIdAsync(id);
        if (factura == null)
            return NotFound();
        return Ok(factura);
    }

    [HttpGet("ganancias/totales/paralela")]
    public async Task<ActionResult<decimal>> GetGananciasParalelas()
    {
        var ganancias = await _ventaService.GetGananciasParalelasAllTime();
        return Ok(ganancias);
    }

    [HttpGet("ganancias/totales/secuencial")]
    public async Task<ActionResult<decimal>> GetGananciasSecuenciales()
    {
        var ganancias = await _ventaService.GetGananciasSecuencialesAllTime();
        return Ok(ganancias);
    }


    [HttpGet("procesar")]
    public async Task<IActionResult> ProcesarVentas()
    {
        var (ventasAltas, ventasRecientes, ventasVehiculoIdGrande) =
            await _ventaService.ProcesarVentasEnParaleloAsync();

        return Ok(new
        {
            VentasConTotalAlto = ventasAltas,
            VentasDesde2024 = ventasRecientes,
            VentasVehiculoIdMayor200 = ventasVehiculoIdGrande
        });
    }

    [HttpGet("procesar-dataflow")]
    public async Task<IActionResult> ProcesarConDataflow()
    {
        var resultado = await _ventaService.ProcesarVentasConDataflowAsync();
        return Ok(resultado);
    }











}



