using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Markets.Queries.GetMarketInstance
{
    public class GetMarketInstanceQueryValidator : AbstractValidator<GetMarketInstanceQuery>
    {
        public GetMarketInstanceQueryValidator()
        {
            RuleFor(e => e.Dto)
                .NotNull();

            RuleFor(e => e.Dto.MarketId)
                .NotEmpty()
                .Must(val => val > 0);
        }
    }
}
