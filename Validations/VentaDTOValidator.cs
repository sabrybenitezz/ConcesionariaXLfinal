using ConcesionariaVehiculos.DTOs;
using FluentValidation;
using FluentValidation.AspNetCore;


    public class VentaDTOValidator : AbstractValidator<VentaDTO>
    {
        public VentaDTOValidator()
        {
            RuleFor(v => v.ClienteId)
                .GreaterThan(0).WithMessage("El ClienteId debe ser mayor a cero.");

            RuleFor(v => v.VehiculoId)
                .GreaterThan(0).WithMessage("El VehiculoId debe ser mayor a cero.");

            RuleFor(v => v.Total)
                .GreaterThan(0).WithMessage("El total debe ser mayor a cero.");

            RuleFor(v => v.Fecha)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("La fecha no puede ser futura.");

            
            
        }
    }

