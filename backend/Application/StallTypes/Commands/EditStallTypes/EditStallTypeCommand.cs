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

namespace Application.StallTypes.Commands.EditStallTypes
{
    public class EditStallTypeCommand : IRequest<EditStallTypeResponse>
    {
        public EditStallTypeRequest Dto { get; set; }

        public class EditStallTypeCommandHandler : IRequestHandler<EditStallTypeCommand, EditStallTypeResponse>
        {
            private readonly IApplicationDbContext _context;

            public EditStallTypeCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<EditStallTypeResponse> Handle(EditStallTypeCommand request, CancellationToken cancellationToken)
            {
                var instance = await _context.MarketInstances
                    .Include(x => x.MarketTemplate)
                    .Include(x => x.MarketTemplate.StallTypes)
                    .FirstOrDefaultAsync(x => x.Id == request.Dto.MarketId);
                if(instance == null )
                {
                    throw new NotFoundException($"Market with ID {request.Dto.MarketId} not found.");
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
