
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
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
                    .Include(x => x.MarketTemplate.Organiser)
                    .Include(x => x.MarketTemplate.Organiser.Address)
                    .Include(x => x.Stalls)
                    .ToListAsync();

                var allBookings = _context.Bookings
                    .Include(x => x.Stall)
                    .Include(x => x.ItemCategories);

                int total = 0;
                int booked = 0;
                List<Category> itemCategories = new List<Category>();
                List<string> marketCategories = new List<string>();
                OrganiserBaseVM organiser;
                var result = instances.Select(market => {
                    total = market.Stalls.Count();
                    booked = allBookings.Where(b => b.Stall.MarketInstanceId.Equals(market.Id)).Count();

                    itemCategories = allBookings
                        .Where(x => x.Stall.MarketInstanceId == market.Id)
                        .SelectMany(x => x.ItemCategories)
                        .ToList();

                    marketCategories = itemCategories.Select(x => x.Name).Distinct().ToList();
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
                        TotalStallCount = total,
                        AvailableStallCount = total - booked,
                        OccupiedStallCount = booked,
                        Categories = marketCategories
                    };
                }).ToList();

                return new GetAllMarketInstancesQueryResponse() { Markets = result };
            }
        }
    }
}
