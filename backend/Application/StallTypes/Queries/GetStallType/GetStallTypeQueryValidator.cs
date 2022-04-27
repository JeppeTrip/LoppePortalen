using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StallTypes.Queries.GetStallType
{
    public class GetStallTypeQueryValidator : AbstractValidator<GetStallTypeQuery>
    {
        public GetStallTypeQueryValidator()
        {
            RuleFor(x => x.Dto.StallTypeId)
                .GreaterThan(0);
        }
    }
}
