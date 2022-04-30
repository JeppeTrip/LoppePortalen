using Application.Common.Models;
using System.Collections.Generic;

namespace Application.Stalls.Queries.GetStall
{
    public class GetStallVM : StallBaseVM
    {
        public GetStallMarketVM Market { get; set; }
        public List<GetStallBoothVM> Booths { get; set; } = new List<GetStallBoothVM>();
    }
}
