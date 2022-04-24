
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
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

                var bookings = await _context.Bookings
                    .Include(x => x.Stall)
                    .ToListAsync();

                int total = 0;
                int booked = 0;
                OrganiserBaseVM organiser;
                var result = instances.Select(x => {
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
                    return new GetAllMarketsVM()
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

                return new GetAllMarketInstancesQueryResponse() { Markets = result };
            }
        }
    }
}
