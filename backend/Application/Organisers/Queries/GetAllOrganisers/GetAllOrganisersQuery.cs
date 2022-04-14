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

namespace Application.Organisers.Queries.GetAllOrganisers
{
    public class GetAllOrganisersQuery : IRequest<GetAllOrganisersResponse>
    {
        public class GetAllOrganisersQueryHandler : IRequestHandler<GetAllOrganisersQuery, GetAllOrganisersResponse>
        {
            private readonly IApplicationDbContext _context;

            public GetAllOrganisersQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<GetAllOrganisersResponse> Handle(GetAllOrganisersQuery request, CancellationToken cancellationToken)
            { 
                var allOrganisers = await _context.Organisers
                    .Select(x => new Organiser() { 
                        Id = x.Id, 
                        UserId = x.UserId,
                        Name=x.Name,
                        Description = x.Description,
                        Street = x.Address.Street,
                        StreetNumber = x.Address.Number,
                        Appartment = x.Address.Appartment,
                        PostalCode = x.Address.PostalCode,
                        City = x.Address.City
                    })
                    .ToListAsync();

                allOrganisers.ForEach(o =>
                {
                    var instances = _context.MarketInstances
                        .Include(m => m.MarketTemplate);

                    var organiserInstances = instances.Where(m =>
                        m.MarketTemplate.OrganiserId == o.Id);

                    var result = organiserInstances.Select(m => new Market()
                    {
                        MarketId = m.Id,
                        MarketName = m.MarketTemplate.Name,
                        Description = m.MarketTemplate.Description,
                        StartDate = m.StartDate,
                        EndDate = m.EndDate,
                        IsCancelled = m.IsCancelled
                    }).ToList();
                    o.Markets = result;
                });

                return new GetAllOrganisersResponse() { Organisers = allOrganisers};
            }
        }
    }
}
