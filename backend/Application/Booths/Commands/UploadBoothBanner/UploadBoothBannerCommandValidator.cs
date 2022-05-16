using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Booths.Commands.UploadBoothBanner
{
    public class UploadBoothBannerCommandValidator : AbstractValidator<UploadBoothBannerCommand>
    {
        public UploadBoothBannerCommandValidator()
        {
            RuleFor(e => e.Dto.Title).NotEmpty();
            RuleFor(e => e.Dto.BoothId).NotEmpty();
            RuleFor(e => e.Dto.ImageData).NotEmpty();
        }
    }
}
