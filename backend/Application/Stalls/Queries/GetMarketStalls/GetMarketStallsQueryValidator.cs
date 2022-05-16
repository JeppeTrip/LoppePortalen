using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Stalls.Queries.GetMarketStalls
{
    public class GetMarketStallsQueryValidator : AbstractValidator<GetMarketStallsQuery>
    {
        public GetMarketStallsQueryValidator()
        {
            RuleFor(e => e.Dto.MarketId)
                .GreaterThan(0);
        }
    }
}
