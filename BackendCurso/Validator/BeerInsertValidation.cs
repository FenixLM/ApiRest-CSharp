using BackendCurso.DTOs;
using FluentValidation;

namespace BackendCurso.Validator
{
    // Clase que valida los datos de la cerveza a insertar
    // AbstractValidator es una clase de FluentValidation
    public class BeerInsertValidation : AbstractValidator<BeerInsertDto>
    {

        public BeerInsertValidation() { 
            RuleFor(beer => beer.Name)
                .NotNull()
                .WithMessage("El nombre no puede ser nulo")
                .NotEmpty()
                .WithMessage("El nombre no puede estar vacío")
                .MaximumLength(50)
                .WithMessage("El nombre no puede tener más de 50 caracteres");
        }
    }
}
