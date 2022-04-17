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

namespace Application.Merchants.Queryies.GetMerchant
{
    public class GetMerchantQuery : IRequest<GetMerchantQueryResponse>
    {
        public GetMerchantQueryRequest Dto { get; set; }

        public class GetMerchantQueryHandler : IRequestHandler<GetMerchantQuery, GetMerchantQueryResponse>
        {
            private readonly IApplicationDbContext _context;

            public GetMerchantQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<GetMerchantQueryResponse> Handle(GetMerchantQuery request, CancellationToken cancellationToken)
            {
                var merchant = await _context.Merchants.FirstOrDefaultAsync(x => x.Id == request.Dto.Id);
                if(merchant == null)
                {
                    throw new NotFoundException($"No merchant with ID {request.Dto.Id}.");
                }

                return new GetMerchantQueryResponse(Result.Success()) { Merchant= new Merchant() { Id = merchant.Id, Name=merchant.Name, Description=merchant.Description, UserId=merchant.UserId } };
            }
        }
    }
}
