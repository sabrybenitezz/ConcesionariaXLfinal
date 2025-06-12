using ConcesionariaVehiculos.DTOs;
using FluentValidation;
using FluentValidation.AppNetCore;



    public class FacturaDTOValidator : AbstractValidator<FacturaDTO>
    {
        public FacturaDTOValidator()
        {
            RuleFor(f => f.VentaId).GreaterThan(0);
            RuleFor(f => f.ClienteNombre).NotEmpty().MaximumLength(100);
            RuleFor(f => f.Total).GreaterThan(0);
            RuleFor(f => f.Fecha).LessThanOrEqualTo(DateTime.Now);
            RuleFor(f => f.NumeroFactura).NotEmpty().MaximumLength(30);
            RuleFor(f => f.Vehiculo);
        }
    }

