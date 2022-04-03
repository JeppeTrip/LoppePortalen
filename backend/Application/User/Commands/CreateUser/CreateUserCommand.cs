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
    public class CreateUserCommand : IRequest<CreateUserResponse>
    {
        public CreateUserRequest Dto { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
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

            public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var res = await _identityService.CreateUserAsync(request.Dto.UserName, request.Dto.Password);
                return new CreateUserResponse(res.Result.Succeeded, res.Result.Errors, res.UserId);
            }
        }
    }
}
