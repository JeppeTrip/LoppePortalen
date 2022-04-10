using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Organisers.Commands.CreateOrganiser
{
    public class CreateOrganiserCommandValidator : AbstractValidator<CreateOrganiserCommand>
    {
        public CreateOrganiserCommandValidator() { 
            RuleFor(e => e.Dto).NotEmpty();

            //TODO: Check these rules and maybe specify.
            RuleFor(e => e.Dto.Name)
                .NotEmpty();

            RuleFor(e => e.Dto.Street)
                .NotEmpty();

            RuleFor(e => e.Dto.Number)
                .NotEmpty();

            RuleFor(e => e.Dto.PostalCode)
                .NotEmpty();

            RuleFor(e => e.Dto.City)
                .NotEmpty();

            RuleFor(e => e.Dto.Description)
                .NotNull();

            RuleFor(e => e.Dto.UserId)
                .NotEmpty();
        }
    }
}
