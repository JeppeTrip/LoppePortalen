using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User.Commands.RefreshJwt
{
    public class RefreshJwtCommand : IRequest<RefreshJwtResponse>
    {
        public RefreshJwtRequest Dto { get; set; }

        public class RefreshJwtCommandHandler : IRequestHandler<RefreshJwtCommand, RefreshJwtResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly IIdentityService _identityService;

            public RefreshJwtCommandHandler(
                IApplicationDbContext context,
                IIdentityService identityService)
            {
                _context = context;
                _identityService = identityService;
            }

            public async Task<RefreshJwtResponse> Handle(RefreshJwtCommand request, CancellationToken cancellationToken)
            {
                //Check validity 
                var result = await _identityService.VerifyTokens(request.Dto.Token, request.Dto.RefreshToken);
                if(!result.Succeeded)
                {
                    //Return a failure here.
                    return new RefreshJwtResponse(result.Succeeded, new List<string>() { "Error processing request."}, "", "");
                }

                //If valid mark refreshtoken as used.
                var oldRefreshToken = _context.RefreshTokens.FirstOrDefault(x => x.Token == request.Dto.RefreshToken);
                if(oldRefreshToken == null)
                {
                    //return a failure
                    return new RefreshJwtResponse(result.Succeeded, new List<string>() { "Error processing request." }, "", "");
                }

                oldRefreshToken.IsUsed = true;
                _context.RefreshTokens.Update(oldRefreshToken);

                var tokens = await _identityService.GenerateTokens(oldRefreshToken.UserId);
                _context.RefreshTokens.Add(tokens.RefreshToken);
                await _context.SaveChangesAsync(cancellationToken);

                return new RefreshJwtResponse(true, new List<string>(), tokens.JwtToken, tokens.RefreshToken.Token);

            }
        }
    }
}
