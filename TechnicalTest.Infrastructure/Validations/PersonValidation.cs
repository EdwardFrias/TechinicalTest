using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTest.Core.DTOs;

namespace TechnicalTest.Infrastructure.Validations
{
    public class PersonValidation : AbstractValidator<PersonDto>
    {
        public PersonValidation()

        {
            RuleFor(a => a.FullName)
                .NotNull()
                .NotEmpty()
                .WithMessage("El nombre no puede estar vacio");

            RuleFor(a => a.DateOfBirth)
                .LessThan(DateTime.Today.AddYears(-18))
                .WithMessage("La fecha de nacimiento debe corresponder a una persona mayor de 18 años.");


        }
    }
}
