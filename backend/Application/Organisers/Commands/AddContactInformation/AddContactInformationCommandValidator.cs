using Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Organisers.Commands.AddContactInformation
{
    public class AddContactInformationCommandValidator : AbstractValidator<AddContactInformationCommand>
    {
        public AddContactInformationCommandValidator()
        {
            RuleFor(x => x.Dto.OrganiserId)
                .GreaterThan(0);

            RuleFor(x => x.Dto.Value)
                .NotEmpty();

            RuleFor(x => x.Dto.Type)
                .IsInEnum();
        }
    }
}
