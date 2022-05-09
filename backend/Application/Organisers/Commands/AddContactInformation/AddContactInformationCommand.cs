using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Organisers.Commands.AddContactInformation
{
    [AuthorizeAttribute(Roles = "ApplicationUser")]
    public class AddContactInformationCommand : IRequest<AddContactInformationResponse>
    {
        public AddOrganiserContactInformationRequest Dto { get; set; }

        public class AddContactInformationCommandHandler : IRequestHandler<AddContactInformationCommand, AddContactInformationResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public AddContactInformationCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<AddContactInformationResponse> Handle(AddContactInformationCommand request, CancellationToken cancellationToken)
            {
                var organiser = await _context.Organisers
                    .Include(x => x.ContactInformation)
                    .FirstOrDefaultAsync(x => x.Id == request.Dto.OrganiserId && x.UserId.Equals(_currentUserService.UserId));

                if (organiser == null)
                    throw new NotFoundException("Organiser", request.Dto.OrganiserId);

                if(organiser.ContactInformation.Select(x => x.Value).Contains(request.Dto.Value))
                    throw new ValidationException($"Organiser {request.Dto.OrganiserId} already have contacts with value {request.Dto.Value}");

                ContactInfo info = new ContactInfo()
                {
                    OrganiserId = organiser.Id,
                    ContactType = request.Dto.Type,
                    Value = request.Dto.Value
                };

                _context.ContactInformations.Add(info);
                await _context.SaveChangesAsync(cancellationToken);

                return new AddContactInformationResponse();

            }
        }
    }
}
