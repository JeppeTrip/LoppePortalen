using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.StallTypes.Queries.GetMarketStallTypes
{
    public class GetMarketStallTypesQuery : IRequest<GetMarketStallTypesResponse>
    {
        public GetMarketStallTypesRequest Dto { get; set; }

        public class GetMarketStallTypesQueryHandler : IRequestHandler<GetMarketStallTypesQuery, GetMarketStallTypesResponse>
        {
            private readonly IApplicationDbContext _context;

            public GetMarketStallTypesQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<GetMarketStallTypesResponse> Handle(GetMarketStallTypesQuery request, CancellationToken cancellationToken)
            {
                var instance = await _context.MarketInstances
                    .Include(x => x.MarketTemplate)
                    .Include(x => x.MarketTemplate.StallTypes)
                    .FirstOrDefaultAsync(x => x.Id == request.Dto.MarketId);
                var stallTypes = instance.MarketTemplate.StallTypes;


                return new GetMarketStallTypesResponse()
                {
                    StallTypes = stallTypes.Select(x => new GetMarketStallTypesVM() { 
                        Id = x.Id, 
                        Name = x.Name, 
                        Description = x.Description,
                        NumberOfStalls = _context.Stalls.Where(s => s.StallTypeId == x.Id).Count() })
                    .ToList(),
                };
            }
        }
    }
}
