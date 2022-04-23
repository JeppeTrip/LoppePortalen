using Application.Common.Models;
using System.Collections.Generic;

namespace Application.Organisers.Queries.GetUsersOrganisers
{
    public class GetUsersOrganisersResponse
    {
        public List<OrganiserBaseVM> Organisers { get; set; }
    }
}
