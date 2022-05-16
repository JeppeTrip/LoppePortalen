using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.StallTypes.Commands.CreateStallType
{
    [AuthorizeAttribute(Roles = "ApplicationUser")]
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
                var instances = _context.MarketInstances
                    .Include(x => x.MarketTemplate)
                    .Include(x => x.MarketTemplate.StallTypes)
                    .Include(x => x.MarketTemplate.Organiser);

                var usersInstances = instances.Where(x => x.MarketTemplate.Organiser.UserId.Equals(_currentUserService.UserId));

                if (usersInstances.Count() == 0)
                {
                    throw new NotFoundException($"No market with ID {request.Dto.MarketId}.");
                }

                var instance = usersInstances.FirstOrDefault(x => x.Id == request.Dto.MarketId);

                if (instance == null)
                {
                    throw new NotFoundException($"No market with ID {request.Dto.MarketId}.");
                }

                var template = instance.MarketTemplate;
                request.Dto.Name = request.Dto.Name.Trim();
                request.Dto.Description = request.Dto.Description.Trim();
                var existingType = template.StallTypes.FirstOrDefault(x => x.Name.Equals(request.Dto.Name));

                if (existingType != null)
                {
                    throw new ValidationException($"Market with ID {request.Dto.MarketId} already defines stalltype {request.Dto.Name}");
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
                return new CreateStallTypeResponse()
                {
                    Id = type.Id,
                    Name = type.Name,
                    Description = type.Description
                };
            }
        }
    }
}
