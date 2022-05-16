
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Domain.EntityExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Markets.Queries.GetAllMarkets
{
    public class GetAllMarketInstancesQuery : IRequest<GetAllMarketInstancesQueryResponse>
    {
        public class GetAllMarketInstancesQueryHandler : IRequestHandler<GetAllMarketInstancesQuery, GetAllMarketInstancesQueryResponse>
        {
            private readonly IApplicationDbContext _context;

            public GetAllMarketInstancesQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<GetAllMarketInstancesQueryResponse> Handle(GetAllMarketInstancesQuery request, CancellationToken cancellationToken)
            {
                //TODO: this must be horrible performance. Might need update.
                var instances = await _context.MarketInstances
                    .Include(x => x.MarketTemplate)
                    .ThenInclude(x => x.StallTypes)
                    .Include(x => x.MarketTemplate.Organiser)
                    .ThenInclude(x => x.Address)
                    .Include(x => x.Stalls)
                    .ThenInclude(x => x.Bookings)
                    .ThenInclude(x => x.ItemCategories)
                    .Include(x => x.MarketTemplate)
                    .ToListAsync();

                OrganiserBaseVM organiser;
                var result = instances.Select(market => {
                    organiser = new OrganiserBaseVM
                    {
                        Id = market.MarketTemplate.Organiser.Id,
                        UserId = market.MarketTemplate.Organiser.UserId,
                        Name = market.MarketTemplate.Organiser.Name,
                        Description = market.MarketTemplate.Organiser.Description,
                        Street = market.MarketTemplate.Organiser.Address.Street,
                        StreetNumber = market.MarketTemplate.Organiser.Address.Number,
                        Appartment = market.MarketTemplate.Organiser.Address.Appartment,
                        PostalCode = market.MarketTemplate.Organiser.Address.PostalCode,
                        City = market.MarketTemplate.Organiser.Address.City
                    };
                    return new GetAllMarketsVM()
                    {
                        MarketId = market.Id,
                        Description = market.MarketTemplate.Description,
                        MarketName = market.MarketTemplate.Name,
                        Organiser = organiser,
                        StartDate = market.StartDate,
                        EndDate = market.EndDate,
                        IsCancelled = market.IsCancelled,
                        Categories = market.ItemCategories(),
                        TotalStallCount = market.TotalStallCount(),
                        AvailableStallCount = market.AvailableStallCount(),
                        OccupiedStallCount = market.OccupiedStallCount(),
                        Address = market.MarketTemplate.Address,
                        PostalCode = market.MarketTemplate.PostalCode,
                        City = market.MarketTemplate.City
                    };
                }).ToList();

                return new GetAllMarketInstancesQueryResponse() { Markets = result };
            }
        }
    }
}
