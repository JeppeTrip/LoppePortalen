using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Markets.Queries.GetUsersMarkets
{
    public class GetUsersMarketsQueryValidator : AbstractValidator<GetUsersMArketsQuery>
    {
        public GetUsersMarketsQueryValidator()
        {
            RuleFor(e => e.Dto.UserId)
                .NotEmpty();
        }
    }
}
