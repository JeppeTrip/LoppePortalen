using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StallTypes.Commands.CreateStallType
{
    public class CreateStallTypeCommandValidator : AbstractValidator<CreateStallTypeCommand>
    {
        public CreateStallTypeCommandValidator()
        {
            RuleFor(x => x.Dto.MarketId)
                .GreaterThan(0);

            RuleFor(x => x.Dto.Name)
                .NotEmpty();

            RuleFor(x => x.Dto.Description)
                .NotNull(); 
        }
    }
}
