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

namespace Application.Stalls.Queries.GetMarketStalls
{
    public class GetMarketStallsQuery : IRequest<List<GetMarketStallsResponse>>
    {
        public GetMarketStallsRequest Dto { get; set; }

        public class GetMarketStallsQueryHandler : IRequestHandler<GetMarketStallsQuery, List<GetMarketStallsResponse>>
        {
            private readonly IApplicationDbContext _context;

            public GetMarketStallsQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<GetMarketStallsResponse>> Handle(GetMarketStallsQuery request, CancellationToken cancellationToken)
            {
                var marketInstance = await _context.MarketInstances
                    .Include(x => x.MarketTemplate)
                    .FirstOrDefaultAsync(x => x.Id == request.Dto.MarketId);

                if(marketInstance == null)
                {
                    throw new NotFoundException($"No market with id {request.Dto.MarketId}.");
                }
                
                var stalls = await _context.Stalls
                    .Include(x => x.StallType)
                    .Where(x => x.StallType.MarketTemplateId == marketInstance.MarketTemplateId)
                    .ToListAsync();

                return stalls.Select(x => new GetMarketStallsResponse() { StallId = x.Id, StallName = x.StallType.Name, StallDescription = x.StallType.Description }).ToList();
            }

            
        }

    }
}
