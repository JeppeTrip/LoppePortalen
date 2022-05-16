using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Markets.Commands.EditMarket
{
    public class EditMarketCommandValidator : AbstractValidator<EditMarketCommand>
    {
        public EditMarketCommandValidator()
        {
            RuleFor(e => e.Dto.MarketId)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);

            RuleFor(e => e.Dto.OrganiserId)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);

            RuleFor(e => e.Dto.MarketName)
                .NotEmpty();

            RuleFor(e => e.Dto.Description)
                .NotEmpty();

            RuleFor(e => e.Dto.EndDate)
                .GreaterThan(e => e.Dto.StartDate)
                .NotEmpty();

            RuleFor(e => e.Dto.StartDate)
                .LessThan(e => e.Dto.EndDate)
                .NotEmpty();
        }
    }
}
