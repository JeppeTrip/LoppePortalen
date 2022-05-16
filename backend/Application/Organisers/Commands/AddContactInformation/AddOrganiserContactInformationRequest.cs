using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Organisers.Commands.AddContactInformation
{
    public class AddOrganiserContactInformationRequest
    {
        public int OrganiserId { get; set; }
        public string Value { get; set; }
        public ContactInfoType Type { get; set; }
    }
}
