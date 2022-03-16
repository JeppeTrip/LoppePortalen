using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Markets.Commands.CreateMarket
{
    public class CreateMarketCommandValidator : AbstractValidator<CreateMarketCommand>
    {
        public CreateMarketCommandValidator()
        {

        }
    }
}
