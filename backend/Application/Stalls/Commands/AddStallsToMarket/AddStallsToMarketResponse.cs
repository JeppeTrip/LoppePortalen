using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Stalls.Commands.AddStallsToMarket
{
    public class AddStallsToMarketResponse
    {
        public List<StallBaseVM> Stalls { get; set; }
    }
}
