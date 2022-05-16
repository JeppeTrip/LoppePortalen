using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Merchants.Commands.CreateMerchant
{
    public class CreateMerchantCommandValidator : AbstractValidator<CreateMerchantCommand>
    {
        public CreateMerchantCommandValidator()
        {
            RuleFor(e => e.Dto).NotEmpty();
            RuleFor(e => e.Dto.Name).NotEmpty();
            RuleFor(e => e.Dto.Description).NotNull();
        }
    }
}
