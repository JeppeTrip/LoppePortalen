using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
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
                    .Include(x => x.Stalls)
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
                    return new FilteredMarketVM()
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

                return new GetFilteredMarketsQueryResponse() { Markets = result};
                
            }
        }
    }
}
