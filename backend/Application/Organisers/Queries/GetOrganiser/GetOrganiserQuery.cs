using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
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

                var instances = _context.MarketInstances
                    .Include(m => m.MarketTemplate);

                var organiserInstances = instances
                    .Where(m => m.MarketTemplate.OrganiserId == organiser.Id);

                var markets = organiserInstances.Select(m => new MarketBaseVM()
                {
                    MarketId = m.Id,
                    MarketName = m.MarketTemplate.Name,
                    Description = m.MarketTemplate.Description,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    IsCancelled = m.IsCancelled
                }).ToList();

                GetOrganiserVM result = new GetOrganiserVM()
                {
                    Id = organiser.Id,
                    UserId = organiser.UserId,
                    Name = organiser.Name,
                    Description = organiser.Description,
                    Street = organiser.Address.Street,
                    StreetNumber = organiser.Address.Number,
                    Appartment = organiser.Address.Appartment,
                    PostalCode = organiser.Address.PostalCode,
                    City = organiser.Address.City,
                    Markets = markets
                };


                return new GetOrganiserQueryResponse() { Organiser = result};
            }
        }
    }
}
