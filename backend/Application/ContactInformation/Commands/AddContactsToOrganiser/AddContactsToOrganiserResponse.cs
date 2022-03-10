using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ContactInformation.Commands.AddContactsToOrganiser
{
    public class AddContactsToOrganiserResponse
    {
        public int OrganiserId { get; set; }
        public Dictionary<int, KeyValuePair<string, ContactInfoType>> ContactInformation { get; set; }
    }
}
