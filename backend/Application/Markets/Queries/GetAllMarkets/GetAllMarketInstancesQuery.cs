
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

namespace Application.Markets.Queries.GetAllMarkets
{
    public class GetAllMarketInstancesQuery : IRequest<GetAllMarketInstancesQueryResponse>
    {
        public class GetAllMarketInstancesQueryHandler : IRequestHandler<GetAllMarketInstancesQuery, GetAllMarketInstancesQueryResponse>
        {
            private readonly IApplicationDbContext _context;

            public GetAllMarketInstancesQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<GetAllMarketInstancesQueryResponse> Handle(GetAllMarketInstancesQuery request, CancellationToken cancellationToken)
            {
                var instances = await _context.MarketInstances.Include(x => x.MarketTemplate).ToListAsync();

                var result = instances.Select(x => new Market() {
                    MarketId = x.Id,
                    Description = x.MarketTemplate.Description,
                    MarketName = x.MarketTemplate.Name,
                    OrganiserId = x.MarketTemplate.OrganiserId,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    IsCancelled = x.IsCancelled
                }).ToList();

                return new GetAllMarketInstancesQueryResponse() { Markets = result };
            }
        }
    }
}
