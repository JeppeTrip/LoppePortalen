using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Markets.Commands.CancelMarket
{
    public class CancelMarketInstanceCommandValidator : AbstractValidator<CancelMarketInstanceCommand>
    {
        public CancelMarketInstanceCommandValidator()
        {
            RuleFor(e => e.Dto)
                .NotNull();

            RuleFor(e => e.Dto.MarketId)
                .GreaterThan(0);
        }
    }
}
