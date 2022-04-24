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

                var bookings = await _context.Bookings
                    .Include(x => x.Stall)
                    .ToListAsync();

                int total = 0;
                int booked = 0;

                var result = instances.Select(x =>
                {
                    total = x.Stalls.Count();
                    booked = bookings.Where(b => b.Stall.MarketInstanceId.Equals(x.Id)).Count();
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
                    return new UsersMarketsVM()
                    {
                        MarketId = x.Id,
                        Description = x.MarketTemplate.Description,
                        MarketName = x.MarketTemplate.Name,
                        Organiser = organiser,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate,
                        IsCancelled = x.IsCancelled,
                        TotalStallCount = total,
                        AvailableStallCount = total - booked,
                        OccupiedStallCount = booked
                    };
                }).ToList();


                return new GetUsersMarketsResponse() { Markets = result };
            }
        }
    }
}
