using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Stalls.Queries.GetStall
{
    public class GetStallQueryValidator : AbstractValidator<GetStallQuery>
    {
        public GetStallQueryValidator()
        {
            RuleFor(x => x.Dto.StallId)
                .GreaterThan(0);
        }
    }
}
