using ConcesionariaVehiculos.DTOs;
using FluentValidation;


    public class VehiculoDTOValidator : AbstractValidator<VehiculoDTO>
    {
        public VehiculoDTOValidator()
        {
            RuleFor(v => v.Marca)
                .NotEmpty().WithMessage("La marca es obligatoria.")
                .MaximumLength(50);

            RuleFor(v => v.Modelo)
                .NotEmpty().WithMessage("El modelo es obligatorio.")
                .MaximumLength(50);

            RuleFor(v => v.Anio)
                .InclusiveBetween(1950, DateTime.Now.Year + 1)
                .WithMessage("El año debe ser válido.");

            RuleFor(v => v.Precio)
                .GreaterThan(0).WithMessage("El precio debe ser mayor a cero.");

            RuleFor(v => v.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser negativo.");
        }
    }
