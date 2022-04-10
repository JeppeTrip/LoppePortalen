using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Queries.GetUser
{
    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator()
        {
            EnsureInstanceNotNull(nameof(GetUserQuery.Dto));

            RuleFor(e => e.Dto)
                .NotNull();

            
            RuleFor(e => e.Dto.UserId)
                .NotEmpty();
        }
    }
}
