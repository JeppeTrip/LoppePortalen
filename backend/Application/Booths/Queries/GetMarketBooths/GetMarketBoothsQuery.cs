using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Booths.Queries.GetMarketBooths
{
    public class GetMarketBoothsQuery : IRequest<GetMarketBoothsResponse>
    {
        public GetMarketBoothsRequest Dto { get; set; }

        public class GetMarketBoothsQueryHandler : IRequestHandler<GetMarketBoothsQuery, GetMarketBoothsResponse>
        {
            private readonly IApplicationDbContext _context;

            public GetMarketBoothsQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<GetMarketBoothsResponse> Handle(GetMarketBoothsQuery request, CancellationToken cancellationToken)
            {
                var marketInstance = await _context.MarketInstances
                    .Include(x => x.Stalls)
                    .FirstOrDefaultAsync(x => x.Id == request.Dto.MarketId);

                if (marketInstance == null)
                    throw new NotFoundException($"No market with id {request.Dto.MarketId}");

                if (!marketInstance.Stalls.Any())
                    return new GetMarketBoothsResponse() { Booths = new List<BoothDto>() };

                var stalls = marketInstance.Stalls
                    .AsQueryable()
                    .Select(x => x.Id)
                    .ToList();

                if(!stalls.Any())
                    return new GetMarketBoothsResponse() { Booths = new List<BoothDto>() };

                List<Booking> bookings = new List<Booking>();
                Booking currentBooking;
                stalls.ForEach(x =>
                {
                    currentBooking = _context.Bookings.FirstOrDefault(b => b.StallId == x);
                    if(currentBooking != null)
                        bookings.Add(currentBooking);
                });

                var booths = bookings.Select(x => new BoothDto()
                {
                    Id = x.Id,
                    BoothName = x.BoothName,
                    BoothDescription = x.BoothDescription,
                    MerchantId = x.MerchantId,
                    StallId = x.StallId
                }).ToList();

                return new GetMarketBoothsResponse() { Booths = booths };

            }
        }

    }
}
