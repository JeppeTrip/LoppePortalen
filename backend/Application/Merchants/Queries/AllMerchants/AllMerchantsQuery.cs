using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Merchants.Queries.AllMerchants
{
    public class AllMerchantsQuery : IRequest<AllMerchantsQueryResponse>
    {
        public class AllMerchantsQueryHandler : IRequestHandler<AllMerchantsQuery, AllMerchantsQueryResponse>
        {
            private readonly IApplicationDbContext _context;

            public AllMerchantsQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<AllMerchantsQueryResponse> Handle(AllMerchantsQuery request, CancellationToken cancellationToken)
            {
                var merchants = await _context.Merchants.ToListAsync();
                return new AllMerchantsQueryResponse() { 
                    MerchantList = merchants.Select(
                        x => new MerchantBaseVM() { 
                            Id = x.Id, 
                            Name = x.Name, 
                            Description = x.Description, 
                            UserId = x.UserId })
                    .ToList() 
                };
            }
        }
    }
}
