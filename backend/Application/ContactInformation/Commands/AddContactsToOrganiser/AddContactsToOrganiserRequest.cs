using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ContactInformation.Commands.AddContactsToOrganiser
{
    public class AddContactsToOrganiserRequest
    {
        public int OrganiserId { get; set; }
        public Dictionary<string, ContactInfoType> ContactInformation { get; set; }
    }
}
