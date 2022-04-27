using FluentValidation;

namespace Application.Merchants.Commands.EditMerchant
{
    public class EditMerchantCommandValidator : AbstractValidator<EditMerchantCommand>
    {
        public EditMerchantCommandValidator()
        {
            RuleFor(e => e.Dto).NotEmpty();
            RuleFor(e => e.Dto.Id)
                .GreaterThan(0)
                .NotEmpty();
            RuleFor(e => e.Dto.Name).NotEmpty();
            RuleFor(e => e.Dto.Description).NotNull();
        }
    }
}
