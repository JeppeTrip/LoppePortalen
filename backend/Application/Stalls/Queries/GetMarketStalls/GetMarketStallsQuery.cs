using Application.Common.Exceptions;
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

namespace Application.Stalls.Queries.GetMarketStalls
{
    public class GetMarketStallsQuery : IRequest<GetMarketStallsResponse>
    {
        public GetMarketStallsRequest Dto { get; set; }

        public class GetMarketStallsQueryHandler : IRequestHandler<GetMarketStallsQuery, GetMarketStallsResponse>
        {
            private readonly IApplicationDbContext _context;

            public GetMarketStallsQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<GetMarketStallsResponse> Handle(GetMarketStallsQuery request, CancellationToken cancellationToken)
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

                var result = new List<StallBaseVM>();

                foreach(var stall in stalls)
                {
                    result.Add(new StallBaseVM()
                    {
                        Id = stall.Id,
                        StallType = new StallTypeBaseVM()
                        {
                            Id = stall.StallType.Id,
                            Name = stall.StallType.Name,
                            Description = stall.StallType.Description
                        }
                    });
                }

                return new GetMarketStallsResponse()
                {
                    Stalls = result
                };
            }
        }
    }
}
