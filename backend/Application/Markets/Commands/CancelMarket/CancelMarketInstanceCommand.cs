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

namespace Application.Markets.Commands.CancelMarket
{
    public class CancelMarketInstanceCommand : IRequest<CancelMarketInstanceResponse>
    {
        public CancelMarketInstanceRequest Dto { get; set; }

        public class CancelMarketInstanceCommandHandler : IRequestHandler<CancelMarketInstanceCommand, CancelMarketInstanceResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public CancelMarketInstanceCommandHandler(
                IApplicationDbContext context,
                ICurrentUserService currentUserService)
            { 
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<CancelMarketInstanceResponse> Handle(CancelMarketInstanceCommand request, CancellationToken cancellationToken)
            {
                if (_currentUserService.UserId.Length == 0 || _currentUserService.UserId == null)
                {
                    throw new UnauthorizedAccessException();
                }

                var marketInstance = _context.MarketInstances
                    .Include(x => x.MarketTemplate.Organiser)
                    .Where(x => x.MarketTemplate.Organiser.UserId.Equals(_currentUserService.UserId))
                    .FirstOrDefault(x => x.Id == request.Dto.MarketId);
                if(marketInstance == null)
                {
                    throw new NotFoundException();
                }

                marketInstance.IsCancelled = true;
                _context.MarketInstances.Update(marketInstance);
                await _context.SaveChangesAsync(cancellationToken);

                return new CancelMarketInstanceResponse() { MarketId = marketInstance.Id, IsCancelled = marketInstance.IsCancelled };
            }
        }
    }
}
