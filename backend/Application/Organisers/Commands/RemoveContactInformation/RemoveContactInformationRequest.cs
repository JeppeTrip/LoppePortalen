using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Organisers.Commands.RemoveContactInformation
{
    public class RemoveContactInformationRequest
    {
        public int OrganiserId { get; set; }    
        public string Value { get; set; }
    }
}
