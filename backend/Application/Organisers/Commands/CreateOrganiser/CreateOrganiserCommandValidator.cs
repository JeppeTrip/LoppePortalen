using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Organisers.Commands.CreateOrganiser
{
    public class CreateOrganiserCommandValidator : AbstractValidator<CreateOrganiserCommand>
    {
        public CreateOrganiserCommandValidator() { 
            RuleFor(e => e.Dto).NotEmpty();

            //TODO: Protect this by a max length.
            RuleFor(e => e.Dto.Name)
                .NotEmpty();
        }
    }
}
