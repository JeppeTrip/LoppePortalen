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

namespace Application.Organisers.Commands.EditOrganiser
{
    public class EditOrganiserCommand : IRequest<EditOrganiserResponse>
    {
        public EditOrganiserRequest Dto { get; set; }

        public class EditOrganiserCommandHandler : IRequestHandler<EditOrganiserCommand, EditOrganiserResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public EditOrganiserCommandHandler(
                IApplicationDbContext context,
                ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<EditOrganiserResponse> Handle(EditOrganiserCommand request, CancellationToken cancellationToken)
            {
                if (!request.Dto.UserId.Equals(_currentUserService.UserId))
                {
                    throw new UnauthorizedAccessException();
                }
                var organiser = await _context.Organisers
                    .Include(x => x.User)
                    .Include(x => x.Address)
                    .FirstOrDefaultAsync(x => x.UserId.Equals(request.Dto.UserId) && x.Id == request.Dto.OrganiserId);
                if(organiser == null)
                {
                    throw new UnauthorizedAccessException();
                }

                organiser.Name = request.Dto.Name;
                organiser.Description = request.Dto.Description;
                organiser.Address.Street = request.Dto.Street;
                organiser.Address.City = request.Dto.City;
                organiser.Address.Appartment = request.Dto.Appartment;
                organiser.Address.PostalCode = request.Dto.PostalCode;
                organiser.Address.Number = request.Dto.Number;
                _context.Organisers.Update(organiser);
                await _context.SaveChangesAsync(cancellationToken);

                return new EditOrganiserResponse(true, new List<string>());
            }
        }
    }
}
