using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Organisers.Queries.GetAllOrganisersWithPagination
{
    public class GetOrganisersWithPaginationQuery : IRequest<GetOrganisersWithPaginationResponse>
    {
        public GetOrganisersWithPaginationRequest Dto { get; set; }

        public class GetAllOrganisersWithPaginationQueryHandler : IRequestHandler<GetOrganisersWithPaginationQuery, GetOrganisersWithPaginationResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper; 

            public GetAllOrganisersWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GetOrganisersWithPaginationResponse> Handle(GetOrganisersWithPaginationQuery request, CancellationToken cancellationToken)
            {
                var paginatedOrganisers = await _context.Organisers
                    .OrderBy(x => x.Name)
                    .Select(x => new Organiser { Name = x.Name, Id = x.Id})
                    .PaginatedListAsync(request.Dto.PageNumber, request.Dto.PageSize);


                return new GetOrganisersWithPaginationResponse() { Organisers = paginatedOrganisers };
            }
        }

    }
}
