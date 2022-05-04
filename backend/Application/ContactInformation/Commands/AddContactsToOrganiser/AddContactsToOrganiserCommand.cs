using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ContactInformation.Commands.AddContactsToOrganiser
{
    public class AddContactsToOrganiserCommand : IRequest<AddContactsToOrganiserResponse>
    {
        public AddContactsToOrganiserRequest Dto { get; set; }

        public class AddContactsToOrganiserCommandHandler : IRequestHandler<AddContactsToOrganiserCommand, AddContactsToOrganiserResponse>
        {
            private readonly IApplicationDbContext _context;

            public AddContactsToOrganiserCommandHandler(IApplicationDbContext context) { _context = context; }

            public async Task<AddContactsToOrganiserResponse> Handle(AddContactsToOrganiserCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
