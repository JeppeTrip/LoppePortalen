using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Merchants.Queries.GetMerchant
{
    public class GetMerchantQueryValidator : AbstractValidator<GetMerchantQuery>
    {
        public GetMerchantQueryValidator()
        {
            RuleFor(e => e.Dto.Id)
                .GreaterThan(0);
        }
    }
}
