using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Markets.Commands.CreateMarket
{
    public class CreateMarketCommand : IRequest<CreateMarketResponse>
    {
        public CreateMarketRequest Dto { get; set; }

        public class CreateMarketCommandHandler : IRequestHandler<CreateMarketCommand, CreateMarketResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public CreateMarketCommandHandler(
                IApplicationDbContext context,
                ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<CreateMarketResponse> Handle(CreateMarketCommand request, CancellationToken cancellationToken)
            {
                
                var organiser = _context.Organisers.FirstOrDefault(x => x.Id == request.Dto.OrganiserId && x.UserId.Equals(_currentUserService.UserId));
                if (organiser == null)
                {
                    throw new NotFoundException();
                }

                //Create market template
                MarketTemplate template = new MarketTemplate()
                {
                    Name = request.Dto.MarketName,
                    Description = request.Dto.Description,
                    Organiser = organiser,
                    OrganiserId = organiser.Id
                };
                _context.MarketTemplates.Add(template);
                
                MarketInstance instance = new MarketInstance()
                {
                    MarketTemplate = template,
                    StartDate = request.Dto.StartDate,
                    EndDate = request.Dto.EndDate
                };
                _context.MarketInstances.Add(instance);

                await _context.SaveChangesAsync(cancellationToken);
                Market market = new Market()
                {
                    MarketId = instance.Id,
                    Description = instance.MarketTemplate.Description,
                    MarketName = instance.MarketTemplate.Name,
                    StartDate = instance.StartDate,
                    EndDate = instance.EndDate,
                    IsCancelled = instance.IsCancelled
                };

                return new CreateMarketResponse(Result.Success()) { Market = market };
            }
        }

    }
}
