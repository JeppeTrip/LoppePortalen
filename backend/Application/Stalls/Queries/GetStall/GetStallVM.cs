using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Stalls.Queries.GetStall
{
    public class GetStallVM : StallBaseVM
    {
        public GetStallMarketVM Market { get; set; }
        public GetStallBoothVM Booth { get; set; }
    }
}
