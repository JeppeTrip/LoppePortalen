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

namespace Application.Merchants.Queries.GetUsersMerchants
{
    public class GetUsersMerchantsQuery : IRequest<GetUsersMerchantsResponse>
    {
        public class GetUsersMerchantsQueryHandler : IRequestHandler<GetUsersMerchantsQuery, GetUsersMerchantsResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public GetUsersMerchantsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }
            public async Task<GetUsersMerchantsResponse> Handle(GetUsersMerchantsQuery request, CancellationToken cancellationToken)
            {
                var merchants = await _context.Merchants.Where(x => x.UserId.Equals(_currentUserService.UserId)).ToListAsync();
                return new GetUsersMerchantsResponse() { 
                    Merchants = merchants.Select(x => new MerchantBaseVM() 
                    { 
                        Id = x.Id, 
                        Name = x.Name, 
                        Description = x.Description, 
                        UserId = x.UserId }).ToList() };
            }
        }
    }
}
