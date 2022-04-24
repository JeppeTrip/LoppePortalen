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

namespace Application.StallTypes.Commands.CreateStallTypes
{
    public class CreateStallTypesCommand : IRequest<CreateStallTypesResponse>
    {
        public CreateStallTypesRequest Dto { get; set; }

        public class CreateStallTypesCommandHandler : IRequestHandler<CreateStallTypesCommand, CreateStallTypesResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public CreateStallTypesCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<CreateStallTypesResponse> Handle(CreateStallTypesCommand request, CancellationToken cancellationToken)
            {
                var template = _context.MarketInstances
                    .Include(x => x.MarketTemplate)
                    .Include(x => x.MarketTemplate.StallTypes)
                    .FirstOrDefault(x => x.Id == request.Dto.MarketId).MarketTemplate;

                if (template == null)
                {
                    throw new NotFoundException("Market could not be found.");
                }

                HashSet<string> templateTypeSet = template.StallTypes.Select(x => x.Name).ToHashSet();
                request.Dto.Types.ForEach(x =>
                {
                    x.name = x.name.Trim();
                    x.description = x.description.Trim();
                });
                var dtoTypeSet = request.Dto.Types.Select(x => x.name).ToHashSet();

                if (templateTypeSet.Overlaps(dtoTypeSet))
                {
                    throw new ValidationException("Stalltypes already exists.");
                }

                var types = request.Dto.Types.Select(x => new Domain.Entities.StallType()
                {
                    Name = x.name,
                    Description = x.description,
                    MarketTemplate = template,
                    MarketTemplateId = template.Id,
                    Stalls = new List<Domain.Entities.Stall>()
                }).ToList();

                _context.StallTypes.AddRange(types);
                await _context.SaveChangesAsync(cancellationToken);
                return new CreateStallTypesResponse()
                {
                    StallTypes = types.Select(x => new StallTypeBaseVM()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description
                    }).ToList()
                };
            }
        }
    }
}
