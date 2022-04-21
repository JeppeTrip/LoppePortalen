using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Booths.Queries.GetUsersBooths
{
    public class GetUsersBoothsQuery : IRequest<GetUsersBoothsResponse>
    {
        public class GetUsersBoothsQueryHandler : IRequestHandler<GetUsersBoothsQuery, GetUsersBoothsResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public GetUsersBoothsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<GetUsersBoothsResponse> Handle(GetUsersBoothsQuery request, CancellationToken cancellationToken)
            {
                var bookings = await _context.Bookings
                    .Include(x => x.Merchant)
                    .Where(x => x.Merchant.UserId.Equals(_currentUserService.UserId))
                    .ToListAsync();

                if(bookings == null || bookings.Count == 0)
                {
                    return new GetUsersBoothsResponse() { Booths = new List<BoothDto>() };
                }

                var booths = bookings.Select(x => new BoothDto()
                {
                    Id = x.Id,
                    BoothName = x.BoothName,
                    BoothDescription = x.BoothDescription,
                    MerchantId = x.MerchantId,
                    StallId = x.StallId
                }).ToList();

                return new GetUsersBoothsResponse() { Booths = booths };    
            }
        }
    }
}
