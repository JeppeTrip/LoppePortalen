using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StallTypes.Commands.EditStallTypes
{
    public class EditStallTypeCommandValidator : AbstractValidator<EditStallTypeCommand>
    {
        public EditStallTypeCommandValidator()
        {
            RuleFor(x => x.Dto.MarketId)
                .GreaterThan(0);

            RuleFor(x => x.Dto.StallTypeId)
                .GreaterThan(0);

            RuleFor(x => x.Dto.StallTypeName)
                .NotEmpty();

            RuleFor(x => x.Dto.StallTypeDescription)
                .NotNull();
        }
    }
}
