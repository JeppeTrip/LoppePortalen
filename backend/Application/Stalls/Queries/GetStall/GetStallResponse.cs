using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Stalls.Queries.GetStall
{
    public class GetStallResponse
    {
        public int StallId { get; set; }    
        public int StallTypeId { get; set; }
        public int MarketInstanceId { get; set; }
    }
}
