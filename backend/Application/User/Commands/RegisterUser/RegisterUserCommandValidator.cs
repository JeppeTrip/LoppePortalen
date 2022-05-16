using Application.User.Commands.CreateUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(e => e.Dto)
                .NotNull();

            RuleFor(e => e.Dto.Email)
                .NotEmpty();

            RuleFor(e => e.Dto.Password)
                .NotEmpty();

            RuleFor(e => e.Dto.FirstName)
                .NotEmpty();

            RuleFor(e => e.Dto.LastName)
                .NotEmpty();
        }
    }
}
