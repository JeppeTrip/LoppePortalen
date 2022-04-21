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

namespace Application.Stalls.Queries.GetStall
{
    public class GetStallQuery : IRequest<GetStallResponse>
    { 
        public GetStallRequest Dto { get; set; }

        public class GetStallQueryHandler : IRequestHandler<GetStallQuery, GetStallResponse>
        {
            private readonly IApplicationDbContext _context;

            public GetStallQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<GetStallResponse> Handle(GetStallQuery request, CancellationToken cancellationToken)
            {
                var stall = await _context.Stalls.FirstOrDefaultAsync(x => x.Id == request.Dto.StallId);
                if (stall == null)
                    throw new NotFoundException($"No stall with Id {request.Dto.StallId}.");
                return new GetStallResponse() { StallId = stall.Id, StallTypeId = stall.StallTypeId, MarketInstanceId = stall.MarketInstanceId };
            }
        }
    }
}
