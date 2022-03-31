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

namespace Application.Organisers.Queries.GetOrganiser
{
    public class GetOrganiserQuery : IRequest<GetOrganiserQueryResponse>
    {
        public GetOrganiserQueryRequest Dto { get; set; }

        public class GetOrganiserQueryHandler : IRequestHandler<GetOrganiserQuery, GetOrganiserQueryResponse>
        {
            private readonly IApplicationDbContext _context;

            public GetOrganiserQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<GetOrganiserQueryResponse> Handle(GetOrganiserQuery request, CancellationToken cancellationToken)
            {
                var organiser = await _context.Organisers
                    .Include(o => o.Address)
                    .FirstOrDefaultAsync(x => x.Id == request.Dto.Id);

                if (organiser == null)
                {
                    throw new NotFoundException($"No organiser with ID {request.Dto.Id}");
                }

                var result = new GetOrganiserQueryResponse()
                {
                    Id = organiser.Id,
                    Name = organiser.Name,
                    Description = organiser.Description,
                    Street = organiser.Address.Street,
                    Number = organiser.Address.Number,
                    Appartment = organiser.Address.Appartment,
                    PostalCode = organiser.Address.PostalCode,
                    City = organiser.Address.City
                };

                return result;
            }
        }
    }
}
