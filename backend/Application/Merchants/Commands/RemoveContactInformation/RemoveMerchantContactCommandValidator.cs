using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Merchants.Commands.RemoveContactInformation
{
    public class RemoveMerchantContactCommandValidator : AbstractValidator<RemoveMerchantContactCommand>
    {
        public RemoveMerchantContactCommandValidator()
        {
            RuleFor(x => x.Dto.MerchantId)
                .GreaterThan(0);

            RuleFor(x => x.Dto.Value)
                .NotEmpty();
        }
    }
}
