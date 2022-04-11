using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Markets.Commands.EditMarket
{
    public class EditMarketCommand : IRequest<EditMarketResponse>
    {
        public EditMarketRequest Dto { get; set; }

        public class EditMarketCommandHandler : IRequestHandler<EditMarketCommand, EditMarketResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public EditMarketCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<EditMarketResponse> Handle(EditMarketCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
                var organiser = _context.Organisers.FirstOrDefault(x => x.Id == request.Dto.OrganiserId && x.UserId.Equals(_currentUserService.UserId));
                if (organiser == null)
                {
                    throw new NotFoundException();
                }

                var instance = _context.MarketInstances
                    .Include(x => x.MarketTemplate)
                    .FirstOrDefault(x => x.MarketTemplate.OrganiserId == organiser.Id && x.Id == request.Dto.MarketId);
                if (instance == null)
                {
                    throw new NotFoundException();
                }

                instance.StartDate = request.Dto.StartDate;
                instance.EndDate = request.Dto.EndDate;
                instance.MarketTemplate.Name = request.Dto.MarketName;
                instance.MarketTemplate.Description = request.Dto.Description;
                instance.MarketTemplate.Organiser = organiser;



                foreach (var stall in request.Dto.Stalls)
                {
                    var entity = _context.Stalls.Include(x => x.StallType).FirstOrDefault(x => x.Id == stall.Id);
                    if(entity == null)
                    {
                        var type = new StallType() { Name = stall.Name, Description = stall.Description, MarketTemplate = instance.MarketTemplate };
                        _context.StallTypes.Add(type);
                        for (int i = 0; i < stall.Count; i++)
                        {
                            _context.Stalls.Add(new Stall()
                            {
                                StallType = type
                            });
                        }
                    } 
                    else
                    {

                    }
                    
                }

                await _context.SaveChangesAsync(cancellationToken);
                return null;
            }
        }

    }
}
