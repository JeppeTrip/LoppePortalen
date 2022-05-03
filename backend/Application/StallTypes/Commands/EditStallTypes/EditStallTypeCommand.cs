using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.StallTypes.Commands.EditStallTypes
{
    [AuthorizeAttribute(Roles = "ApplicationUser")
    public class EditStallTypeCommand : IRequest<EditStallTypeResponse>
    {
        public EditStallTypeRequest Dto { get; set; }

        public class EditStallTypeCommandHandler : IRequestHandler<EditStallTypeCommand, EditStallTypeResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public EditStallTypeCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<EditStallTypeResponse> Handle(EditStallTypeCommand request, CancellationToken cancellationToken)
            {
                var instances = _context.MarketInstances
                    .Include(x => x.MarketTemplate)
                    .Include(x => x.MarketTemplate.StallTypes)
                    .Include(x => x.MarketTemplate.Organiser)
                    .Where(x => x.MarketTemplate.Organiser.UserId.Equals(_currentUserService.UserId));

                var instance = await instances.FirstOrDefaultAsync(x => x.Id == request.Dto.MarketId);

                if (instance == null )
                {
                    throw new NotFoundException($"Market with ID {request.Dto.MarketId} not found.");
                }

                var otherType = instance.MarketTemplate.StallTypes.FirstOrDefault(x => !x.Id.Equals(request.Dto.StallTypeId) && x.Name.Equals(request.Dto.StallTypeName));
                if(otherType != null)
                {
                    throw new ValidationException($"StallType with name {request.Dto.StallTypeName} already exists.");
                }

                var type = instance.MarketTemplate.StallTypes.FirstOrDefault(x => x.Id == request.Dto.StallTypeId);
                if(type == null)
                {
                    throw new NotFoundException($"Stalltype with ID {request.Dto.StallTypeId} not found.");
                }

                type.Name = request.Dto.StallTypeName;
                type.Description = request.Dto.StallTypeDescription;
                _context.StallTypes.Update(type);
                await _context.SaveChangesAsync(cancellationToken);

                return new EditStallTypeResponse();
            }
        }
    }
}
