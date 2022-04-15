using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Stalls.Commands.RemoveStallsFromMarket
{
    public class RemoveStallsFromMarketCommand : IRequest<RemoveStallsFromMarketResponse>
    {
        public RemoveStallsFromMarketRequest Dto { get; set; }

        public class UpdateStallsOnMarketCommandHandler : IRequestHandler<RemoveStallsFromMarketCommand, RemoveStallsFromMarketResponse>
        {
            private readonly IApplicationDbContext _context;

            public UpdateStallsOnMarketCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<RemoveStallsFromMarketResponse> Handle(RemoveStallsFromMarketCommand request, CancellationToken cancellationToken)
            {
                var marketInstance = _context.MarketInstances
                    .Include(x => x.MarketTemplate)
                    .Include(x => x.MarketTemplate.StallTypes)
                    .FirstOrDefault(x => x.Id == request.Dto.MarketId);
                if (marketInstance == null)
                {
                    throw new NotFoundException($"No market instance with ID {request.Dto.MarketId}");
                }

                var type = marketInstance.MarketTemplate.StallTypes.FirstOrDefault(x => x.Id == request.Dto.StallTypeId);
                if (type == null)
                {
                    throw new NotFoundException($"No stalltype with ID {request.Dto.MarketId}");
                }

                var allStalls = await _context.Stalls.Where(x => x.StallTypeId == type.Id).ToListAsync();
                if (request.Dto.Diff > allStalls.Count)
                {
                    throw new ValidationException("Cannot remove more elements than there exists in the list");
                }
                List<Domain.Entities.Stall> stallsToRemove = allStalls.Take(request.Dto.Diff).ToList();
                _context.Stalls.RemoveRange(stallsToRemove);
                await _context.SaveChangesAsync(cancellationToken);

                return new RemoveStallsFromMarketResponse(Result.Success());
            }
        }
    }
}
