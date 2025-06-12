using AutoMapper
using ConcesionariaVehiculos.Models;

namespace ConcesionariaVehiculos.Mapping
{
   public class AutoMapperProfile
    {
        CreateMap<Cliente, ClienteDTO>();
        CreateMap<ClienteDTO, Cliente>();

        CreateMap<Vehiculo, VehiculoDTO>();
        CreateMap<VehiculoDTO, Vehiculo>();

        CreateMap<Venta, VentaDTO>();
        CreateMap<VentaDTO, Venta>();

        CreateMap<ServicioPosVenta, ServicioPosVentaDTO>();
        CreateMap<ServicioPosVentaDTO, ServicioPosVenta>();
       
        CreateMap<Factura,FacturaVentaDTO>();
        CreateMap<FacturaVentaDTO, Factura>();
    

    }
}
