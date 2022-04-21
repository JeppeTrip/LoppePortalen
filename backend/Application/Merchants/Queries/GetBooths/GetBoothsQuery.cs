using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Merchants.Queries.GetBooths
{
    public class GetBoothsQuery : IRequest<GetBoothsResponse>
    {
        public GetBoothsRequest Dto { get; set; }

        public class GetBoothsQueryHandler : IRequestHandler<GetBoothsQuery, GetBoothsResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public GetBoothsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<GetBoothsResponse> Handle(GetBoothsQuery request, CancellationToken cancellationToken)
            {
                var merchant = await _context.Merchants.FirstOrDefaultAsync(x => x.UserId.Equals(_currentUserService.UserId));
                if(merchant == null)
                {
                    throw new NotFoundException("No such merchant.");
                }

                var bookings = _context.Bookings
                    .Include(x => x.Stall)
                    .Include(x => x.Stall.StallType)
                    .Where(x => x.MerchantId == request.Dto.MerchantId);

                return new GetBoothsResponse() { Booths = bookings.Select(x => new BoothsDto() { 
                    BoothName = x.BoothName, 
                    BoothDescription = x.BoothDescription, 
                    MerchantId = x.MerchantId, 
                    StallId = x.StallId })
                    .ToList() };
            }
        }
    }
}
