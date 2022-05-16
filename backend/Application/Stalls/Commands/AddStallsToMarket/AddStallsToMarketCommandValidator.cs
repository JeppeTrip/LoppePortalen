using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Stalls.Commands.AddStallsToMarket
{
    public class AddStallsToMarketCommandValidator : AbstractValidator<AddStallsToMarketCommand>
    {
        public AddStallsToMarketCommandValidator()
        {
            RuleFor(x => x.Dto.StallTypeId)
                .GreaterThan(0);

            RuleFor(x => x.Dto.MarketId)
                .GreaterThan(0);

            RuleFor(x => x.Dto.Number)
                .GreaterThan(0);
        }
    }
}
