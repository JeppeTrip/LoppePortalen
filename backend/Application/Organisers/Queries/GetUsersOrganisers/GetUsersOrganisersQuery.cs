using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Organisers.Queries.GetUsersOrganisers
{
    public class GetUsersOrganisersQuery : IRequest<List<GetUsersOrganisersResponse>>
    {
        public GetUsersOrganisersRequest Dto { get; set; }

        public class GetUsersOrganisersQueryHandler : IRequestHandler<GetUsersOrganisersQuery, List<GetUsersOrganisersResponse>>
        {
            private readonly IApplicationDbContext _context;

            public GetUsersOrganisersQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<GetUsersOrganisersResponse>> Handle(GetUsersOrganisersQuery request, CancellationToken cancellationToken)
            {
                var organisers = await _context.Organisers.Where(x => x.UserId.ToString().Equals(request.Dto.UserId)).ToListAsync();
                var result = organisers.Select(x => new GetUsersOrganisersResponse()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Appartment = x.Address.Appartment,
                    City = x.Address.City,
                    Number = x.Address.Number,
                    PostalCode = x.Address.PostalCode,
                    Street = x.Address.Street
                }).ToList();

                return result;
            }
        }
    }
}
