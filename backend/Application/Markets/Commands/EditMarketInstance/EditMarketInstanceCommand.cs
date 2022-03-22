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

namespace Application.Markets.Commands.EditMarketInstance
{
    public class EditMarketInstanceCommand : IRequest<EditMarketInstanceResponse>
    {
        public EditMarketInstanceRequest Dto { get; set; }

        public class EditMarketInstanceCommandHandler : IRequestHandler<EditMarketInstanceCommand, EditMarketInstanceResponse>
        {
            private readonly IApplicationDbContext _context;

            public EditMarketInstanceCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            /*
                Only update the instance. 
                If there are more than one instance of this type of market template, then create a new template 
                with this new information.
             */
            public async Task<EditMarketInstanceResponse> Handle(EditMarketInstanceCommand request, CancellationToken cancellationToken)
            {
                var marketInstance = _context.MarketInstances
                    .Include(x => x.MarketTemplate)
                    .FirstOrDefault(x => x.Id == request.Dto.MarketInstanceId && x.MarketTemplate.OrganiserId == request.Dto.OrganiserId);

                if (marketInstance == null)
                {
                    throw new NotFoundException("No such market.");
                }

                bool hasSiblings = marketInstance.MarketTemplate.MarketInstances.Count() > 1;
                bool hasTemplateChanged = !(marketInstance.MarketTemplate.Name.Equals(request.Dto.MarketName) && marketInstance.MarketTemplate.Description.Equals(request.Dto.Description));
                if(hasSiblings && hasTemplateChanged)
                {
                    MarketTemplate newTemplate = new MarketTemplate() { 
                        Name = request.Dto.MarketName,
                        OrganiserId = request.Dto.OrganiserId,
                        Description = request.Dto.Description
                    };
                    _context.MarketTemplates.Add(newTemplate);
                    marketInstance.MarketTemplate = newTemplate;
                    marketInstance.StartDate = request.Dto.StartDate;
                    marketInstance.EndDate = request.Dto.EndDate;
                    _context.MarketInstances.Update(marketInstance);
                } else
                {
                    marketInstance.MarketTemplate.Name = request.Dto.MarketName;
                    marketInstance.MarketTemplate.Description = request.Dto.Description;
                    marketInstance.StartDate = request.Dto.StartDate;
                    marketInstance.EndDate = request.Dto.EndDate;
                    _context.MarketTemplates.Update(marketInstance.MarketTemplate);
                    _context.MarketInstances.Update(marketInstance);
                }
                await _context.SaveChangesAsync(cancellationToken);

                return new EditMarketInstanceResponse()
                {
                    MarketInstanceId = marketInstance.Id,
                    Description = marketInstance.MarketTemplate.Description,
                    MarketName = marketInstance.MarketTemplate.Name,
                    OrganiserId = marketInstance.MarketTemplate.OrganiserId,
                    StartDate = marketInstance.StartDate,
                    EndDate = marketInstance.EndDate
                };
                
            }
        }
    }
}
