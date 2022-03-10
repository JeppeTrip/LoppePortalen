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
                Organiser organiser = _context.Organisers.FirstOrDefault(x => x.Id == request.Dto.OrganiserId);
                if(organiser == null)
                    throw new NotImplementedException();

                List<ContactInfo> newContactInfo = new List<ContactInfo>();
                foreach (var contactInfo in request.Dto.ContactInformation)
                {
                   newContactInfo.Add(new ContactInfo
                    {
                        OrganiserId = organiser.Id,
                        Organiser = organiser,
                        ContactType = contactInfo.Value,
                        Value = contactInfo.Key
                    });
                }

                _context.ContactInformations.AddRange(newContactInfo);
                await _context.SaveChangesAsync(cancellationToken);

                Dictionary<int, KeyValuePair<string, ContactInfoType>> infoResponse = new Dictionary<int, KeyValuePair<string, ContactInfoType>>();
                foreach (var contactInfo in newContactInfo)
                {
                    infoResponse.Add(contactInfo.Id, new KeyValuePair<string, ContactInfoType>(contactInfo.Value, contactInfo.ContactType));
                }
                return new AddContactsToOrganiserResponse() { 
                    OrganiserId=organiser.Id,
                    ContactInformation=infoResponse
                };
            }
        }
    }
}
