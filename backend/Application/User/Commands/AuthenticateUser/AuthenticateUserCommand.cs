using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User.Commands.AuthenticateUser
{
    public class AuthenticateUserCommand : IRequest<AuthenticateUserResponse>
    {
        public AuthenticateUserRequest Dto { get; set; }

        public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, AuthenticateUserResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly IIdentityService _identityService;

            public AuthenticateUserCommandHandler(
                IApplicationDbContext context,
                IIdentityService identityService)
            {
                _context = context;
                _identityService = identityService;
            }

            public async Task<AuthenticateUserResponse> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
            {
                var authenticated = await _identityService.AuthenticateUser(request.Dto.Email, request.Dto.Password);
                if (authenticated.Result.Succeeded)
                {
                    var jwt = authenticated.Tokens.JwtToken;
                    var refreshToken = authenticated.Tokens.RefreshToken;

                    _context.RefreshTokens.Add(refreshToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    return new AuthenticateUserResponse(authenticated.Result.Succeeded, authenticated.Result.Errors, jwt, refreshToken.Token);
                } else
                {
                    return new AuthenticateUserResponse(authenticated.Result.Succeeded, authenticated.Result.Errors, "", "");
                }
            }
        }
    }
}
