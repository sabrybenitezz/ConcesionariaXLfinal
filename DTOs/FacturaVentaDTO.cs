public class FacturaVentaDTO
{
    public int VentaId { get; set; }
    public string ClienteNombre { get; set; }
    public Vehiculo Vehiculo { get; set; }
    public decimal Total { get; set; }
    public DateTime Fecha { get; set; }
    public string NumeroFactura { get; set; }
}