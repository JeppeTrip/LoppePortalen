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

namespace Application.Markets.Queries.GetMarket
{
    public class GetMarketInstanceQuery : IRequest<GetMarketInstanceQueryResponse>
    {
        public GetMarketInstanceQueryRequest Dto { get; set; }

        public class GetMarketInstanceQueryHandler : IRequestHandler<GetMarketInstanceQuery, GetMarketInstanceQueryResponse>
        {
            private readonly IApplicationDbContext _context;

            public GetMarketInstanceQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<GetMarketInstanceQueryResponse> Handle(GetMarketInstanceQuery request, CancellationToken cancellationToken)
            {
                var marketInstance = await _context.MarketInstances
                    .Include(x => x.MarketTemplate)
                    .FirstOrDefaultAsync(x => x.Id == request.Dto.MarketId);
                if (marketInstance == null)
                {
                    throw new NotFoundException("No such market.");
                }
                GetMarketInstanceQueryResponse response = new GetMarketInstanceQueryResponse() { 
                    MarketId = marketInstance.Id,
                    Description = marketInstance.MarketTemplate.Description,
                    MarketName = marketInstance.MarketTemplate.Name,
                    OrganiserId = marketInstance.MarketTemplate.OrganiserId,
                    StartDate = marketInstance.StartDate,
                    EndDate = marketInstance.EndDate,
                    IsCancelled = marketInstance.IsCancelled
                };
                return response;
            }
        }
    }
}
