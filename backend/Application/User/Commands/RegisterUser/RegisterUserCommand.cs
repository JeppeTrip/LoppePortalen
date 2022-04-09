using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User.Commands.CreateUser
{
    public class RegisterUserCommand : IRequest<RegisterUserResponse>
    {
        public RegisterUserRequest Dto { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly IIdentityService _identityService;

            public CreateUserCommandHandler(
                IApplicationDbContext context,
                IIdentityService identityService)
            {
                _context = context;
                _identityService = identityService;
            }

            public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                var createRes = await _identityService.CreateUserAsync(request.Dto.Email, request.Dto.Password);
                if (!createRes.Result.Succeeded)
                {
                    return new RegisterUserResponse(createRes.Result.Succeeded, createRes.Result.Errors, "", "");
                }

                var newUser = new Domain.Entities.User()
                {
                    IdentityId = new Guid(createRes.UserId),
                    FirstName = request.Dto.FirstName,
                    LastName = request.Dto.LastName,
                    Email = request.Dto.Email,
                    DateOfBirth = request.Dto.DateOfBirth,
                    Phone = request.Dto.PhoneNumber,
                    Country = request.Dto.Country
                };

                _context.UserInfo.Add(newUser);
                await _context.SaveChangesAsync(cancellationToken);

                var token = await _identityService.AuthenticateUser(newUser.Email, request.Dto.Password);

                return new RegisterUserResponse(
                    createRes.Result.Succeeded, 
                    createRes.Result.Errors, 
                    token.Tokens.JwtToken, 
                    token.Tokens.RefreshToken.Token) { Id=newUser.IdentityId.ToString()};
            }
        }
    }
}
