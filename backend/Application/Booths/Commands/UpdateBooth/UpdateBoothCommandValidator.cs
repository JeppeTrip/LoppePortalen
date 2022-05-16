using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Booths.Commands.UpdateBooth
{
    public class UpdateBoothCommandValidator : AbstractValidator<UpdateBoothCommand>
    {
        public UpdateBoothCommandValidator()
        {
            RuleFor(e => e.Dto.Id).NotEmpty();
            RuleFor(e => e.Dto.BoothName).NotNull();
            RuleFor(e => e.Dto.BoothDescription).NotNull();
            RuleFor(e => e.Dto.ItemCategories).NotNull();
        }
    }
}
