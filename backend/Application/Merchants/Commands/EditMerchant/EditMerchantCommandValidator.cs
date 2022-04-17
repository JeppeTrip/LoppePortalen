using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Merchants.Commands.EditMerchant
{
    public class EditMerchantCommandValidator : AbstractValidator<EditMerchantCommand>
    {
        public EditMerchantCommandValidator()
        {
            RuleFor(e => e.Dto).NotEmpty();
            RuleFor(e => e.Dto.Id).NotEmpty();
            RuleFor(e => e.Dto.Name).NotEmpty();
            RuleFor(e => e.Dto.Description).NotEmpty();
        }
    }
}
