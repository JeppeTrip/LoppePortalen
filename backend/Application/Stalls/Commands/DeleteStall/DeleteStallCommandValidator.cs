using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Stalls.Commands.DeleteStall
{
    public class DeleteStallCommandValidator : AbstractValidator<DeleteStallCommand>
    {
        public DeleteStallCommandValidator()
        {
            RuleFor(e => e.Dto.StallId)
                .GreaterThan(0);
        }
    }
}
