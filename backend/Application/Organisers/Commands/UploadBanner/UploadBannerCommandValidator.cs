using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Organisers.Commands.UploadBanner
{
    public class UploadBannerCommandValidator : AbstractValidator<UploadBannerCommand>
    {
        public UploadBannerCommandValidator()
        {
            RuleFor(e => e.Dto.Title).NotEmpty();
            RuleFor(e => e.Dto.OrganiserId).GreaterThan(0);
            RuleFor(e => e.Dto.ImageData).NotEmpty();
        }
    }
}
