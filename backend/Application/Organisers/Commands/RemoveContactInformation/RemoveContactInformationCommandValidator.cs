using FluentValidation;

namespace Application.Organisers.Commands.RemoveContactInformation
{
    public class RemoveContactInformationCommandValidator : AbstractValidator<RemoveContactInformationCommand>
    {
        public RemoveContactInformationCommandValidator()
        {
            RuleFor(x => x.Dto.OrganiserId)
                .GreaterThan(0);

            RuleFor(x => x.Dto.Value)
                .NotEmpty();
        }
    }
}
