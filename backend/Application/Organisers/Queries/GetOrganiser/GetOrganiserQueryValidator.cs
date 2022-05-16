using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Organisers.Queries.GetOrganiser
{
    public class GetOrganiserQueryValidator : AbstractValidator<GetOrganiserQuery>
    {
        public GetOrganiserQueryValidator()
        {
            RuleFor(e => e.Dto.Id)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
