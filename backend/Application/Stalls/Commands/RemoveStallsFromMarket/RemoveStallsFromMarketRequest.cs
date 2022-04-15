using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Stalls.Commands.RemoveStallsFromMarket
{
    public class RemoveStallsFromMarketRequest
    {
        public int MarketId { get; set; }
        public int StallTypeId { get; set; }
        public int Diff { get; set; }
    }
}
