using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Organisers.Commands.EditOrganiser
{
    public class EditOrganiserCommandValidator : AbstractValidator<EditOrganiserCommand>
    {
        public EditOrganiserCommandValidator()
        {
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

            RuleFor(e => e.Dto.OrganiserId)
                .NotEmpty();
        }
    }
}
