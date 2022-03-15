using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Organisers.Queries.GetAllOrganisers
{
    public class GetAllOrganisersQuery : IRequest<List<GetAllOrganisersResponse>>
    {
        public class GetAllOrganisersQueryHandler : IRequestHandler<GetAllOrganisersQuery, List<GetAllOrganisersResponse>>
        {
            private readonly IApplicationDbContext _context;

            public GetAllOrganisersQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<GetAllOrganisersResponse>> Handle(GetAllOrganisersQuery request, CancellationToken cancellationToken)
            {
                var allOrganisers = _context.Organisers.Select(x => new GetAllOrganisersResponse() { Id = x.Id, Name=x.Name }).ToList(); 
                return allOrganisers;
            }
        }
    }
}
