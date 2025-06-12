using ConcesionariaVehiculos.DTOs;
using FluentValidation;
using FluentValidation.AspNetCore;


public class ClienteDTOValidator : AbstractValidator<ClienteDTO>
{

    public ClienteDTOValidator()
    {
        RuleFor(c => c.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .Length(3, 50).WithMessage("El nombre debe tener entre 3 y 50 caracteres.");


        RuleFor(c => c.Apellido)
               .NotEmpty().WithMessage("El nombre es obligatorio.")
               .Length(3, 50).WithMessage("El nombre debe tener entre 3 y 50 caracteres.");



        RuleFor(c => c.Email)
                .NotEmpty().WithMessage("El email es obligatorio.")
                .EmailAddress().WithMessage(" debe ser un email válido.");

        RuleFor(c => c.Telefono)
            .NotEmpty().WithMessage("El teléfono es obligatorio.")
            .Matches(@"^\d{10}$").WithMessage("El teléfono debe tener 10 dígitos.");
    }
}



