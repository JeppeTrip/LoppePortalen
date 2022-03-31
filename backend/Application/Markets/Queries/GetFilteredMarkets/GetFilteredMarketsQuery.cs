using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Markets.Queries.GetFilteredMarkets
{
    public class GetFilteredMarketsQuery : IRequest<List<GetFilteredMarketsQueryResponse>>
    {
        public GetFilteredMarketsQueryRequest Dto { get; set; }

        public class GetFilteredMarketsQueryHandler : IRequestHandler<GetFilteredMarketsQuery, List<GetFilteredMarketsQueryResponse>>
        {
            private readonly IApplicationDbContext _context;

            public GetFilteredMarketsQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<GetFilteredMarketsQueryResponse>> Handle(GetFilteredMarketsQuery request, CancellationToken cancellationToken)
            {
                var instances = await _context.MarketInstances
                    .Include(x => x.MarketTemplate)
                    .ToListAsync();

                if (request.Dto.HideCancelled != null && (bool)request.Dto.HideCancelled)
                {
                    instances = instances.Where(x => !x.IsCancelled).ToList();
                }
                if(request.Dto.StartDate != null)
                {
                    instances = instances.Where(x => DateTimeOffset.Compare(x.StartDate, (DateTimeOffset)request.Dto.StartDate) >= 0).ToList();
                }
                if (request.Dto.EndDate != null)
                {
                    instances = instances.Where(x => DateTimeOffset.Compare(x.EndDate, (DateTimeOffset)request.Dto.EndDate) <= 0).ToList();
                }

                List<GetFilteredMarketsQueryResponse> responses = new List<GetFilteredMarketsQueryResponse>();
                foreach (var instance in instances)
                {
                    responses.Add(new GetFilteredMarketsQueryResponse
                    {
                        MarketId = instance.Id,
                        OrganiserId = instance.MarketTemplate.OrganiserId,
                        MarketName = instance.MarketTemplate.Name,
                        Description = instance.MarketTemplate.Description,
                        StartDate = instance.StartDate,
                        EndDate = instance.EndDate,
                        IsCancelled = instance.IsCancelled
                    });
                }

                return responses;
                
            }
        }
    }
}
