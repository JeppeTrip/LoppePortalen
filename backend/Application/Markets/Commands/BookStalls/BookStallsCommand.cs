using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Markets.Commands.BookStalls
{
    public class BookStallsCommand : IRequest<BookStallsResponse>
    {
        public BookStallsRequest Dto { get; set; }

        public class BookStallsCommandHandler : IRequestHandler<BookStallsCommand, BookStallsResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public BookStallsCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<BookStallsResponse> Handle(BookStallsCommand request, CancellationToken cancellationToken)
            {
                //make sure merchant exists for current user
                var merchant = await _context.Merchants.FirstOrDefaultAsync(x => x.Id == request.Dto.MerchantId && x.UserId.Equals(_currentUserService.UserId));
                if(merchant == null)
                {
                    throw new NotFoundException("No such merchant");
                }

                //make sure the market exists
                var market = await _context.MarketInstances
                    .Include(x => x.MarketTemplate)
                    .Include(x => x.MarketTemplate.StallTypes)
                    .FirstOrDefaultAsync(x => x.Id == request.Dto.MarketId);
                if(market == null)
                {
                    throw new NotFoundException($"No market with id {request.Dto.MarketId}.");
                }

                //make sure all the stall types exist
                foreach(var booking in request.Dto.Stalls)
                {
                    var stallTypes = market.MarketTemplate.StallTypes.FirstOrDefault(x => x.Id == booking.StallTypeId);
                    if(stallTypes == null)
                    {
                        throw new NotFoundException($"No stalltype with id {request.Dto.MarketId}.");
                    }
                }

                foreach (var booking in request.Dto.Stalls)
                {
                    var stallType = market.MarketTemplate.StallTypes.FirstOrDefault(x => x.Id == booking.StallTypeId);
                    if (stallType == null)
                    {
                        throw new NotFoundException($"No stalltype with id {booking.StallTypeId}.");
                    }

                    //Take a number of stalls
                    //This is currently setup to ensure a stall doesn't get booked twice, this logic needs to change 
                    //once it is possible to book stalls for variable length of time.
                    var stallsToBook = _context.Stalls.Include(x => x.Bookings)
                        .Where(x => x.StallTypeId == booking.StallTypeId && (x.Bookings == null || x.Bookings.Count == 0))
                        .Take(booking.BookingAmount)
                        .ToList();
                    if (stallsToBook.Count < booking.BookingAmount)
                        return new BookStallsResponse(false, new List<string>() { $"Not enough of stall: {stallType.Name}" });

                    //For each stall to book add it to the booking table..
                    stallsToBook.ForEach(x => {
                        var booking = new Domain.Entities.Booking()
                        {
                            Id = Guid.NewGuid().ToString(),
                            MerchantId = merchant.Id,
                            StallId = x.Id,
                            BoothName = $"{merchant.Name}'s Booth",
                            BoothDescription = ""
                        };
                        _context.Bookings.Add(booking);
                    });
                }

                await _context.SaveChangesAsync(cancellationToken);
                return new BookStallsResponse(true, new List<string>());
            }
        }
    }
}
