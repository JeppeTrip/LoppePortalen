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
                        throw new NotFoundException($"No stalltype with id {request.Dto.MarketId}.");
                    }
                    var stallsToBook = _context.Stalls.Where(x => x.StallTypeId == booking.StallTypeId && x.MerchantId == null).Take(booking.BookingAmount).ToList();
                    if (stallsToBook.Count < booking.BookingAmount)
                        return new BookStallsResponse(false, new List<string>() { $"Not enough of stall: {stallType.Name}" });
                    stallsToBook.ForEach(x => { 
                        x.Merchant = merchant; 
                        x.MerchantId=merchant.Id;
                        _context.Stalls.Update(x);
                    });
                }

                await _context.SaveChangesAsync(cancellationToken);
                return new BookStallsResponse(true, new List<string>());

                

            }
        }
    }
}
