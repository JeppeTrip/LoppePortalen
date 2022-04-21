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

namespace Application.StallTypes.Queries.GetStallType
{
    public class GetStallTypeQuery : IRequest<GetStallTypeResponse>
    {
        public GetStallTypeRequest Dto { get; set; }

        public class GetStallTypeQueryHandler : IRequestHandler<GetStallTypeQuery, GetStallTypeResponse>
        {
            private readonly IApplicationDbContext _context;

            public GetStallTypeQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<GetStallTypeResponse> Handle(GetStallTypeQuery request, CancellationToken cancellationToken)
            {
                var stallType = await _context.StallTypes.FirstOrDefaultAsync(x => x.Id == request.Dto.StallTypeId);
                if (stallType == null)
                    throw new NotFoundException($"No stalltype with id {request.Dto.StallTypeId}");

                return new GetStallTypeResponse()
                {
                    Name = stallType.Name,
                    Description = stallType.Description,
                    StallTypeId = stallType.Id
                };
            }
        }
    }

}
