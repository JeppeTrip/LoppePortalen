using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Organisers.Queries.GetOrganiser
{
    public class GetOrganiserVM : OrganiserBaseVM
    {
        public List<MarketBaseVM> Markets { get; set; }
    }
}
