using Application.Common.Models;
using System.Collections.Generic;

namespace Application.Organisers.Queries.GetAllOrganisers
{
    public class GetAllOrganisersResponse
    {
        public List<OrganiserBaseVM> Organisers { get; set; }
    }
}
