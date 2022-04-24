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

namespace Application.Markets.Queries.GetFilteredMarkets
{
    public class GetFilteredMarketsQuery : IRequest<GetFilteredMarketsQueryResponse>    {
        public GetFilteredMarketsQueryRequest Dto { get; set; }

        public class GetFilteredMarketsQueryHandler : IRequestHandler<GetFilteredMarketsQuery, GetFilteredMarketsQueryResponse>
        {
            private readonly IApplicationDbContext _context;

            public GetFilteredMarketsQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<GetFilteredMarketsQueryResponse> Handle(GetFilteredMarketsQuery request, CancellationToken cancellationToken)
            {
                var instances = await _context.MarketInstances
                    .Include(x => x.MarketTemplate)
                    .Include(x => x.MarketTemplate.Organiser)
                    .Include(x => x.MarketTemplate.Organiser.Address)
                    .ToListAsync();

                if (request.Dto.OrganiserId != null)
                {
                    instances = instances.Where(x => x.MarketTemplate.OrganiserId == request.Dto.OrganiserId).ToList();
                }
                if (request.Dto.HideCancelled != null && (bool)request.Dto.HideCancelled)
                {
                    instances = instances.Where(x => !x.IsCancelled).ToList();
                }

                var startDate = request.Dto.StartDate == null ? DateTimeOffset.MinValue : (DateTimeOffset) request.Dto.StartDate;
                
                var endDate = request.Dto.EndDate == null ? DateTimeOffset.MaxValue : (DateTimeOffset) request.Dto.EndDate;
                
                instances = instances.Where(
                        x => ( DateTimeOffset.Compare(x.StartDate, startDate) >= 0 || DateTimeOffset.Compare(x.EndDate, startDate) >= 0) )
                    .ToList();

                instances = instances.Where(
                    x => DateTimeOffset.Compare(x.StartDate, endDate) <= 0 || DateTimeOffset.Compare(x.EndDate, endDate) <= 0)
                    .ToList();

                OrganiserBaseVM organiser;
                var result = instances.Select(x => {
                    organiser = new OrganiserBaseVM
                    {
                        Id = x.MarketTemplate.Organiser.Id,
                        UserId = x.MarketTemplate.Organiser.UserId,
                        Name = x.MarketTemplate.Organiser.Name,
                        Description = x.MarketTemplate.Organiser.Description,
                        Street = x.MarketTemplate.Organiser.Address.Street,
                        StreetNumber = x.MarketTemplate.Organiser.Address.Number,
                        Appartment = x.MarketTemplate.Organiser.Address.Appartment,
                        PostalCode = x.MarketTemplate.Organiser.Address.PostalCode,
                        City = x.MarketTemplate.Organiser.Address.City
                    };
                    return new FilteredMarketVM()
                    {
                        MarketId = x.Id,
                        Description = x.MarketTemplate.Description,
                        MarketName = x.MarketTemplate.Name,
                        Organiser = organiser,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate,
                        IsCancelled = x.IsCancelled
                    };
                }).ToList();

                return new GetFilteredMarketsQueryResponse() { Markets = result};
                
            }
        }
    }
}
