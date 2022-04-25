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

namespace Application.Markets.Queries.GetUsersMarkets
{
    public class GetUsersMarketsQuery : IRequest<GetUsersMarketsResponse>
    {

        public class GetUsersMarketsQueryHandler : IRequestHandler<GetUsersMarketsQuery, GetUsersMarketsResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public GetUsersMarketsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context =context;
                _currentUserService =currentUserService;
            }

            public async Task<GetUsersMarketsResponse> Handle(GetUsersMarketsQuery request, CancellationToken cancellationToken)
            {

                OrganiserBaseVM organiser;
                var instances = await _context.MarketInstances
                    .Include(x => x.MarketTemplate)
                    .Include(x => x.MarketTemplate.Organiser)
                    .Include(x => x.MarketTemplate.Organiser.Address)
                    .Include(x => x.Stalls)
                    .Where(x => x.MarketTemplate.Organiser.UserId.Equals(_currentUserService.UserId))
                    .ToListAsync();

                var bookings = _context.Bookings
                    .Include(x => x.Stall)
                    .Include(x => x.ItemCategories);

                int total = 0;
                int booked = 0;

                List<Category> itemCategories = new List<Category>();
                List<string> marketCategories = new List<string>();

                var result = instances.Select(market =>
                {
                    total = market.Stalls.Count();
                    booked = bookings.Where(b => b.Stall.MarketInstanceId.Equals(market.Id)).Count();
                    itemCategories = bookings
                        .Where(b => b.Stall.MarketInstanceId.Equals(market.Id))
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
                    return new UsersMarketsVM()
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


                return new GetUsersMarketsResponse() { Markets = result };
            }
        }
    }
}
