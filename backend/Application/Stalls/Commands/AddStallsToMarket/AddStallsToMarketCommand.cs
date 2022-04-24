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

namespace Application.Stalls.Commands.AddStallsToMarket
{
    public class AddStallsToMarketCommand : IRequest<AddStallsToMarketResponse>
    {
        public AddStallsToMarketRequest Dto { get; set; }

        public class AddStallsToMarketCommandHandler : IRequestHandler<AddStallsToMarketCommand, AddStallsToMarketResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public AddStallsToMarketCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<AddStallsToMarketResponse> Handle(AddStallsToMarketCommand request, CancellationToken cancellationToken)
            {
                var instance = _context.MarketInstances
                    .Include(x => x.MarketTemplate)
                    .Include(x => x.MarketTemplate.StallTypes)
                    .Include(x => x.MarketTemplate.Organiser)
                    .FirstOrDefault(x => x.Id == request.Dto.MarketId);
                if (instance == null)
                {
                    throw new NotFoundException("Market doesn't exist.");
                }

                var stallType = instance.MarketTemplate.StallTypes.FirstOrDefault(x => x.Id == request.Dto.StallTypeId);
                if(stallType == null)
                {
                    throw new NotFoundException("StallType doesn't exist for current market.");
                }

                List<Domain.Entities.Stall> stalls = new List<Domain.Entities.Stall>();
                for(int i=0; i<request.Dto.Number; i++)
                {
                    stalls.Add(new Domain.Entities.Stall() { StallType = stallType, StallTypeId = stallType.Id, MarketInstance=instance });
                }
                _context.Stalls.AddRange(stalls);
                await _context.SaveChangesAsync(cancellationToken);

                return new AddStallsToMarketResponse()
                {
                    Stalls = stalls.Select(x => new StallBaseVM() { 
                        Id = x.Id,
                        StallType = new StallTypeBaseVM()
                        {
                            Id = stallType.Id,
                            Name = stallType.Name,
                            Description = stallType.Description
                        }
                    }).ToList()
                };
                    
            }
        }
    }
}
