using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Markets.Commands.BookStalls
{
    public class BookStallsCommandValidator : AbstractValidator<BookStallsCommand>
    {
        public BookStallsCommandValidator()
        {
            RuleFor(e => e.Dto)
                .NotEmpty();

            RuleFor(e => e.Dto.MarketId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(e => e.Dto.MerchantId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Dto.Stalls)
                .NotEmpty();

            RuleForEach(x => x.Dto.Stalls)
                .Must(stall => stall.StallTypeId > 0 && stall.BookingAmount > 0);
        }
    }
}
