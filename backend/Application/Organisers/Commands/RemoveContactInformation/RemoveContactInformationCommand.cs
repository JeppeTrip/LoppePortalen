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

namespace Application.Organisers.Commands.RemoveContactInformation
{
    [AuthorizeAttribute(Roles = "ApplicationUser")]
    public class RemoveContactInformationCommand : IRequest<RemoveContactInformationResponse>
    {
        public RemoveContactInformationRequest Dto { get; set; }

        public class RemoveContactInformationCommandHandler : IRequestHandler<RemoveContactInformationCommand, RemoveContactInformationResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public RemoveContactInformationCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<RemoveContactInformationResponse> Handle(RemoveContactInformationCommand request, CancellationToken cancellationToken)
            {
                var organiser = await _context.Organisers
                    .Include(x => x.ContactInformation)
                    .FirstOrDefaultAsync(x => x.Id == request.Dto.OrganiserId && x.UserId.Equals(_currentUserService.UserId));

                if (organiser == null)
                    throw new NotFoundException("Organiser", request.Dto.OrganiserId);

                var contact = organiser.ContactInformation.FirstOrDefault(x => x.Value.Equals(request.Dto.Value));
                if (contact == null)
                    throw new NotFoundException($"Organiser {request.Dto.OrganiserId} have not contacts matching {request.Dto.Value}");

                _context.ContactInformations.Remove(contact);
                await _context.SaveChangesAsync(cancellationToken);

                return new RemoveContactInformationResponse();
            }
        }
    }
}
