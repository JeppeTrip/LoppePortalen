using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User.Queries.GetUser
{
    public class GetUserQuery : IRequest<GetUserResponse>
    {
        public GetUserRequest Dto { get; set; }

        public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserResponse>
        {
            private readonly IApplicationDbContext _context;

            public GetUserQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<GetUserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
            {
                var user = await _context.UserInfo.Where(x => x.IdentityId.Equals(request.Dto.UserId)).FirstOrDefaultAsync();
                if(user == null)
                {
                    throw new NotFoundException("No such user.");
                }
                return new GetUserResponse(true, new List<string>())
                {
                    Id = user.IdentityId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Country = user.Country,
                    DateOfBirth = user.DateOfBirth,
                    PhoneNumber = user.Phone
                };
            }
        }
    }
}
