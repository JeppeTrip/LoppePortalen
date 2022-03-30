using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Markets.Commands.CancelMarket
{
    public class CancelMarketInstanceCommand : IRequest<CancelMarketInstanceResponse>
    {
        public CancelMarketInstanceRequest Dto { get; set; }

        public class CancelMarketInstanceCommandHandler : IRequestHandler<CancelMarketInstanceCommand, CancelMarketInstanceResponse>
        {
            private readonly IApplicationDbContext _context;

            public CancelMarketInstanceCommandHandler(IApplicationDbContext context)
            { 
                _context = context;
            }

            public async Task<CancelMarketInstanceResponse> Handle(CancelMarketInstanceCommand request, CancellationToken cancellationToken)
            {
                var marketInstance = _context.MarketInstances.FirstOrDefault(x => x.Id == request.Dto.MarketId);
                if(marketInstance == null)
                {
                    throw new NotFoundException($"No market instance with id {request.Dto.MarketId}.");
                }

                marketInstance.IsCancelled = true;
                _context.MarketInstances.Update(marketInstance);
                await _context.SaveChangesAsync(cancellationToken);

                return new CancelMarketInstanceResponse() { MarketId = marketInstance.Id, IsCancelled = marketInstance.IsCancelled };
            }
        }
    }
}
