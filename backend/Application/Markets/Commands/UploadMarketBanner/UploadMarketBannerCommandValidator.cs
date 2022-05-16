using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Markets.Commands.UploadMarketBanner
{
    public class UploadMarketBannerCommandValidator : AbstractValidator<UploadMarketBannerCommand>
    {
        public UploadMarketBannerCommandValidator()
        {
            RuleFor(e => e.Dto.Title).NotEmpty();
            RuleFor(e => e.Dto.MarketId).GreaterThan(0);
            RuleFor(e => e.Dto.ImageData).NotEmpty();
        }
    }
}
