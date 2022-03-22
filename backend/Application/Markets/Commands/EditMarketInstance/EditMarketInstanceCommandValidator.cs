﻿/**/using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Markets.Commands.EditMarketInstance
{
    public class EditMarketInstanceCommandValidator : AbstractValidator<EditMarketInstanceCommand>
    {
        public EditMarketInstanceCommandValidator()
        {
            RuleFor(e => e.Dto.OrganiserId)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);

            RuleFor(e => e.Dto.MarketInstanceId)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);

            RuleFor(e => e.Dto.MarketName)
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
