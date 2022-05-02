using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Booths.Queries.GetBooth
{
    public class GetBoothQueryValidator : AbstractValidator<GetBoothQuery>
    {
        public GetBoothQueryValidator()
        {
            RuleFor(x => x.Dto.Id)
                .NotEmpty();
        }
    }
}
