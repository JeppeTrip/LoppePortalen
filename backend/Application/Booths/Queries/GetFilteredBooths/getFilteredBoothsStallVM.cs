using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Booths.Queries.GetFilteredBooths
{
    public class getFilteredBoothsStallVM : StallBaseVM
    {
        public MarketBaseVM Market { get; set; }
    }
}
