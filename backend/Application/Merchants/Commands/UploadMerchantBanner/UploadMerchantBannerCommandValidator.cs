using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Merchants.Commands.UploadMerchantBanner
{
    public class UploadMerchantBannerCommandValidator : AbstractValidator<UploadMerchantBannerCommand>
    {
        public UploadMerchantBannerCommandValidator()
        {
            RuleFor(e => e.Dto.Title).NotEmpty();
            RuleFor(e => e.Dto.MerchantId).GreaterThan(0);
            RuleFor(e => e.Dto.ImageData).NotEmpty();
        }
    }
}
