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

namespace Application.Booths.Queries.GetBooth
{
    public class GetBoothQuery : IRequest<GetBoothResponse>
    {
        public GetBoothRequest Dto { get; set; }

        public class GetBoothQueryHandler : IRequestHandler<GetBoothQuery, GetBoothResponse>
        {
            private readonly IApplicationDbContext _context;

            public GetBoothQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<GetBoothResponse> Handle(GetBoothQuery request, CancellationToken cancellationToken)
            {
                var booking = await _context.Bookings.FirstOrDefaultAsync(x => x.Id.Equals(request.Dto.Id));
                if (booking == null)
                    throw new NotFoundException($"No booth with id {request.Dto.Id}");

                return new GetBoothResponse()
                {
                    Id = booking.Id,
                    BoothName = booking.BoothName,
                    BoothDescription = booking.BoothDescription,
                    MerchantId = booking.MerchantId,
                    StallId = booking.StallId
                };
            }
        }
    }
}
