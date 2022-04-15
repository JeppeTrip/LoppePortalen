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

namespace Application.StallTypes.Commands.CreateStallType
{
    public class CreateStallTypeCommand : IRequest<CreateStallTypeResponse>
    {
        public CreateStallTypeRequest Dto { get; set; }

        public class CreateStallTypeCommandHandler : IRequestHandler<CreateStallTypeCommand, CreateStallTypeResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public CreateStallTypeCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<CreateStallTypeResponse> Handle(CreateStallTypeCommand request, CancellationToken cancellationToken)
            {
                var template = _context.MarketInstances
                    .Include(x => x.MarketTemplate)
                    .Include(x => x.MarketTemplate.StallTypes)
                    .FirstOrDefault(x => x.Id == request.Dto.MarketId).MarketTemplate;

                if (template == null)
                {
                    throw new NotFoundException("Market could not be found.");
                }

                var existingType = template.StallTypes.FirstOrDefault(x => x.Name.Equals(request.Dto.Name));

                if (existingType != null)
                {
                    return new CreateStallTypeResponse(Result.Failure(new List<string>() { "Stall type names for this event overlaps with the new ones." }));
                }

                var type = new Domain.Entities.StallType()
                {
                    Name = request.Dto.Name,
                    Description = request.Dto.Description,
                    MarketTemplate = template,
                    MarketTemplateId = template.Id
                };


                _context.StallTypes.Add(type);
                await _context.SaveChangesAsync(cancellationToken);
                return new CreateStallTypeResponse(Result.Success())
                {
                    Id = type.Id,
                    Name = type.Name,
                    Description = type.Description
                };
            }
        }
    }
}
