using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StallTypes.Queries.GetMarketStallTypes
{
    public class GetMarketStallTypesQueryValidator : AbstractValidator<GetMarketStallTypesQuery>
    {
        public GetMarketStallTypesQueryValidator()
        {
            RuleFor(x => x.Dto.MarketId)
                .GreaterThan(0);
        }
    }
}
