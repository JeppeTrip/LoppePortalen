using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Stalls.Queries.GetMarketStalls
{
    public class GetMarketStallsResponse
    {
        public int StallId { get; set; }
        public string StallName { get; set; }
        public string StallDescription { get; set; }
    }
}
