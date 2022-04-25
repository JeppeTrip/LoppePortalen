using Application.Common.Models;
using System.Collections.Generic;

namespace Application.Organisers.Queries.GetOrganiser
{
    public class GetOrganiserVM : OrganiserBaseVM
    {
        public List<MarketBaseVM> Markets { get; set; }
    }
}
